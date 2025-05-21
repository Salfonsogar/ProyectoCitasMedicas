using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ExamenFisico : BaseEntity
    {
        public string Abdomen { get; set; }
        public string CabezaCuelloCara { get; set; }
        public string SistemaNervioso { get; set; }
        public string Cardiopulmonar { get; set; }
        public string Genitourinario { get; set; }
        public string Extremidades { get; set; }
        public string PielFaneras { get; set; }
        public string Descripcion { get; set; }
        public int IdHistoriaClinica { get; set; }
    }
}
