using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HorarioCitaMedicaRepository : Repository<HorarioCitaMedica>
    {
        public override List<HorarioCitaMedica> Consultar()
        {
            string sentencia = "SELECT id_horario_cita, fecha_hora, hora_inicio, hora_fin FROM horarios_citas;";
            List<HorarioCitaMedica> listaE = new List<HorarioCitaMedica>();
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
        public override HorarioCitaMedica Mappear(NpgsqlDataReader reader)
        {
            HorarioCitaMedica horariocm = new HorarioCitaMedica();

            horariocm.Id = (int)reader["id_horario_cita"];
            horariocm.FechaHora = (DateTime)reader["fecha_hora"];
            TimeSpan horaInicio = (TimeSpan)reader["hora_inicio"];
            TimeSpan horaFin = (TimeSpan)reader["hora_fin"];
            return horariocm;
        }

        public override async Task<string> Agregar(HorarioCitaMedica entity)
        {
            string idHorarioCitaMedica = await EjecutarSentenciaDB($"INSERT INTO horarios_citas (fecha_hora, hora_inicio, hora_fin) VALUES ('{transformarDateTimeATimeStamp(entity.FechaHora)}', '{entity.HoraInicio}', '{entity.HoraFin}')" +
                $"RETURNING id_horario_cita;");
            return $"El horario con el ID {idHorarioCitaMedica} fue agregado exitosamente";
        }

        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string idEliminado = await EjecutarSentenciaDB($"DELETE FROM horarios_citas WHERE id_horario_cita = {id} RETURNING id_horario_cita;");
            return $"El horario con el ID {idEliminado} fue eliminado exitosamente";
        }

        
        public override async Task<string> Modificar(HorarioCitaMedica entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "El horario no puede ser nulo");
            }

            string idHorarioCitaMedica = await EjecutarSentenciaDB($"UPDATE horarios_citas SET fecha_hora = '{transformarDateTimeATimeStamp(entity.FechaHora)}', hora_inicio = '{entity.HoraInicio}', hora_fin = '{entity.HoraFin}' WHERE id_horario_cita = {entity.Id} RETURNING id_horario_cita;");
            return $"El horario con el ID {idHorarioCitaMedica} fue modificada exitosamente";
        }
    }
}
