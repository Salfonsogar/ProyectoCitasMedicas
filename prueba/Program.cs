using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace prueba
{
    class Program
    {
        static async Task Main()
        {
            PacienteRepository Prepo = new PacienteRepository();

            //List<CitaMedica> lista = Prepo.Consultar();

            //foreach (var cita in lista)
            //{
            //    Console.WriteLine(
            //        $"ID: {cita.Id}, Paciente: {cita.paciente.NombreCompleto}, Medico: {cita.medico.NombreCompleto}, Fecha y hora: {cita.horariocm.FechaHora}, Hora final: {cita.horariocm.HoraFin}, estado: {cita.Estado}"
            //        );
            //}

            //String idCita = await Prepo.Agregar(new CitaMedica
            //{
            //    medico = new Medico { IdMedico = 4 },
            //    paciente = new Paciente { IdPaciente = 1 },
            //    horariocm = new HorarioCitaMedica { Id = 1 },
            //    Estado = "Completada"
            //});

            //Console.WriteLine(idCita);

            //String idMedico = await Prepo.Agregar(new Paciente
            //{
            //    NombreCompleto = "Julian Martinez Ospina",
            //    TipoDocumento = "CC",
            //    NroDocumento = 10594786,
            //    Sexo = 'M',
            //    Edad = 28,
            //    Telefono = "314",
            //    Correo = "fran@monguitomemé.com",
            //    Direccion = "a",
            //    FechaNacimiento = new DateTime(1998, 01, 27)
            //});
            //Console.WriteLine(idMedico);

            //List<Paciente> lista = Prepo.Consultar();
            //foreach (var paciente in lista)
            //{
            //    Console.WriteLine(
            //        $"ID: {paciente.IdPaciente}, Nombre: {paciente.NombreCompleto}, " +
            //        $"Tipo Doc: {paciente.TipoDocumento}, Nro Doc: {paciente.NroDocumento}, Sexo: {paciente.Sexo}, Edad: {paciente.Edad}, " +
            //        $"Teléfono: {paciente.Telefono}, Correo: {paciente.Correo}, Dirección: {paciente.Direccion}, " +
            //        $"Nacimiento: {paciente.FechaNacimiento.ToShortDateString()}"
            //    );
            //}


            //List<HorarioCitaMedica> lista = Prepo.Consultar();
            //foreach (var horario in lista)
            //{
            //    Console.WriteLine(
            //        $"ID: {horario.Id}, fecha y hora inicial: {horario.FechaHora}, hora inicio: {horario.HoraInicio}, hora fin: {horario.HoraFin}, duracion: {horario.Duracion}"
            //        );
            //}

            //Console.WriteLine(await Prepo.Eliminar(1));

            //await Prepo.Modificar(new Medico
            //{
            //    Id = "16",
            //    NombreCompleto = "Fran Uvito Mongui",
            //    TipoDocumento = "CC",
            //    NroDocumento = 111111,
            //    Sexo = 'M',
            //    Edad = 28,
            //    Telefono = "314",
            //    Correo = "fran@monguitomemé.com",
            //    Direccion = "a",
            //    FechaNacimiento = new DateTime(1998, 01, 27),
            //    IdEspecialidad = 2,
            //    IdHorarioMedico = 3
            //});

            //String idHorarioMedico = await Prepo.Modificar(new HorarioCitaMedica
            //{
            //    Id = 1,
            //    FechaHora = new DateTime(2025, 5, 22, 8, 0, 0),
            //    HoraFin = new TimeSpan(8, 30, 0)
            //});

            //String idHorarioMedico = await Prepo.Agregar(new HorarioCitaMedica
            //{
            //    FechaHora = new DateTime(2025, 5, 22, 8, 0, 0),
            //    HoraFin = new TimeSpan(18, 0, 0)
            //});

            //String idCita = await Prepo.Modificar(new CitaMedica
            //{
            //    Id = 3,
            //    medico = new Medico { IdMedico = 6},
            //    paciente = new Paciente { IdPaciente = 1},
            //    horariocm = new HorarioCitaMedica { Id = 1},
            //    Estado = "Pendiente"
            //});

            //Console.WriteLine(idCita);
            Console.ReadKey();
        }
    }
}
