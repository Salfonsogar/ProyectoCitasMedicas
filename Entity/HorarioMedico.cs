using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HorarioMedico : Horario
    {
        public override TimeSpan HoraInicio { get; }
        public override TimeSpan HoraFin { get; }
    }
}
