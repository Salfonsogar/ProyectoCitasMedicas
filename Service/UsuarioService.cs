using System;
using System.Collections.Generic;
using DAL;
using Entity;

namespace Service
{
    public class UsuarioService
    {
        private UsuarioRepository repository;

        public UsuarioService()
        {
            repository = new UsuarioRepository();
        }

        public string Registrar(Usuario usuario)
        {
            try
            {
                return repository.Agregar(usuario).Result;
            }
            catch (Exception ex)
            {
                return $"Error al registrar el usuario: {ex.Message}";
            }
        }

        public string Modificar(Usuario usuario)
        {
            try
            {
                return repository.Modificar(usuario).Result;
            }
            catch (Exception ex)
            {
                return $"Error al modificar el usuario: {ex.Message}";
            }
        }

        public string Eliminar(int id)
        {
            try
            {
                return repository.Eliminar(id).Result;
            }
            catch (Exception ex)
            {
                return $"Error al eliminar el usuario: {ex.Message}";
            }
        }

        public List<Usuario> Consultar()
        {
            return repository.Consultar();
        }

        public Usuario ValidarCredenciales(string username, string password)
        {
            try
            {
                var usuarios = repository.Consultar();
                foreach (var usuario in usuarios)
                {
                    if (usuario.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                        usuario.Contraseña == password)
                    {
                        return usuario;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


    }
}
