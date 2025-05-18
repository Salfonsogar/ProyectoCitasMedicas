using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Paciente : Persona
    {
        public int IdPaciente { get; set; }
        public List<HistoriaClinica> HistoriasClinicas { get; set; } = new List<HistoriaClinica>();
        public List<FormulaMedica> FormulasMedicas { get; set; } = new List<FormulaMedica>();
        public List<CitaMedica> CitasMedicas { get; set; } = new List<CitaMedica>();
    }
}
