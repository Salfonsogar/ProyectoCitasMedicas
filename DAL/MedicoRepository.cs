using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Npgsql;

namespace DAL
{
    public class MedicoRepository : Repository<Medico>
    {

        public override List<Medico> Consultar()
        {
            // string sentencia = "SELECT m.ID_MEDICO, pr.NOMBRE_COMPLETO, e.ID_ESPECIALIDAD, hm.ID_HORARIO_MEDICO, pr.TIPO_DOCUMENTO, pr.NRO_DOCUMENTO, pr.SEXO FROM medicos m JOIN personas pr ON m.ID_PERSONA = pr.ID_PERSONA JOIN especialidades e ON m.ID_ESPECIALIDAD  = e.ID_ESPECIALIDAD JOIN horarios_medicos hm ON m.ID_HORARIO_MEDICO = hm.ID_HORARIO_MEDICO;";
            string sentencia = "SELECT m.id_medico, pr.id_persona, pr.nombre_completo, pr.tipo_documento, pr.nro_documento, pr.sexo, pr.edad, pr.telefono, pr.correo, pr.direccion, pr.fecha_nacimiento, e.id_especialidad, e.nombre AS nombre_especialidad, hm.id_horario_medico, hm.hora_inicio, hm.hora_fin FROM medicos m JOIN personas pr ON m.id_persona = pr.id_persona JOIN especialidades e ON m.id_especialidad = e.id_especialidad JOIN horarios_medicos hm ON m.id_horario_medico = hm.id_horario_medico;";
            List<Medico> listaM = new List<Medico>();
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

        public override Medico Mappear(NpgsqlDataReader reader)
        {
            Medico medico = new Medico();

            medico.Id = (int)reader["id_persona"];
            medico.IdMedico = (int)reader["id_medico"];
            medico.NombreCompleto = (string)reader["nombre_completo"];
            medico.TipoDocumento = (string)reader["tipo_documento"];
            medico.NroDocumento = (int)reader["nro_documento"];
            medico.Sexo = ((string)reader["sexo"])[0];
            medico.Edad = (int)reader["edad"];
            medico.Telefono = (string)reader["telefono"];
            medico.Correo = (string)reader["correo"];
            medico.Direccion = (string)reader["direccion"];
            medico.FechaNacimiento = (DateTime)reader["fecha_nacimiento"];

            medico.IdEspecialidad = (int)reader["id_especialidad"];
            medico.NombreEspecialidad = (string)reader["nombre_especialidad"];

            medico.IdHorarioMedico = (int)reader["id_horario_medico"];
            medico.HoraInicio = (TimeSpan)reader["hora_inicio"];
            medico.HoraFin = (TimeSpan)reader["hora_fin"];

            medico.NombreEspecialidad = (string)reader["nombre_especialidad"];
            medico.HorarioDescripcion = $"{reader["hora_inicio"]:hh\\:mm} - {reader["hora_fin"]:hh\\:mm}";

            return medico;
        }


        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string id_medico = await EjecutarSentenciaDB($"DELETE FROM medicos WHERE id_medico = {id} RETURNING id_persona;");
            await EjecutarSentenciaDB($"DELETE FROM personas WHERE id_persona = {id_medico} RETURNING id_paciente;");
            return $"El medico con el ID {id} fue eliminado exitosamente";
        }
        public override async Task<string> Agregar(Medico entity)
        {
            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
            {
                return "El nombre del medico no puede estar vacío";
            }
            string id_persona = await EjecutarSentenciaDB($"INSERT INTO personas (nombre_completo, tipo_documento, nro_documento, sexo) VALUES ('{entity.NombreCompleto}', '{entity.TipoDocumento}',{entity.NroDocumento},'{entity.Sexo}',{entity.Edad}') RETURNING id_persona;");
            string id_medico = await EjecutarSentenciaDB($"INSERT INTO medicos (id_persona,id_especialidad,id_horario_medico) VALUES ({id_persona},{entity.IdEspecialidad},{entity.IdHorarioMedico}) RETURNING id_medico;");
            return $"El medico con el ID {id_medico} fue agregado exitosamente";
        }
        public async Task<string> Agregar(Medico entityM, string especialidadId, string horarioMedicoId)
        {
            string id_persona = await EjecutarSentenciaDB($"INSERT INTO personas VALUES ('{entityM.NombreCompleto}', '{entityM.TipoDocumento}',{entityM.NroDocumento},'{entityM.Sexo}',{entityM.Edad},'{entityM.Telefono}','{entityM.Correo}','{entityM.Direccion}','{transformarDateTimeADate(entityM.FechaNacimiento)}') RETURNING id_persona;");
            string id_medico = await EjecutarSentenciaDB($"INSERT INTO medicos (id_persona,id_especialidad,id_horario_medico) VALUES ({id_persona},{especialidadId},{horarioMedicoId}) RETURNING id_medico;");

            return $"El medico con el ID {id_medico} fue agregado exitosamente";
        }

        public override async Task<string> Modificar(Medico entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La persona no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
            {
                return "El nombre del medico no puede estar vacío";
            }
            string id_medico = await EjecutarSentenciaDB($"UPDATE personas SET nombre_completo = '{entity.NombreCompleto}', tipo_documento = '{entity.TipoDocumento}', nro_documento = {entity.NroDocumento}, sexo = '{entity.Sexo}', edad = {entity.Edad}, telefono = '{entity.Telefono}', correo = '{entity.Correo}', direccion = '{entity.Direccion}', fecha_nacimiento = '{entity.FechaNacimiento.ToString("yyyy-MM-dd")}' WHERE id_persona = {entity.Id} RETURNING id_persona;");
            string modificado = await EjecutarSentenciaDB($"UPDATE medicos SET id_especialidad = '{entity.IdEspecialidad}', id_horario_medico = '{entity.IdHorarioMedico}' WHERE id_medico = {id_medico} RETURNING id_medico;");
            return $"El medico con el ID {id_medico} fue modificado exitosamente";
        }

        public bool ExisteMedicoPorIdPersona(int idPersona)
        {
            string query = $"SELECT COUNT(*) FROM medicos WHERE id_persona = {idPersona};";
            NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
            AbrirConexion();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            CerrarConexion();
            return count > 0;
        }

        public int ObtenerIdPersonaPorDocumento(string nroDocumento)
        {
            string query = $"SELECT id_persona FROM personas WHERE nro_documento = '{nroDocumento}' LIMIT 1;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
            AbrirConexion();
            var result = cmd.ExecuteScalar();
            CerrarConexion();
            return Convert.ToInt32(result);
        }

        public int ObtenerIdMedicoPorIdPersona(int idPersona)
        {
            int idMedico = 0;
            string query = $"SELECT id_medico FROM medicos WHERE id_persona = {idPersona};";
            NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
            AbrirConexion();
            var result = cmd.ExecuteScalar();
            if (result != null)
                idMedico = Convert.ToInt32(result);
            CerrarConexion();
            return idMedico;
        }

    }
}