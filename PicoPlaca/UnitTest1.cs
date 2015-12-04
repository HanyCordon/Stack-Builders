using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PicoPlacaNameSpaces;
using PicoYPlacaNameSpace;

namespace PicoPlacaNameSpaces
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void predictorTest()
        {
            PicoPlacaValidacion picoPlacaValidacion = new PicoPlacaValidacion();
            List<PicoPlacaValidacion> listaValidaciones = new List<PicoPlacaValidacion>();
            //lleno la lista de validaciones
            int y = 2015;
            int m = 12;
            int d = 02;
            int placa = 1;
            for (int dia = 1; dia < 6; dia++)
            {
                for (int i = placa; i <= placa + 1; i++)
                {
                    int numPlaca = i;
                    //Si ya llego a 10
                    if (i == 10)
                        numPlaca = 0;
                    picoPlacaValidacion = new PicoPlacaValidacion(numPlaca.ToString(), dia.ToString(), new DateTime(y,m,d,7,0,0),
                        new DateTime(y,m,d,9,30,0),
                        new DateTime(y,m,d,16,0,0), new DateTime(y,m,d,19,30,0));

                    listaValidaciones.Add(picoPlacaValidacion);
                }
                placa = placa + 2;
            }

            //estos datos serian el input
            PicoPlaca picoPlaca = new PicoPlaca("PV-1235", "02/12/2015", new DateTime(2015, 12, 2, 8, 40, 0));
            //si se ponen solamente dos digitos no funciona

            //longitud de la cadena
            int longitud = picoPlaca.Placa.Length - 1;

            //sacar el ultimo digito
            DateTime fecha= Convert.ToDateTime("02/12/2015");
            string diaSemana = ((int)(fecha.DayOfWeek)).ToString();
            string placaVehiculo = picoPlaca.Placa.Substring(longitud, 1);
            string listaComparar = (from l in listaValidaciones
                                where l.Placa == placaVehiculo &&
                                l.Dia == diaSemana &&
                                 ((picoPlaca.Tiempo >= l.HoraInicioM &&
                                picoPlaca.Tiempo <= l.HoraFinM) ||
                                (picoPlaca.Tiempo >= l.HoraInicioT &&
                                picoPlaca.Tiempo <= l.HoraFinT) )          
                                select l.Dia).FirstOrDefault();

            Assert.AreEqual(listaComparar, diaSemana, "No puede viajar, tiene pico y placa.");

            
        }
    }
}
