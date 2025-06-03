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

        public string NombreEspecialidad { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string HorarioDescripcion { get; set; }


        public List<CitaMedica> CitaMedicas { get; set; } 
        public List<DateTime> HorariosCitasProgramadas { get; set; } 
    }
}
