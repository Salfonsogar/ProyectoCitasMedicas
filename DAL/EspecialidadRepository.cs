using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EspecialidadesRepository : Repository<Especialidad>
    {
        public override Task<string> Agregar(Especialidad entity)
        {
            throw new NotImplementedException();
        }

        public override List<Especialidad> Consultar()
        {
            string sentencia = "SELECT id_especialidad, nombre FROM especialidades;";
            List<Especialidad> listaE = new List<Especialidad>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaE.Add(Mappear(reader));
            }
            CerrarConexion();
            return listaE;
        }

        public override Task<string> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public override Especialidad Mappear(NpgsqlDataReader reader)
        {
            Especialidad espe = new Especialidad();
            espe.idEspecialidad = (int)reader["id_especialidad"];
            espe.nombre = (string)reader["nombre_completo"];

            return espe;
        }

        public override Task<string> Modificar(Especialidad entity)
        {
            throw new NotImplementedException();
        }
    }
}
