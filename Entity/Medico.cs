using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Medico : Persona
    {
        public int IdMedico { get; set; }
        public int IdEspecialidad { get; set; }
        public int IdHorarioMedico { get; set; }
        public List<CitaMedica> CitaMedicas { get; set; } 
        public List<HorarioCitaMedica> CitasProgramadas { get; set; } 
    }
}
