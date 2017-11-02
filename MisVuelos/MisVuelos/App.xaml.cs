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
            IniciarVuelos();
            //MainPage = new MisVuelos.MainPage();
            MainPage = new ListaVuelosPage();
        }

        private static void IniciarVuelos()
        {
            //database.EliminarVuelos();
            List<Vuelos> v = new List<Vuelos>();
            v = database.GetVuelosAsync().Result.ToList();
            if (v.Count == 0)
            {
                string[] aerolineas = { "Aserca", "Conviasa", "Laser", "Venezolana", "Avior" };
                string[] destinos = { "MCBO", "CCS", "BTO", "PTOO", "VAL", "MAT", "MARG" };

                for (int i = 0; i < 50; i++)
                {
                    Random rnd = new Random();
                    int mes = rnd.Next(1, 12);
                    //Por razones de tiempo no valido año bisiesto
                    int dia = rnd.Next(1, (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12 ? 31 : 30));
                    int hora = rnd.Next(0, 23);
                    int minuto = rnd.Next(1, 59);
                    DateTime fec_sal = new DateTime(DateTime.Now.Year, mes, dia, hora, minuto, 0);

                    string _origen = destinos[rnd.Next(1, 7)];
                    string _destino = destinos[rnd.Next(1, 7)];
                    string _aerolinea = aerolineas[rnd.Next(1, 5)];
                    while (_destino == _origen)
                    {
                        _destino = destinos[rnd.Next(1, 7)];
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
