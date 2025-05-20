using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CitaMedica : BaseEntity
    {
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public int IdHorarioCita { get; set; }
        public string Estado { get; set; }
    }
}
