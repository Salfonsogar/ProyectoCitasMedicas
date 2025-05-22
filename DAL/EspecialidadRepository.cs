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
        public override async Task<string> Agregar(Especialidad entity)
        {
            string idEspecialidad = await EjecutarSentenciaDB($"INSERT INTO especialidades (nombre) VALUES ('{entity.NombreCompleto}') RETURNING id_especialidad;");
            return $"La especialidad con el ID {idEspecialidad} fue agregado exitosamente";
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

        public async override Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string idEliminado = await EjecutarSentenciaDB($"DELETE FROM especialidades WHERE id_especialidad = {id} RETURNING id_especialidad;");
            return $"La especialidad con el ID {id} fue eliminada exitosamente";
        }

        public override Especialidad Mappear(NpgsqlDataReader reader)
        {
            Especialidad espe = new Especialidad();
            espe.Id = (int)reader["id_especialidad"];
            espe.NombreCompleto = (string)reader["nombre_completo"];

            return espe;
        }

        public override async Task<string> Modificar(Especialidad entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La persona no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
            {
                return "El nombre del medico no puede estar vacío";
            }

            string id_especialidad = await EjecutarSentenciaDB($"UPDATE especialidades SET nombre = '{entity.NombreCompleto}' WHERE id_especialidad = {entity.Id} RETURNING id_especialidad;");
            return $"La especialidad con el ID {id_especialidad} fue modificada exitosamente";
        }
    }
}