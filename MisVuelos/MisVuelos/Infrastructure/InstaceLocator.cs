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
        public ListaVuelosViewModel Main { get; set; }

        public InstaceLocator()
        {
            Main = new ListaVuelosViewModel();
        }

        
    }
}
