using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServicePaciente : GenericPersonaService<Paciente, PacienteRepository>
    {
        public ServicePaciente(PacienteRepository repository) : base(repository)
        {
        }
    }
}