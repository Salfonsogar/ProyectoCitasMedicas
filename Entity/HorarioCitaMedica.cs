using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HorarioCitaMedica : Horario
    {
        public DateTime FechaHora { get; }
        public TimeSpan Duracion { get; }
        public override TimeSpan HoraInicio => FechaHora.TimeOfDay;
        public override TimeSpan HoraFin => FechaHora.TimeOfDay + Duracion;
    }
}
