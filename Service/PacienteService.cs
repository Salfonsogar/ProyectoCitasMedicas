using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PacienteService : GenericPersonaService<Paciente,PacienteRepository>
    {
        public PacienteService(PacienteRepository repository) : base(repository)
        {
        }

        public int ObtenerIdPaciente(int identificacion)
        {
            try
            {
                var lista = repository.Consultar();
                if (lista == null || lista.Count == 0)
                {
                    Console.WriteLine("No se encontraron registros.");
                    return -1;
                }
                var paciente = lista.FirstOrDefault(p => p.NroDocumento == identificacion);
                if (paciente != null)
                {
                    return paciente.IdPaciente;
                }
                else
                {
                    Console.WriteLine("No se encontró la persona con la identificación proporcionada.");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar los datos: {ex.Message}");
                return -1;
            }
        }
    }
}
