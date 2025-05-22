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
            string sentencia = "SELECT m.ID_MEDICO, pr.NOMBRE_COMPLETO, e.ID_ESPECIALIDAD, hm.ID_HORARIO_MEDICO, pr.TIPO_DOCUMENTO, pr.NRO_DOCUMENTO, pr.SEXO, pr.EDAD, pr.TELEFONO, pr.CORREO, pr.DIRECCION, pr.FECHA_NACIMIENTO FROM medicos m JOIN personas pr ON m.ID_PERSONA = pr.ID_PERSONA JOIN especialidades e ON m.ID_ESPECIALIDAD  = e.ID_ESPECIALIDAD JOIN horarios_medicos hm ON m.ID_HORARIO_MEDICO = hm.ID_HORARIO_MEDICO;";
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
            medico.IdMedico = (int)reader["id_medico"];
            medico.NombreCompleto = (string)reader["nombre_completo"];
            medico.IdEspecialidad = (int)reader["id_especialidad"];
            medico.IdHorarioMedico = (int)reader["id_horario_medico"];
            medico.TipoDocumento = (string)reader["tipo_documento"];
            medico.NroDocumento = (int)reader["nro_documento"];
            medico.Sexo = ((string)reader["sexo"])[0];
            medico.Edad = (int)reader["edad"];
            medico.Telefono = (string)reader["telefono"];
            medico.Correo = (string)reader["correo"];
            medico.Direccion = (string)reader["direccion"];
            medico.FechaNacimiento = (DateTime)reader["fecha_nacimiento"];
            medico.IdHorarioMedico = (int)reader["id_horario_medico"];

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
    }
}