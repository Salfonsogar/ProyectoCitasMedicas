using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HistoriaClinicaRepository : Repository<HistoriaClinica>
    {
        public override List<HistoriaClinica> Consultar()
        {
            string sentencia = "SELECT hc.id_historia_clinica, hc.id_paciente, hc.motivo_consulta, hc.descripcion, hc.evolucion, hc.causa_externa, hc.signos_vitales," +
                " hc.examenes_fisicos, hc.diagnosticos FROM historias_clinicas hc JOIN pacientes p ON hc.id_paciente = p.id_paciente;";
            List<HistoriaClinica> listaHC = new List<HistoriaClinica>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaHC.Add(Mappear(reader));
            }
            CerrarConexion();
            return listaHC;
        }

        public override HistoriaClinica Mappear(NpgsqlDataReader reader)
        {
            HistoriaClinica hc = new HistoriaClinica();
            hc.Id = (int)reader["id_historia_clinica"];
            hc.IdPaciente = (int)reader["id_paciente"];
            hc.MotivoConsulta = (string)reader["motivo_consulta"];
            hc.Descripcion = (string)reader["descripcion"];
            hc.Evolucion = (string)reader["evolucion"];
            hc.CausaExterna = (string)reader["causa_externa"];
            hc.SignosVitales = (string)reader["signos_vitales"];
            hc.ExamenFisico = (string)reader["examenes_fisicos"];
            hc.Diagnostico = (string)reader["diagnosticos"];
            return hc;
        }

        public override async Task<string> Agregar(HistoriaClinica entity)
        {
            string idHistoriaClinica = await EjecutarSentenciaDB($"INSERT INTO historias_clinicas (id_paciente, motivo_consulta, descripcion, evolucion, causa_externa, signos_vitales, diagnosticos, examenes_fisicos) VALUES ({entity.IdPaciente}," +
                $" '{entity.MotivoConsulta}', '{entity.Descripcion}', '{entity.Evolucion}', '{entity.CausaExterna}', '{entity.SignosVitales}','{entity.Diagnostico}', '{entity.ExamenFisico}')" +
                $"RETURNING id_historia_clinica;");
            return $"La historia clinica con el ID {idHistoriaClinica} fue agregado exitosamente";
        }

        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string idEliminado = await EjecutarSentenciaDB($"DELETE FROM historias_clinicas WHERE id_historia_clinica = {id} RETURNING id_historia_clinica;");
            return $"La historia clinica con el ID {idEliminado} fue eliminado exitosamente";
        }

        public override async Task<string> Modificar(HistoriaClinica entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La cita no puede ser nula");
            }

            string idHistoriaClinica = await EjecutarSentenciaDB($"UPDATE historias_clinicas SET motivo_consulta = '{entity.MotivoConsulta}', descripcion = '{entity.Descripcion}', evolucion = '{entity.Evolucion}'," +
                $" causa_externa = '{entity.CausaExterna}', signos_vitales = '{entity.SignosVitales}', diagnosticos = '{entity.Diagnostico}', examenes_fisicos = '{entity.ExamenFisico}' WHERE id_historia_clinica = RETURNING id_historia_clinica;");
            return $"La historia clinica con el ID {idHistoriaClinica} fue modificada exitosamente";
        }
    }
}
