﻿using MisVuelos.Data;
using MisVuelos.Models;
using MisVuelos.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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
        public Page ReservarPage { get; set; }

        public App()
        {
            InitializeComponent();
            IniciarVuelos();
            //MainPage = new ListaVuelosPage();
            MainPage = new NavigationPage(new MainPage());
            //ReservarPage = new NavigationPage(new ReservarPage());

        }

        private static void IniciarVuelos()
        {
            try
            {
                List<Vuelos> v = new List<Vuelos>();
                v = database.GetVuelosAsync().Result.ToList();
                Random rnd = new Random();
                if (v.Count == 0)
                {

                    List<Aerolineas> aerolineas = database.GetAerolineas();
                    List<Ciudades> destinos = database.GetCiudades();
                    string _origen;
                    string _destino;

                    foreach (var item_a in aerolineas)
                    {
                        foreach (var item_o in destinos)
                        {
                            foreach (var item_d in destinos)
                            {
                                if (item_d.ciudad.Trim() != item_o.ciudad.Trim())
                                {
                                    for (int i = 0; i < 50; i++)
                                    {
                                        DateTime fec_sal = DateTime.Now.AddDays(i).AddHours(rnd.Next(1, 23)).AddMinutes(rnd.Next(1, 15));  //new DateTime(DateTime.Now.Year, mes, dia, hora, minuto, 0);
                                        _origen = item_o.ciudad;
                                        _destino = item_d.ciudad;
                                        string _aerolinea = item_a.aerolinea;

                                        database.RegistrarVuelo(new Vuelos
                                        {
                                            aerolinea = _aerolinea,
                                            asientos = 120,
                                            asientos_dis = 120,
                                            fecha = fec_sal,
                                            origen = _origen,
                                            destino = _destino,
                                            precio = 150 *
                                                    (fec_sal.DayOfWeek == DayOfWeek.Friday || fec_sal.DayOfWeek == DayOfWeek.Saturday || fec_sal.DayOfWeek == DayOfWeek.Sunday ? 1.30m : 1.00m) *
                                                    (fec_sal.Hour > 18 ? 1.10m : 1.00m)
                                        });
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw;
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
