using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CitaMedicaRepository : Repository<CitaMedica>
    {
        public override List<CitaMedica> Consultar()
        {
            string sentencia = "SELECT cm.id_cita, pp.nombre_completo, Pm.nombre_completo, hc.fecha_hora, hc.hora_fin, cm.estado FROM CITAS_MEDICAS CM JOIN MEDICOS M ON m.id_medico = cm.id_medico JOIN PERSONAS Pm ON m.ID_PERSONA = Pm.ID_PERSONA JOIN PACIENTES P ON p.id_paciente = cm.id_paciente JOIN PERSONAS pp ON p.ID_PERSONA = pp.ID_PERSONA JOIN HORARIOS_CITAS hc ON hc.id_horario_cita = cm.id_horario_cita;\r\n";
            List<CitaMedica> listaM = new List<CitaMedica>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaM.Add(Mappear(reader));
            }
            CerrarConexion();
            return listaM;
        }

        public override CitaMedica Mappear(NpgsqlDataReader reader)
        {
            CitaMedica cm = new CitaMedica();

            cm.Id = (int)reader["id_cita"];

            Paciente paciente = new Paciente();
            paciente.NombreCompleto = (string)reader["nombre_completo"];
            Medico medico = new Medico();
            medico.NombreCompleto = (string)reader["nombre_completo"];
            HorarioCitaMedica hcm = new HorarioCitaMedica();
            hcm.FechaHora = (DateTime)reader["fecha_hora"];
            hcm.HoraFin = (TimeSpan)reader["hora_fin"];
            cm.Estado = (string)reader["estado"];

            cm.paciente = paciente;
            cm.medico = medico;
            cm.horariocm = hcm;

            return cm;

        }

        public override async Task<string> Agregar(CitaMedica entity)
        {
            string idCitaMedica = await EjecutarSentenciaDB($"INSERT INTO citas_medicas (id_medico, id_paciente, id_horario_cita, estado) VALUES ({entity.medico.IdMedico}," +
                $" {entity.paciente.IdPaciente}, {entity.horariocm.Id}, '{entity.Estado}')" +
                $"RETURNING id_cita;");
            return $"La cita con el ID {idCitaMedica} fue agregado exitosamente";
        }

        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string idEliminado = await EjecutarSentenciaDB($"DELETE FROM citas_medicas WHERE id_cita = {id} RETURNING id_cita;");
            return $"La cita con el ID {idEliminado} fue eliminado exitosamente";
        }

        public override async Task<string> Modificar(CitaMedica entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La cita no puede ser nula");
            }

            string idCitaMedica = await EjecutarSentenciaDB($"UPDATE citas_medicas SET id_medico = '{entity.medico.IdMedico}', id_paciente = '{entity.paciente.IdPaciente}', id_horario_cita = '{entity.horariocm.Id}' WHERE id_cita = {entity.Id} RETURNING id_cita;");
            return $"La cita con el ID {idCitaMedica} fue modificada exitosamente";
        }

        public async Task<string> CancelarCita(CitaMedica entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La cita no puede ser nula");
            }
            string idCitaMedicaCancelada = await EjecutarSentenciaDB($"UPDATE CITAS_MEDICAS SET estado = 'Cancelada' WHERE id_cita = {entity.Id} RETURNING id_cita; ");
            return $"La cita con el ID {idCitaMedicaCancelada} fue cancelada exitosamente";
        }
    }
}