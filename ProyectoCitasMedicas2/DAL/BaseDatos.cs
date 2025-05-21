using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DAL
{
    public class BaseDatos
    {
        string cadenaConexion = "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres";
        protected NpgsqlConnection conexion;

        public BaseDatos()
        {
            conexion = new NpgsqlConnection(cadenaConexion);
            //conexion.ConnectionString = cadenaConexion;
        }

        public string AbrirConexion()
        {
            conexion.Open();
            return conexion.State.ToString();
        }

        public void CerrarConexion()
        {
            conexion.Close();
        }
    }
}

