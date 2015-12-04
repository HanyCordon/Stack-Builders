using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PicoPlacaNameSpaces;
using PicoPlacaClass;

namespace PicoYPlacaNameSpace
{
    public partial class PicoYPlacaForm : Form
    {
        PicoPlacaValidacion picoPlacaValidacion = new PicoPlacaValidacion();
        List<PicoPlacaValidacion> listaPicoPlacaValidacion = new List<PicoPlacaValidacion>();
        DateTime fechaNuevo = new DateTime();
        int dia, mes, a;
        
        public PicoYPlacaForm()
        {
            InitializeComponent();
        }
        public List<PicoPlacaValidacion> Validaciones(int d, int m, int y)
        {
            
            List<PicoPlacaValidacion> listaPicoPlacaValidacion = new List<PicoPlacaValidacion>();
            try
            {
                int placa = 1;
                for (int dia = 1; dia < 6; dia++)
                {                    
                    for (int i = placa; i <= placa+1; i++)
			        {
                        int numPlaca = i;
                        //Si ya llego a 10
                        if (i == 10)
                           numPlaca = 0;
                        picoPlacaValidacion = new PicoPlacaValidacion(numPlaca.ToString(),dia.ToString(),
                            new DateTime(y,m,d,7,0,0), //asi aseugro que sea en el mismo dia aunque el usuario haya movido el calendario de la hora
                            new DateTime(y,m,d,9,30,0),
                            new DateTime(y,m,d,16,0,0),new DateTime(y,m,d,19,30,0));
                
                        listaPicoPlacaValidacion.Add(picoPlacaValidacion);
			        }
                    placa = placa+2; //incremento en 2 para que no repita
                    
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message);
            }
            return listaPicoPlacaValidacion;
        }

        private void picoPlacaButton_Click(object sender, EventArgs e)
        {
            try
            {
                //ingrese todos los campos
                if (this.placTextBox.Text == string.Empty || this.fechaDateTimePicker.Value == null
                    || this.tiempoDateTimePicker.Value == null)
                {
                    MessageBox.Show("Todos los datos son obligatorios, favor ingrese todos los campos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //la placa debe tener 7 digitos en Ecuador
                    if (this.placTextBox.Text.Length < 7)
                    {
                        MessageBox.Show("La placa debe tener 7 digitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                   
                    
                    dia = fechaDateTimePicker.Value.Day;
                    mes = fechaDateTimePicker.Value.Month;
                    a = fechaDateTimePicker.Value.Year;   
                    //llamo al predictor       
                    Predictor predictor = new Predictor();
                    PicoPlacaME picoPlacaME = new PicoPlacaME();
                    picoPlacaME.PicoPlaca = this.EnviarDatos();
                    picoPlacaME.ListaValidacion = this.Validaciones(dia, mes, a);
                    bool puedeCircular = predictor.Pronosticar(picoPlacaME);
                    if (!puedeCircular)
                        MessageBox.Show("No puede viajar, tiene pico y placa.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Puede viajar.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message);
            }
        }


        public PicoPlaca EnviarDatos()
        {
            PicoPlaca picoPlaca = new PicoPlaca();
            try
            {
                picoPlaca.Placa = this.placTextBox.Text;
                picoPlaca.Fecha = this.fechaDateTimePicker.Value.ToString();
                picoPlaca.Tiempo = new DateTime(a,mes,dia,int.Parse(this.tiempoDateTimePicker.Value.TimeOfDay.Hours.ToString()),
                    int.Parse(this.tiempoDateTimePicker.Value.TimeOfDay.Minutes.ToString()),
                    int.Parse(this.tiempoDateTimePicker.Value.TimeOfDay.Seconds.ToString()));
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message);
            }
            return picoPlaca;
        }


        private void salirButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message);
            }
        }

    }
}
