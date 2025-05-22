using Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class Repository<T> : BaseDatos, IRepository<T>
    {
        public async Task<string> EjecutarSentenciaDB(string query)
        {
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexion))
                {

                    AbrirConexion();

                    var idGenerado = cmd.ExecuteScalar();

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

        public string transformarDateTimeADate(DateTime fecha)
        {
            return fecha.ToString("yyyy-MM-dd");
        }

        public string transformarDateTimeATimeStamp(DateTime fecha)
        {
            return fecha.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public abstract List<T> Consultar();
        public abstract T Mappear(NpgsqlDataReader reader);


        public string GenerarReporteErrores(string errorMessage)
        {
            if (errorMessage.Contains("duplicate key") || errorMessage.Contains("violates unique constraint"))
            {
                return "Error: Hay valores repetidos que deben ser únicos.";
            }
            else if (errorMessage.Contains("null value") || errorMessage.Contains("violates not-null constraint"))
            {
                return "Error: Valor nulo no permitido en campo requerido.";
            }
            else if (errorMessage.Contains("value too long"))
            {
                return "Error: El valor es demasiado largo para el campo.";
            }
            else if (errorMessage.Contains("relation") && errorMessage.Contains("does not exist"))
            {
                return "Error: Tabla o columna no existente. Revise la query y/o vuelva a ejecutar DDL";
            }

            return errorMessage;
        }

        public abstract Task<string> Agregar(T entity);
        public abstract Task<string> Modificar(T entity);
        public abstract Task<string> Eliminar(int id);
    }
}