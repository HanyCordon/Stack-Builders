using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicoPlacaNameSpaces;

namespace PicoPlacaClass
{
    public class Predictor
    {
        public bool Pronosticar(PicoPlacaME picoPlacaME)
        {
            bool puedeViajar = true;
            try
            {
                int longitud = picoPlacaME.PicoPlaca.Placa.Length - 1; //longitud
                DateTime fecha = Convert.ToDateTime(picoPlacaME.PicoPlaca.Fecha);
                string diaSemana = ((int)(fecha.DayOfWeek)).ToString();
                var listaComparar = from l in picoPlacaME.ListaValidacion
                                    where l.Placa == picoPlacaME.PicoPlaca.Placa.Substring(longitud, 1) &&
                                    l.Dia == diaSemana &&
                                    ((picoPlacaME.PicoPlaca.Tiempo >= Convert.ToDateTime(l.HoraInicioM) &&
                                    picoPlacaME.PicoPlaca.Tiempo <= Convert.ToDateTime(l.HoraFinM)) ||
                                    (picoPlacaME.PicoPlaca.Tiempo >= Convert.ToDateTime(l.HoraInicioT) &&
                                    picoPlacaME.PicoPlaca.Tiempo <= Convert.ToDateTime(l.HoraFinT)))
                                    select l;
                if (listaComparar.Count() > 0)
                    puedeViajar = false;
            }
            catch (Exception ex)
            {

               // MessageBox.Show("Ocurrio el siguiente error: " + ex.Message);
            }
            return puedeViajar;
        }
    }
}
