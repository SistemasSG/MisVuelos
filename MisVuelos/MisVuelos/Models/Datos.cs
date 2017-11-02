using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisVuelos.Models
{
    public class Vuelos
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string aerolinea { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public int? asientos { get; set; }
        public int? asientos_dis { get; set; }
        public DateTime? fecha { get; set; }
        public decimal precio { get; set; }
    }

    public class Clientes
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nombre { get; set; }
    }

    public class Reservaciones
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string reserva { get; set; }
        public int id_vuelo { get; set; }
        public int id_cliente { get; set; }
        public int asientos { get; set; }
    }

}
