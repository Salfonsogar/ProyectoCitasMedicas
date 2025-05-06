using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Clinica : EntidadSalud
    {
        public List<Medico> Medicos { get; set; }
        public List<Paciente> Pacientes { get; set; }
    }
}
