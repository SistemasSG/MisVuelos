using MisVuelos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MisVuelos.Models;
using Xamarin.Forms;
using MisVuelos.Views;

namespace MisVuelos
{
    public partial class App : Application
    {
        static MisVuelosDataBase database;

        public static MisVuelosDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MisVuelosDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MisVuelos.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            //database.EliminarVuelos();
            IniciarVuelos();
            //MainPage = new MisVuelos.MainPage();
            MainPage = new ListaVuelosPage();
        }

        private static void IniciarVuelos()
        {
            List<Vuelos> v = new List<Vuelos>();
            v = database.GetVuelosAsync().Result.ToList();
            Random rnd = new Random();
            if (v.Count == 0)
            {

                List<Aerolineas> aerolineas = database.GetAerolineas();
                List<Ciudades> destinos = database.GetCiudades();

                for (int i = 0; i < 50; i++)
                {
 
                    DateTime fec_sal = DateTime.Now.AddDays(rnd.Next(1, 30)).AddHours(rnd.Next(1, 23)).AddMinutes(rnd.Next(1,15));  //new DateTime(DateTime.Now.Year, mes, dia, hora, minuto, 0);

                    string _origen = destinos[rnd.Next(1, 7)].ciudad;
                    string _destino = destinos[rnd.Next(1, 7)].ciudad;
                    string _aerolinea = aerolineas[rnd.Next(1, 5)].aerolinea;
                    while (_destino == _origen)
                    {
                        _destino = destinos[rnd.Next(1, 7)].ciudad;
                    }

                    database.RegistrarVuelo(new Vuelos
                    {
                        aerolinea = _aerolinea,
                        asientos = 120,
                        asientos_dis = 120,
                        fecha = fec_sal,
                        origen = _origen,
                        destino = _destino,
                        precio = rnd.Next(120, 160) *
                                (fec_sal.DayOfWeek == DayOfWeek.Friday || fec_sal.DayOfWeek == DayOfWeek.Saturday || fec_sal.DayOfWeek == DayOfWeek.Sunday ? 1.30m : 1.00m) *
                                (fec_sal.Hour > 18 ? 1.10m : 1.00m)
                    });
                }
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
