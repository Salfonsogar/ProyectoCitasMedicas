using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HorarioMedicoRepository : Repository<HorarioMedico>
    {
        public override List<HorarioMedico> Consultar()
        {
            string sentencia = "SELECT id_horario_medico, hora_inicio, hora_fin FROM horarios_medicos;";
            List<HorarioMedico> listaE = new List<HorarioMedico>();
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

        public override HorarioMedico Mappear(NpgsqlDataReader reader)
        {
            HorarioMedico hm = new HorarioMedico();
            hm.Id = (int)reader["id_horario_medico"];
            hm.HoraInicio = (TimeSpan)reader["hora_inicio"];
            hm.HoraFin = (TimeSpan)reader["hora_fin"];
            return hm;
        }

        public override async Task<string> Agregar(HorarioMedico entity)
        {
            string idHorarioMedico = await EjecutarSentenciaDB($"INSERT INTO horarios_medicos (hora_inicio, hora_fin) VALUES ('{entity.HoraInicio}', '{entity.HoraFin}') " +
                $"RETURNING id_horario_medico;");
            return $"El horario con el ID {idHorarioMedico} fue agregado exitosamente";
        }

        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string idEliminado = await EjecutarSentenciaDB($"DELETE FROM horarios_medicos WHERE id_horario_medico = {id} RETURNING id_horario_medico;");
            return $"El horario con el ID {idEliminado} fue eliminado exitosamente";
        }

        public override async Task<string> Modificar(HorarioMedico entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La persona no puede ser nula");
            }

            string idHorarioMedico = await EjecutarSentenciaDB($"UPDATE horarios_medicos SET hora_inicio = '{entity.HoraInicio}', hora_fin = '{entity.HoraFin}' WHERE id_horario_medico = {entity.Id} RETURNING id_horario_medico;");
            return $"El horario con el ID {idHorarioMedico} fue modificada exitosamente";
        }
    }
}
