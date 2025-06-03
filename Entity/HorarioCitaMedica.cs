using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HorarioCitaMedica : Horario
    {
        public DateTime FechaHora { get; set; }
        public TimeSpan Duracion { get; set; }
        public override TimeSpan HoraInicio
        {
            get => FechaHora.TimeOfDay;
            set => FechaHora = FechaHora.Date + value;
        }

        public override TimeSpan HoraFin
        {
            get => FechaHora.TimeOfDay + Duracion;
            set => Duracion = value - FechaHora.TimeOfDay;
        }

    }
}