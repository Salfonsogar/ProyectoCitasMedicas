using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Npgsql;

namespace DAL
{
    public class UsuarioRepository : Repository<Usuario>
    {
        public override List<Usuario> Consultar()
        {
            string sentencia = "SELECT * FROM usuarios;";
            List<Usuario> lista = new List<Usuario>();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(Mappear(reader));
            }

            CerrarConexion();
            return lista;
        }

        public override Usuario Mappear(NpgsqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = (int)reader["id_usuario"];
            usuario.IdPersona = (int)reader["id_persona"];
            usuario.Username = (string)reader["username"];
            usuario.Contraseña = (string)reader["contraseña"];
            usuario.Rol = (string)reader["rol"];
            return usuario;
        }

        public async Task<Usuario> ValidarCredenciales(string username, string contraseña)
        {
            string sentencia = $"SELECT * FROM usuarios WHERE username = '{username}' AND contraseña = '{contraseña}' LIMIT 1;";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            Usuario usuario = null;
            if (await reader.ReadAsync())
            {
                usuario = Mappear(reader);
            }

            CerrarConexion();
            return usuario;
        }

        public string ObtenerNombrePersonaPorIdUsuario(int idUsuario)
        {
            string nombre = null;
            string query = "SELECT p.nombre_completo FROM usuarios u JOIN personas p ON u.id_persona = p.id_persona WHERE u.id_usuario = @idUsuario;";

            using (var cmd = new NpgsqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                AbrirConexion();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nombre = reader.GetString(0);
                    }
                }
                CerrarConexion();
            }

            return nombre;
        }



        public override async Task<string> Agregar(Usuario entity)
        {
            string query = $"INSERT INTO usuarios (username, contraseña, rol, id_persona) VALUES ('{entity.Username}', '{entity.Contraseña}', '{entity.Rol}', {entity.IdPersona}) RETURNING id_usuario;";
            string id_usuario = await EjecutarSentenciaDB(query);   
            return $"El usuario con ID {id_usuario} fue agregado exitosamente";
        }

        public override async Task<string> Modificar(Usuario entity)
        {
            if (entity.Id < 1)
            {
                throw new ArgumentNullException(nameof(entity.Id), "El ID del usuario no puede ser nulo");
            }

            string query = $"UPDATE usuarios SET username = '{entity.Username}', contraseña = '{entity.Contraseña}', rol = '{entity.Rol}' WHERE id_usuario = {entity.Id} RETURNING id_usuario;";
            string id_usuario = await EjecutarSentenciaDB(query);
            return $"El usuario con ID {id_usuario} fue modificado exitosamente";
        }

        public override async Task<string> Eliminar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentNullException(nameof(id), "El ID del usuario no puede ser nulo");
            }

            string id_usuario = await EjecutarSentenciaDB($"DELETE FROM usuarios WHERE id_usuario = {id} RETURNING id_usuario;");
            return $"El usuario con ID {id_usuario} fue eliminado exitosamente";
        }
    }
}
