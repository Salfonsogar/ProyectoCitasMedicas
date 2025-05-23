using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace Service
{
    public class MedicoService : GenericPersonaService<Medico, MedicoRepository>
    {
        private readonly HorarioMedicoRepository horarioMedicoRepository;
        public MedicoService(MedicoRepository repository) : base(repository)
        {
            horarioMedicoRepository = new HorarioMedicoRepository();
        }
        public List<Medico> MedicosPorEspecialidad(int idEspecialidad)
        {
            return Consultar().Where(m => m.IdEspecialidad == idEspecialidad).ToList();
        }

        public HorarioMedico ObtenerHorarioMedico(int idMedico)
        {
            var medico = Consultar().FirstOrDefault(m => m.Id == idMedico);
            if (medico == null)
                return null;

            return horarioMedicoRepository.Consultar()
                .FirstOrDefault(h => h.Id == medico.IdHorarioMedico);
        }
    }
}
