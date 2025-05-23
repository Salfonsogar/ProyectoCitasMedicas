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
        public CitaMedicaService(CitaMedicaRepository repository) : base(repository)
        {
            _medicoService = new MedicoService(new MedicoRepository());
        }

        public List<FechaDisponibleDto> ObtenerFechasDisponibles(int idEspecialidad)
        {
            var cantidad = 5;
            var duracion = TimeSpan.FromMinutes(30);
            var fechasDisponibles = new List<FechaDisponibleDto>();
            var medicos = _medicoService.MedicosPorEspecialidad(idEspecialidad);

            foreach (var medico in medicos)
            {
                var fechasOcupadas = FechasOcupadasPorMedico(medico.IdMedico);
                var horarioMedico = _medicoService.ObtenerHorarioMedico(medico.IdMedico);
                var horaActual = horarioMedico.HoraInicio;

                for (int i = 0; i < 30; i++)
                {
                    var dia = DateTime.Now.AddDays(i);

                    if (dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday)continue;
                    if(fechasDisponibles.Count >= cantidad) break;
                    while (horaActual < horarioMedico.HoraFin)
                    {
                        bool estaOcupada = fechasOcupadas.Any(f => f.TimeOfDay == horaActual);

                        if (!estaOcupada)
                        {
                            fechasDisponibles.Add(new FechaDisponibleDto
                            {
                                Fecha = dia.Date + horaActual,
                                IdMedico = medico.IdMedico
                            });
                        }
                        horaActual = horaActual.Add(duracion);
                    }

                    horaActual = horarioMedico.HoraInicio;
                }
            }
            return fechasDisponibles;
        }

        public List<DateTime> FechasOcupadasPorMedico(int idMedico)
        {
            var citas = ObtenerCitasPorMedico(idMedico);
            var fechasOcupadas = new List<DateTime>();
            foreach (var cita in citas)
            {
                fechasOcupadas.Add(cita.Fecha);
            }
            return fechasOcupadas;
        }

        public List<CitaMedica> ObtenerCitasPorMedico(int idMedico)
        {
            return Consultar().Where(c => c.IdMedico == idMedico).ToList();
        }
    }
}
