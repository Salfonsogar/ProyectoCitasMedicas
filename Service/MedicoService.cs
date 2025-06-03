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
        private readonly HorarioMedicoService horarioMedicoService;
        private readonly MedicoRepository repository;

        public MedicoService(MedicoRepository repository) : base(repository)
        {
            this.repository = repository;
            horarioMedicoService = new HorarioMedicoService(new HorarioMedicoRepository());
        }
        public List<Medico> MedicosPorEspecialidad(int idEspecialidad)
        {
            return Consultar().Where(m => m.IdEspecialidad == idEspecialidad).ToList();
        }

        public HorarioMedico ObtenerHorarioMedico(int idMedico)
        {
            var medico = Consultar().FirstOrDefault(m => m.IdMedico == idMedico);

            return horarioMedicoService.ObtenerHorarioPorId(medico.IdHorarioMedico);
        }

        

        public bool EsMedicoPorIdPersona(int idPersona)
        {
            return repository.ExisteMedicoPorIdPersona(idPersona);
        }

    }
}
