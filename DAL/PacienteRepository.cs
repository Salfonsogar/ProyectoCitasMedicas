using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Npgsql;
namespace DAL
{
    public class PacienteRepository : BaseDatos, IRepository<Paciente>
    {
        public List<Paciente> Consultar()
        {
            string sentencia = "SELECT p.id_paciente, pr.NOMBRE_COMPLETO, pr.TIPO_DOCUMENTO, pr.NRO_DOCUMENTO, pr.SEXO, pr.EDAD, pr.TELEFONO, pr.CORREO, pr.DIRECCION, pr.FECHA_NACIMIENTO FROM pacientes p JOIN personas pr ON p.ID_PERSONA = pr.ID_PERSONA;";
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

        private Paciente Mappear(NpgsqlDataReader reader)
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


        public async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }

            const string queryPacienteEliminar = "DELETE FROM pacientes WHERE id_paciente = @IdPaciente RETURNING id_persona;";

            try
            {
                using (NpgsqlCommand cmdPaciente = new NpgsqlCommand(queryPacienteEliminar, conexion))
                {
                    cmdPaciente.Parameters.AddWithValue("@IdPaciente", id);
                    AbrirConexion();

                    var idGenerado = cmdPaciente.ExecuteScalar();
                    await EliminarPersona((int)idGenerado);
                    return idGenerado.ToString();
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = ex.Message;
                return GenerarReporteErrores(errorMessage);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public async Task<string> EliminarPersona(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El id no puede ser nula");
            }

            const string queryPersonaEliminar = "DELETE FROM personas WHERE id_persona = @id";

            try
            {
                using (NpgsqlCommand cmdPersona = new NpgsqlCommand(queryPersonaEliminar, conexion))
                {
                    cmdPersona.Parameters.AddWithValue("@id", id);

                    var idGenerado = cmdPersona.ExecuteScalar();
                    return idGenerado.ToString();
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = ex.Message;
                return GenerarReporteErrores(errorMessage);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public async Task<string> AgregarPersona(Paciente entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
            {
                return "El nombre de la persona no puede estar vacío";
            }

            const string queryPersona = "INSERT INTO personas (nombre_completo, tipo_documento, nro_documento, sexo, edad, telefono, correo, direccion, fecha_nacimiento) VALUES (@NombreCompleto, @TipoDocumento, @NroDocumento, @Sexo, @Edad, @Telefono, @Correo, @Direccion, @FechaNacimiento) RETURNING id_persona;";

            try
            {
                using (NpgsqlCommand cmdPersona = new NpgsqlCommand(queryPersona, conexion))
                {
                    cmdPersona.Parameters.AddWithValue("@NombreCompleto", entity.NombreCompleto);
                    cmdPersona.Parameters.AddWithValue("@TipoDocumento", entity.TipoDocumento);
                    cmdPersona.Parameters.AddWithValue("@NroDocumento", entity.NroDocumento);
                    cmdPersona.Parameters.AddWithValue("@Sexo", entity.Sexo);
                    cmdPersona.Parameters.AddWithValue("@Edad", entity.Edad);
                    cmdPersona.Parameters.AddWithValue("@Telefono", entity.Telefono);
                    cmdPersona.Parameters.AddWithValue("@Correo", entity.Correo);
                    cmdPersona.Parameters.AddWithValue("@Direccion", entity.Direccion);
                    cmdPersona.Parameters.AddWithValue("@FechaNacimiento", entity.FechaNacimiento);

                    AbrirConexion();

                    var idGenerado = cmdPersona.ExecuteScalar();

                    return idGenerado.ToString();
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = ex.Message;
                return GenerarReporteErrores(errorMessage);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public async Task<string> Agregar(Paciente entity)
        {

            string idPersona = await AgregarPersona(entity);

            if (idPersona == null)
            {
                throw new ArgumentNullException(nameof(idPersona), "La persona no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(idPersona))
            {
                return "El id de la persona no puede estar vacío";
            }

            const string queryPaciente = "INSERT INTO pacientes (id_persona) VALUES (@IdPersona) RETURNING id_paciente;";

            try
            {
                using (NpgsqlCommand cmdPaciente = new NpgsqlCommand(queryPaciente, conexion))
                {
                    int id = int.Parse(idPersona);
                    cmdPaciente.Parameters.AddWithValue("@IdPersona", id);

                    AbrirConexion();

                    var idGenerado = cmdPaciente.ExecuteScalar();

                    return $"Paciente guardado correctamente con ID: {idGenerado}";
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = ex.Message;

                return GenerarReporteErrores(errorMessage);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public string Modificar(Paciente entity)
        {
            if (entity.Id == null)
            {
                throw new ArgumentNullException(nameof(entity.Id), "La persona no puede ser nula");
            }

            if (string.IsNullOrWhiteSpace(entity.NombreCompleto))
            {
                return "El nombre del paciente no puede estar vacío";
            }
            const string queryPacienteModificar = "UPDATE personas SET nombre_completo = @NombreCompleto, tipo_documento = @TipoDocumento, nro_documento = @NroDocumento, sexo = @Sexo, edad = @Edad, telefono = @Telefono, correo = @Correo, direccion = @Direccion, fecha_nacimiento = @FechaNacimiento WHERE id_persona = @Id RETURNING id_persona;";

            try
            {
                using (NpgsqlCommand cmdPaciente = new NpgsqlCommand(queryPacienteModificar, conexion))
                {
                    cmdPaciente.Parameters.AddWithValue("@Id", int.Parse(entity.Id));
                    cmdPaciente.Parameters.AddWithValue("@NombreCompleto", entity.NombreCompleto);
                    cmdPaciente.Parameters.AddWithValue("@TipoDocumento", entity.TipoDocumento);
                    cmdPaciente.Parameters.AddWithValue("@NroDocumento", entity.NroDocumento);
                    cmdPaciente.Parameters.AddWithValue("@Sexo", entity.Sexo);
                    cmdPaciente.Parameters.AddWithValue("@Edad", entity.Edad);
                    cmdPaciente.Parameters.AddWithValue("@Telefono", entity.Telefono);
                    cmdPaciente.Parameters.AddWithValue("@Correo", entity.Correo);
                    cmdPaciente.Parameters.AddWithValue("@Direccion", entity.Direccion);
                    cmdPaciente.Parameters.AddWithValue("@FechaNacimiento", entity.FechaNacimiento);
                    AbrirConexion();

                    var idGenerado = cmdPaciente.ExecuteScalar();

                    return $"El paciente con ID: {idGenerado} fue modificado correctamente";
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = ex.Message;

                return GenerarReporteErrores(errorMessage);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public string GenerarReporteErrores(string errorMessage)
        {
            if (errorMessage.Contains("duplicate key") || errorMessage.Contains("violates unique constraint"))
            {
                return "Error: El nombre de la especie ya existe (violación de unique).";
            }
            else if (errorMessage.Contains("null value") || errorMessage.Contains("violates not-null constraint"))
            {
                return "Error: Valor nulo no permitido en campo requerido.";
            }
            else if (errorMessage.Contains("value too long"))
            {
                return "Error: El valor es demasiado largo para el campo.";
            }
            else if (errorMessage.Contains("relation \"especies\" does not exist"))
            {
                return "Error: La tabla 'especies' no existe.";
            }

            return errorMessage;
        }
    }
}
