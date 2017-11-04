using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisVuelos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            pck_origen.SelectedIndex = -1;
            pck_destino.SelectedIndex = -1;
            pck_origen.Focus();
        }

        private void Validaciones()

        {
            if (pck_origen.SelectedIndex == -1 || pck_destino.SelectedIndex == -1)
            {
                DisplayAlert("Error", "Elija un origen y un destino", "OK");
            }
            else
            {
                string po = pck_origen.Items[pck_origen.SelectedIndex].Trim();
                string de = pck_destino.Items[pck_destino.SelectedIndex].Trim();

                if (po == de)
                {
                    DisplayAlert("Error", "El origen y el destino no pueden ser iguales.", "OK");
                }
                else
                {
                    if (App.Database.GetVuelosAsync().Result.Where
                        (
                            x => x.origen.Trim() == po.Trim() &&
                            x.destino.Trim() == de.Trim() &&
                            x.fecha.Value.Day == dp_fecha.Date.Day &&
                            x.fecha.Value.Month == dp_fecha.Date.Month &&
                            x.fecha.Value.Year == dp_fecha.Date.Year
                            ).ToList().Count > 0)
                        Navigation.PushAsync(new ListaVuelosPage(po, de, dp_fecha.Date.Day, dp_fecha.Date.Month, dp_fecha.Date.Year));
                    else
                        DisplayAlert("Error", "No hay vuelos disponibles por el momento.", "OK");
                }
            }


        }

        private void Buscar_Clicked(object sender, EventArgs e)
        {
            Validaciones();
        }

        private void buscar_reserva_Clicked(object sender, EventArgs e)
        {

        }
    }
}