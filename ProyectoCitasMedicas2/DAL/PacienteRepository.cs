using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Npgsql;
namespace DAL
{
    public class PacienteRepository : Repository<Paciente>
    {
        public override List<Paciente> Consultar()
        {
            string sentencia = "SELECT p.id_paciente, pr.NOMBRE_COMPLETO, pr.TIPO_DOCUMENTO, pr.NRO_DOCUMENTO, pr.SEXO, pr.EDAD," +
                " pr.TELEFONO, pr.CORREO, pr.DIRECCION, pr.FECHA_NACIMIENTO FROM pacientes p JOIN " +
                "personas pr ON p.ID_PERSONA = pr.ID_PERSONA;";
            List<Paciente> listaP = new List<Paciente>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaP.Add(Mappear(reader));
            }
            CerrarConexion();
            return listaP;
        }

        public override Paciente Mappear(NpgsqlDataReader reader)
        {
            Paciente paciente = new Paciente();
            paciente.IdPaciente = (int)reader["id_paciente"];
            paciente.NombreCompleto = (string)reader["nombre_completo"];
            paciente.TipoDocumento = (string)reader["tipo_documento"];
            paciente.NroDocumento = (int)reader["nro_documento"];
            paciente.Sexo = ((string)reader["sexo"])[0];
            paciente.Edad = (int)reader["edad"];
            paciente.Telefono = (string)reader["telefono"];
            paciente.Correo = (string)reader["correo"];
            paciente.Direccion = (string)reader["direccion"];
            paciente.FechaNacimiento = (DateTime)reader["fecha_nacimiento"];

            return paciente;
        }

        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }
            string id_paciente = await EjecutarSentenciaDB($"DELETE FROM pacientes WHERE id_paciente = {id} RETURNING id_persona;");
            await EjecutarSentenciaDB($"DELETE FROM personas WHERE id_persona = {id_paciente} RETURNING id_paciente;");
            return $"El paciente con el ID {id} fue eliminado exitosamente";
        }

        public override async Task<string> Agregar(Paciente entity)
        {
            string id_persona = await EjecutarSentenciaDB($"INSERT INTO personas (nombre_completo, tipo_documento, nro_documento, sexo, edad, telefono, correo, direccion, fecha_nacimiento) VALUES ('{entity.NombreCompleto}', '{entity.TipoDocumento}',{entity.NroDocumento},'{entity.Sexo}',{entity.Edad},'{entity.Telefono}','{entity.Correo}','{entity.Direccion}','{entity.FechaNacimiento.ToString("yyyy-MM-dd")}') RETURNING id_persona;");
            string id_paciente = await EjecutarSentenciaDB($"INSERT INTO pacientes (id_persona) VALUES ({id_persona}) RETURNING id_paciente;");

            return $"El paciente con el ID {id_paciente} fue agregado exitosamente";
        }
        public override async Task<string> Modificar(Paciente entity)
        {
            if (entity.Id == null)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La persona no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
            {
                return "El nombre del paciente no puede estar vacío";
            }
            string id_paciente = await EjecutarSentenciaDB($"UPDATE personas SET nombre_completo = '{entity.NombreCompleto}', tipo_documento = '{entity.TipoDocumento}', nro_documento = {entity.NroDocumento}, sexo = '{entity.Sexo}', edad = {entity.Edad}, telefono = '{entity.Telefono}', correo = '{entity.Correo}', direccion = '{entity.Direccion}', fecha_nacimiento = '{entity.FechaNacimiento.ToString("yyyy-MM-dd")}' WHERE id_persona = {entity.Id} RETURNING id_persona;");
            return $"El paciente con el ID {id_paciente} fue modificado exitosamente";
        }
    }
}
