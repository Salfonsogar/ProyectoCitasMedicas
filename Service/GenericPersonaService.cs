using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;

namespace Service
{
    public abstract class GenericPersonaService<T, R> : GenericService<T, R>
    where T : Persona
    where R : IRepository<T>
    {
        public GenericPersonaService(R repository) : base(repository) {
        }

        public virtual string ConsultarNombre(int identificacion)
        {
            try
            {
                var lista = repository.Consultar();
                if (lista == null || lista.Count == 0)
                {
                    Console.WriteLine("No se encontraron registros.");
                    return null;
                }

                var persona = lista.FirstOrDefault(p => p.NroDocumento == identificacion);
                if (persona != null)
                {
                    return persona.NombreCompleto;
                }
                else
                {
                    Console.WriteLine("No se encontró la persona con la identificación proporcionada.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar los datos: {ex.Message}");
                return null;
            }
        }

        public virtual T ConsultarIdentificacion(int identificacion)
        {
            try
            {
                var lista = repository.Consultar();
                if (lista == null || lista.Count == 0)
                {
                    Console.WriteLine("No se encontraron registros.");
                    return null;
                }

                var persona = lista.FirstOrDefault(p => p.NroDocumento == identificacion);
                if (persona != null)
                {
                    return persona;
                }
                else
                {
                    Console.WriteLine("No se encontró la persona con la identificación proporcionada.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar los datos: {ex.Message}");
                return null;
            }
        }
    }
}
