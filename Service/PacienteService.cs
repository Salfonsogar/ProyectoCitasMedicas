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
    }
}
