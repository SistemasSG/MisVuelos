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
            
        }

        public ObservableCollection<Data.MisVuelosDataBase> ListaCiudades
        {
            get;
            set;
        }


    }
}