using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Diagnostico : BaseEntity
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Antecedentes { get; set; }
        public int IdHistoriaClinica { get; set; }
    }
}
