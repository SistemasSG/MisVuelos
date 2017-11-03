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
        }

        private void Buscar_Clicked(object sender, EventArgs e)
        {
            Validaciones();
        }


        private void Validaciones()

        {

            string po = pck_origen.Items[pck_origen.SelectedIndex].Trim();
            string de = pck_destino.Items[pck_destino.SelectedIndex].Trim();

            if (po == de)
            {
                DisplayAlert("Error", "El origen y el destino no pueden ser iguales.", "Reintentar");
            }
            else
            {
                Navigation.PushAsync(new ListaVuelosPage(po,de));
            }
        }

    }
}