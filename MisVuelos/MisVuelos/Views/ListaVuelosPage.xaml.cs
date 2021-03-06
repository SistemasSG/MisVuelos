﻿using MisVuelos.ViewModels;
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

        public ListaVuelosPage(string origen, string destino, int dia, int mes, int año)
        {
            InitializeComponent();
            BuscarVuelos(origen, destino, dia, mes, año);
        }

        public ObservableCollection<Models.Vuelos> Lista
        {
            get;
            set;
        }
        private async void BuscarVuelos(string origen, string destino, int dia, int mes, int año)
        {
            try
            {
                Lista = new ObservableCollection<Models.Vuelos>();
                var client = await App.Database.GetVuelosAsync();
                var lista = client.Where
                                (
                                    x => x.origen.Trim() == origen.Trim() && 
                                    x.destino.Trim() == destino.Trim() &&
                                    x.fecha.Value.Day == dia &&
                                    x.fecha.Value.Month == mes &&
                                    x.fecha.Value.Year == año
                                    ).ToList(); // && x.fecha.Value.Date == fecha.Date).ToList();

                foreach (var item in lista)
                {
                    Lista.Add(item);
                }
                ListaVuelos.ItemsSource = Lista;
                ListaVuelos.ItemSelected += ListaVuelos_ItemSelected;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.InnerException.Message, "Cerrar");
            }
        }

        private void ListaVuelos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            if (((ListView)sender).SelectedItem != null)
            {
                var Id_vuelo = (ListaVuelos.SelectedItem as Models.Vuelos).ID;
                Navigation.PushAsync(new ReservarPage(Id_vuelo));
            }
        }

    }
}