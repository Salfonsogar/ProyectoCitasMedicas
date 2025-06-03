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
            string sentencia = "SELECT hc.id_historia_clinica, hc.id_paciente, per.nombre_completo AS nombre_paciente, hc.id_medico, mp.nombre_completo AS nombre_medico, hc.motivo_consulta, hc.descripcion, hc.evolucion, hc.causa_externa, hc.signos_vitales, hc.examenes_fisicos, hc.diagnosticos FROM historias_clinicas hc JOIN pacientes p ON hc.id_paciente = p.id_paciente JOIN personas per ON p.id_persona = per.id_persona JOIN medicos m ON hc.id_medico = m.id_medico JOIN personas mp ON m.id_persona = mp.id_persona;";


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
            hc.NombrePaciente = reader["nombre_paciente"].ToString();
            hc.IdMedico = (int)reader["id_medico"];
            hc.NombreMedico = reader["nombre_medico"].ToString();
            hc.MotivoConsulta = reader["motivo_consulta"].ToString();
            hc.Descripcion = reader["descripcion"].ToString();
            hc.Evolucion = reader["evolucion"].ToString();
            hc.CausaExterna = reader["causa_externa"].ToString();
            hc.SignosVitales = reader["signos_vitales"].ToString();
            hc.ExamenFisico = reader["examenes_fisicos"].ToString();
            hc.Diagnostico = reader["diagnosticos"].ToString();
            return hc;
        }


        public override async Task<string> Agregar(HistoriaClinica entity)
        {
            string idHistoriaClinica = await EjecutarSentenciaDB(
                $"INSERT INTO historias_clinicas (id_paciente, id_medico, motivo_consulta, descripcion, evolucion, causa_externa, signos_vitales, diagnosticos, examenes_fisicos) " +
                $"VALUES ({entity.IdPaciente}, {entity.IdMedico}, '{entity.MotivoConsulta}', '{entity.Descripcion}', '{entity.Evolucion}', '{entity.CausaExterna}', " +
                $"'{entity.SignosVitales}', '{entity.Diagnostico}', '{entity.ExamenFisico}') RETURNING id_historia_clinica;");

            return $"La historia clínica con el ID {idHistoriaClinica} fue agregada exitosamente.";
        }


        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
                throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo");

            string idEliminado = await EjecutarSentenciaDB(
                $"DELETE FROM historias_clinicas WHERE id_historia_clinica = {id} RETURNING id_historia_clinica;");

            return $"La historia clínica con el ID {idEliminado} fue eliminada exitosamente.";
        }


        public override async Task<string> Modificar(HistoriaClinica entity)
        {
            if (entity.Id < 1)
                throw new ArgumentNullException(nameof(entity.Id), "El ID de la historia clínica no puede ser nulo");

            string idHistoriaClinica = await EjecutarSentenciaDB(
                $"UPDATE historias_clinicas SET motivo_consulta = '{entity.MotivoConsulta}', descripcion = '{entity.Descripcion}', evolucion = '{entity.Evolucion}', " +
                $"causa_externa = '{entity.CausaExterna}', signos_vitales = '{entity.SignosVitales}', diagnosticos = '{entity.Diagnostico}', " +
                $"examenes_fisicos = '{entity.ExamenFisico}' WHERE id_historia_clinica = {entity.Id} RETURNING id_historia_clinica;");

            return $"La historia clínica con el ID {idHistoriaClinica} fue modificada exitosamente.";
        }

        public List<HistoriaClinica> ConsultarPorMedico(int idMedico)
        {
            string sentencia = $"SELECT hc.id_historia_clinica, hc.id_paciente, per.nombre_completo AS nombre_paciente, hc.id_medico, mp.nombre_completo AS nombre_medico, hc.motivo_consulta, hc.descripcion, hc.evolucion, hc.causa_externa, hc.signos_vitales, hc.examenes_fisicos, hc.diagnosticos FROM historias_clinicas hc JOIN pacientes p ON hc.id_paciente = p.id_paciente JOIN personas per ON p.id_persona = per.id_persona JOIN medicos m ON hc.id_medico = m.id_medico JOIN personas mp ON m.id_persona = mp.id_persona WHERE hc.id_medico = {idMedico};";

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


    }
}