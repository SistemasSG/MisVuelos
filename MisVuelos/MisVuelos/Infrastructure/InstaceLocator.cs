using MisVuelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisVuelos.Infrastructure
{
    public class InstaceLocator
    {
        //public ListaVuelosViewModel MainVuelos { get; set; }
        public MainPageViewModel MainPage { get; set; }
        public InstaceLocator()
        {
            MainPage = new MainPageViewModel();
            //MainVuelos = new ListaVuelosViewModel();
        }


    }
}
