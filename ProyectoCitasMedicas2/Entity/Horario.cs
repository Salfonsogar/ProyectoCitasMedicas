using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public abstract class Horario : BaseEntity
    {
        public abstract TimeSpan HoraInicio { get; }
        public abstract TimeSpan HoraFin { get; }
    }
}
