using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace Service
{
    public class CitaMedicaService : GenericService<CitaMedica, CitaMedicaRepository>
    {
        private readonly MedicoService _medicoService;
        private readonly HorarioCitaMedicaService _horarioCitaMedicaService;
        public CitaMedicaService(CitaMedicaRepository repository) : base(repository)
        {
            _medicoService = new MedicoService(new MedicoRepository());
            _horarioCitaMedicaService = new HorarioCitaMedicaService(new HorarioCitaMedicaRepository());
        }

        public List<FechaDisponibleDto> ObtenerFechasDisponibles(int idEspecialidad)
        {
            int cantidad = 5;
            var fechasDisponibles = new List<FechaDisponibleDto>();

            var medicos = _medicoService.FiltrarMedicosPorEspecialidad(idEspecialidad);

            foreach (var medico in medicos)
            {
                var horario = _medicoService.ObtenerHorarioPorMedico(medico.IdMedico);
                if (horario == null) continue;

                var fechasOcupadas = ObtenerCitasPorMedico(medico.IdMedico)
                    .Select(c => ObtenerFechaCita(c))
                    .Where(f => f != DateTime.MinValue)
                    .ToList();

                var horasDisponibles = BuscarHorasDisponibles(horario, fechasOcupadas, cantidad - fechasDisponibles.Count);

                fechasDisponibles.AddRange(horasDisponibles.Select(h => new FechaDisponibleDto
                {
                    IdMedico = medico.IdMedico,
                    Fecha = h
                }));

                if (fechasDisponibles.Count >= cantidad)
                    break;
            }

            return fechasDisponibles;
        }


        public List<CitaMedica> ObtenerCitasPorMedico(int idMedico)
        {
            return Consultar().Where(c => c.IdMedico == idMedico).ToList();
        }
        public List<DateTime> BuscarHorasDisponibles(HorarioMedico horario, List<DateTime> fechasOcupadas, int cantidad)
        {
            var fechasDisponibles = new List<DateTime>();
            var duracion = HorarioCitaMedica.DuracionDefecto;

            for (int i = 0; i < 30 && fechasDisponibles.Count < cantidad; i++)
            {
                var dia = DateTime.Today.AddDays(i);

                if (dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday) continue;

                var horaInicio = dia.Date.Add(horario.HoraInicio);
                var horaFin = dia.Date.Add(horario.HoraFin);

                for (var hora = horaInicio; hora + duracion <= horaFin && fechasDisponibles.Count < cantidad; hora = hora.Add(duracion))
                {
                    if (!EstaOcupada(hora,fechasOcupadas))
                        fechasDisponibles.Add(hora);
                }
            }

            return fechasDisponibles;
        }
        bool EstaOcupada(DateTime hora, List<DateTime> ocupadas)
        {
            return ocupadas.Any(f => f.Date == hora.Date && f.Hour == hora.Hour && f.Minute == hora.Minute);
        }
        public DateTime ObtenerFechaCita(CitaMedica cita)
        {
            var horarioCita = _horarioCitaMedicaService.Consultar()?.FirstOrDefault(h => h.Id == cita.IdHorarioCita);
            return horarioCita != null ? horarioCita.FechaHora : DateTime.MinValue;
        }
    }
}
