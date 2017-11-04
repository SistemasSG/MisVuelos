using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MisVuelos.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisVuelos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservacionesPage : ContentPage
    {
        public ReservacionesViewModel CargarDatos { get; set; }

        public ReservacionesPage(int cedula = 0, string reserva ="" )
        {
            InitializeComponent();
            CargarDatos = new ReservacionesViewModel(cedula, reserva);
            BindingContext = CargarDatos;
            var a = 0;
        }
    }
}