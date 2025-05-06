using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HistoriaClinica : BaseEntity
    {
        public int IdPaciente { get; set; }
        public string MotivoConsulta { get; set; }
        public string Descripcion { get; set; }
        public string Evolucion { get; set; }
        public string CausaExterna { get; set; }
        public SignosVitales SignosVitales { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public ExamenFisico ExamenFisico { get; set; }
    }
}
