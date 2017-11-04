using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MisVuelos.ViewModels
{
    public class MainPageViewModel : ContentPage
    {
        public MainPageViewModel()
        {
            CargarBusquedas();
        }

        public List<Models.Aerolineas> ListaAerolineas
        {
            get;
            set;
        }

        public List<Models.Ciudades> ListaCiudades
        {
            get;
            set;
        }

        private void CargarBusquedas()
        {
            try
            {
                ListaAerolineas = App.Database.GetAerolineas().ToList();
                ListaCiudades = App.Database.GetCiudades().ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

        }




    }
}