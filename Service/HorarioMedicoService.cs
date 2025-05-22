using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace Service
{
    public class HorarioMedicoService : GenericService<HorarioMedico, HorarioMedicoRepository>
    {
        public HorarioMedicoService(HorarioMedicoRepository repository) : base(repository)
        {
        }
    }    
}
