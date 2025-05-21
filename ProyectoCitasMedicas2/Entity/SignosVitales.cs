using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SignosVitales : BaseEntity
    {
        public double Peso { get; set; }
        public double Talla { get; set; }
        public double IndiceMasaCorporal { get; set; }
        public int FrecuenciaCardiaca { get; set; }
        public int PresionArterialDiastolica { get; set; }
        public int PresionArterialSistolica { get; set; }
        public int PresionArterialMedia { get; set; }
        public int SaturacionOxigeno { get; set; }
        public int FrecuenciaRespiratoria { get; set; }
        public int IdHistoriaClinica { get; set; }
    }
}
