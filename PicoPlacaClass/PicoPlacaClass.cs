using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicoPlacaNameSpaces
{
    public class PicoPlaca
    {
        public string Placa { get; set; }

        public string Fecha { get; set; }

        public DateTime Tiempo { get; set; }

        public PicoPlaca() { }
        public PicoPlaca(string placa,
                  string fecha,
                  DateTime tiempo)
        {
            Placa = placa;
            Fecha = fecha;
            Tiempo = tiempo;
        }
    }
    public class PicoPlacaValidacion
    {
        public string Dia { get; set; }

        public string Placa { get; set; }

        public DateTime HoraInicioM { get; set; }
        public DateTime HoraFinM { get; set; }
        public DateTime HoraInicioT { get; set; }
        public DateTime HoraFinT { get; set; }

        public PicoPlacaValidacion() { }
        public PicoPlacaValidacion(string placa,
                  string dia,
                  DateTime horaInicioM,
            DateTime horaFinM, DateTime horaInicioT,
            DateTime horaFinT)
        {
            Placa = placa;
            HoraInicioM = horaInicioM;
            HoraFinM = horaFinM;
            HoraInicioT = horaInicioT;
            HoraFinT = horaFinT;
            Dia = dia;
        }
    }

    public class PicoPlacaME
    {
        public PicoPlaca PicoPlaca { get; set; }

        public List<PicoPlacaValidacion> ListaValidacion { get; set; }

        public PicoPlacaME() { }
        public PicoPlacaME(PicoPlaca picoPlaca,
                  List<PicoPlacaValidacion> listaValidacion)
        {
            PicoPlaca = picoPlaca;
            ListaValidacion = listaValidacion;
        }
    }
}
