using MisVuelos.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisVuelos.Data
{

    public class MisVuelosDataBase
    {
        readonly SQLiteAsyncConnection database;

        public MisVuelosDataBase (string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Vuelos>().Wait();
            database.CreateTableAsync<Clientes>().Wait();
            database.CreateTableAsync<Reservaciones>().Wait();
        }

        public Task<List<Vuelos>>GetVuelosAsync()
        {
            //return database.Table<Vuelos>().Where( x => x.fecha >= DateTime.Now && x.asientos_dis > 0).ToListAsync();
            return database.Table<Vuelos>().Where(x => x.asientos_dis > 0).ToListAsync();
        }

        public Task<Vuelos> GetVueloAsync(int id)
        {
            return database.Table<Vuelos>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<Reservaciones>> GetReservacionesAsync()
        {
            return database.Table<Reservaciones>().ToListAsync();
        }

        public Task<List<Reservaciones>> GetReservacionAsync(int cedula = 0, string reserva = "")
        {
            return database.Table<Reservaciones>().Where(x => x.id_cliente == cedula || x.reserva.Trim() == reserva.Trim()).ToListAsync();
        }

        public Task<List<Clientes>> GetClientesAsync()
        {
            return database.Table<Clientes>().ToListAsync();
        }

        public Task<int> RegistrarReservacion(Reservaciones item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> RegistrarVuelo(Vuelos item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public bool EliminarVuelos()
        {
            return database.QueryAsync<Vuelos>("delete from Vuelos").IsCompleted;
        }

        public Task<int> RegistrarCliente(Clientes item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public List<Ciudades> GetCiudades()
        {
            List<Ciudades> c = new List<Ciudades>
            {
                new Ciudades { ciudad = "MBO" },
                new Ciudades { ciudad = "CCS" },
                new Ciudades { ciudad = "PTO" },
                new Ciudades { ciudad = "MAT" },
                new Ciudades { ciudad = "POR" },
                new Ciudades { ciudad = "VAL" },
                new Ciudades { ciudad = "BTO" }
            };
            return c;

        }

        public List<Aerolineas> GetAerolineas()
        {
            List<Aerolineas> a = new List<Aerolineas>
            {
                new Aerolineas { aerolinea = "Aserca" },
                new Aerolineas { aerolinea = "Conviasa" },
                new Aerolineas { aerolinea = "Laser" },
                new Aerolineas { aerolinea = "Venezolana" },
                new Aerolineas { aerolinea = "Avior" }
            };
            return a;
        }

    }
}
