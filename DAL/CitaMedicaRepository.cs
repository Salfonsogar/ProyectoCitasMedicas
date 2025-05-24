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
            string sentencia = "SELECT cm.id_cita, p.id_paciente, pp.nombre_completo AS nombre_paciente, m.id_medico,m.id_especialidad, pm.nombre_completo AS nombre_medico, cm.fecha_hora, cm.estado FROM citas_medicas cm JOIN medicos m ON m.id_medico = cm.id_medico JOIN personas pm ON m.id_persona = pm.id_persona JOIN pacientes p ON p.id_paciente = cm.id_paciente JOIN personas pp ON p.id_persona = pp.id_persona;";
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
            paciente.NombreCompleto = (string)reader["nombre_paciente"];
            cm.IdPaciente = (int)reader["id_paciente"];
            Medico medico = new Medico();
            medico.NombreCompleto = (string)reader["nombre_medico"];
            medico.IdEspecialidad = (int)reader["id_especialidad"];
            cm.IdMedico = (int)reader["id_medico"];
            cm.Fecha = (DateTime)reader["fecha_hora"];
            cm.Estado = (string)reader["estado"];

            cm.paciente = paciente;
            cm.medico = medico;

            return cm;

        }

        public override async Task<string> Agregar(CitaMedica entity)
        {
            string idCitaMedica = await EjecutarSentenciaDB($"INSERT INTO citas_medicas (id_medico, id_paciente, fecha_hora, estado) VALUES ({entity.medico.IdMedico}," +
                $" {entity.paciente.IdPaciente}, '{transformarDateTimeATimeStamp(entity.Fecha)}', '{entity.Estado}')" +
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

            string idCitaMedica = await EjecutarSentenciaDB($"UPDATE citas_medicas SET id_medico = '{entity.medico.IdMedico}', id_paciente = '{entity.paciente.IdPaciente}', fecha_hora = '{entity.Fecha.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE id_cita = {entity.Id} RETURNING id_cita;");
            return $"La cita con el ID {idCitaMedica} fue modificada exitosamente";
        }

        public async Task<string> CancelarCita(CitaMedica entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La cita no puede ser nula");
            }
            string idCitaMedicaCancelada = await EjecutarSentenciaDB($"UPDATE citas_medicas SET estado = 'Cancelada' WHERE id_cita = {entity.Id} RETURNING id_cita; ");
            return $"La cita con el ID {idCitaMedicaCancelada} fue cancelada exitosamente";
        }
    }
}