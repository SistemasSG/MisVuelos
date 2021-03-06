﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisVuelos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservarPage : ContentPage
    {
        int _id_vuelo;
        int _id_cliente;

        public ReservarPage(int Id)
        {
            InitializeComponent();
            _id_vuelo = Id;
        }

        private bool ValidarReservasExistentes()
        {
            //int _id_cliente = App.Database.GetClientesAsync().Result.Where( x=> x.Cedula == Convert.ToInt32(cedula.Text)).FirstOrDefault().ID;
            int reservas = App.Database.GetReservacionAsync(_id_cliente).Result.Where
                        (
                            x => x.fecha.Day == DateTime.Now.Day &&
                            x.fecha.Month == DateTime.Now.Month &&
                            x.fecha.Year == DateTime.Now.Year
                        ).ToList().Count;   
            
            return reservas > 0 ? true : false;
           
        }

        private async void RegistrarReservaAsync(int Id_vuelo)
        {
            try
            {
                string n = nombre.Text.ToString();
                int c = Convert.ToInt32(cedula.Text);
                int e = Convert.ToInt32(edad.Text);

                bool ExisteCliente = App.Database.GetClientesAsync().Result.Where(x => x.Cedula == c).ToList().Count > 0;
                if (ExisteCliente == false)
                {
                    _id_cliente = await App.Database.RegistrarCliente(
                    new Models.Clientes
                    {
                        Nombre = n,
                        Cedula = c,
                        Edad = e
                    }
                    );
                }
                else
                {
                    _id_cliente = App.Database.GetClientesAsync().Result.Where(x => x.Cedula == c).FirstOrDefault().ID;
                }
                
                if (ValidarReservasExistentes() == true)
                    await DisplayAlert("Mis Vuelos", "No puede hacer mas reservas por el dia de hoy." , "OK");
                else
                {
                    var client = await App.Database.GetClientesAsync();
                    decimal _precioasiento = App.Database.GetVuelosAsync().Result.Where(x => x.ID == Id_vuelo).FirstOrDefault().precio;
                    var _reserva = Guid.NewGuid();

                    await App.Database.RegistrarReservacion(
                        new Models.Reservaciones
                        {
                            id_cliente = Convert.ToInt32(_id_cliente),
                            id_vuelo = Id_vuelo,
                            asientos = Convert.ToInt32(asientos.Text),
                            reserva = _reserva.ToString().Substring(0, 5).ToUpper(),
                            pago = Convert.ToInt32(asientos.Text) * _precioasiento,
                            fecha = DateTime.Now
                        }
                        );

                    var _vuelo = App.Database.GetVueloAsync(Id_vuelo).Result;
                    var _total_asientos = App.Database.GetReservacionesAsync().Result.Where(x => x.id_vuelo == Id_vuelo).ToList();
                    var _asientos_ocupados = _total_asientos.Sum(y => y.asientos);

                    await App.Database.RegistrarVuelo(new Models.Vuelos
                    {
                        ID = _vuelo.ID,
                        origen = _vuelo.origen,
                        destino = _vuelo.destino,
                        precio = _vuelo.precio,
                        fecha = _vuelo.fecha,
                        asientos = _vuelo.asientos,
                        asientos_dis = _vuelo.asientos - _asientos_ocupados,
                    }
                    );

                    await DisplayAlert("Mis Vuelos", "Su numero de reservacion es: " + _reserva.ToString().Substring(0, 5).ToUpper(), "OK");
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.InnerException.Message, "Cancelar");
            }
            
        }

        private bool ValidarCampos()
        {
            if (nombre.Text == null && cedula.Text == null && edad.Text == null && asientos.Text == null)
            { return false; }

            else { return true; }
                
        }

        private bool ValidarEdad()
        {
            if (Convert.ToInt32(edad.Text) < 18)
                return false;
            else
                return true;
        }

        private bool ValidarReservasDias()
        {
            
            if (Convert.ToInt32(edad.Text) < 18)
                return false;
            else
                return true;
        }

        private void reservar_Clicked(object sender, EventArgs e)
        {
            if(ValidarCampos() == false)
            {
                DisplayAlert("Error", "Por favor ingrese valores en todos los campos.", "OK");
            }
            else
            {
                if (ValidarEdad() == false)
                {
                    DisplayAlert("Error", "Debe ser mayor de edad.", "OK");
                }
                else
                {
                    RegistrarReservaAsync(_id_vuelo);
                }
            }
            
            
        }
    }
}