using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HorarioMedico : Horario
    {
        public override TimeSpan HoraInicio { get; set; }
        public override TimeSpan HoraFin { get; set; }

        public string Descripcion => $"{HoraInicio:hh\\:mm} - {HoraFin:hh\\:mm}";
    }
}
