using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisVuelos.ViewModels
{
    public class ListaVuelosViewModel
    {
        public ListaVuelosViewModel()
        {
            CargarVuelos();
        }

        public ObservableCollection<Models.Vuelos> ListaVuelos
        {
            get;
            set;
        }

        private async void CargarVuelos()
        {
            try
            {
                ListaVuelos = new ObservableCollection<Models.Vuelos>();
                var client = await App.Database.GetVuelosAsync();
                var lista = client.ToList();

                foreach (var item in lista)
                {
                    ListaVuelos.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
