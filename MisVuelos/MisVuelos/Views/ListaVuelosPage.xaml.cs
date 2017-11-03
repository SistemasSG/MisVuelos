using MisVuelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisVuelos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaVuelosPage : ContentPage
    {
        public ListaVuelosPage()
        {
            InitializeComponent();
           
        }

        public ListaVuelosPage(string origen, string destino)
        {
            InitializeComponent();
            BuscarVuelos(origen, destino);
        }

        public ObservableCollection<Models.Vuelos> Lista
        {
            get;
            set;
        }
        private async void BuscarVuelos(string origen, string destino)
        {
            try
            {
                Lista = new ObservableCollection<Models.Vuelos>();
                var client = await App.Database.GetVuelosAsync();
                var lista = client.Where(x => x.origen.Trim() == origen.Trim() && x.destino.Trim() == destino.Trim()).ToList(); // && x.fecha.Value.Date == fecha.Date).ToList();

                foreach (var item in lista)
                {
                    Lista.Add(item);
                }
                ListaVuelos.ItemsSource = Lista;
                ListaVuelos.ItemSelected += ListaVuelos_ItemSelected;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ListaVuelos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Id_vuelo = (ListaVuelos.SelectedItem as Models.Vuelos).ID;
            Navigation.PushAsync(new ReservarPage(Id_vuelo));
        }

    }
}