using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public abstract class GenericService<T, TRepository> : IService<T>
           where T : class
           where TRepository : IRepository<T>
    {
        protected readonly TRepository repository;

        public GenericService(TRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            this.repository = repository;
        }

        public virtual async Task<string> Agregar(T entity)
        {
            return await repository.Agregar(entity);
        }

        public virtual List<T> Consultar()
        {
            try
            {
                var lista = repository.Consultar();

                if (lista == null || lista.Count == 0)
                {
                    Console.WriteLine("No se encontraron registros.");
                    return new List<T>();
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar los datos: {ex.Message}");
                return new List<T>();
            }
        }

        public virtual async Task<string> Modificar(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return "Error: La entidad no puede ser nula.";
                }

                string resultado = await repository.Modificar(entity);

                if (string.IsNullOrWhiteSpace(resultado))
                {
                    return "No se pudo modificar la entidad, resultado vacío.";
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Modificar: {ex.Message}");
                return $"Error inesperado al modificar: {ex.Message}";
            }
        }


        public virtual async Task<string> Eliminar(int id)
        {
            try
            {
                string resultado = await repository.Eliminar(id);

                if (string.IsNullOrWhiteSpace(resultado))
                {
                    return "No se pudo modificar la entidad, resultado vacío.";
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Modificar: {ex.Message}");

                return $"Error inesperado al modificar: {ex.Message}";
            }
        }
    }
}
