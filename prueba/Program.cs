using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using DAL;
using Entity;

namespace prueba
{
    class Program
    {
        static async Task Main()
        {
            CitaMedicaRepository cr = new CitaMedicaRepository();

            List<CitaMedica> lista = cr.Consultar();

            foreach (var cita in lista)
            {
                Console.WriteLine(
                    $"Id: {cita.Id}, IdPaciente: {cita.IdPaciente}, Nombre paciente: {cita.paciente.NombreCompleto}, IdMedico: {cita.IdMedico}, " +
                    $"Nombre medico: {cita.medico.NombreCompleto}, fecha y hora: {cita.Fecha}, estado: {cita.Estado} "
                    );
            }

            //var cita = cr.Agregar(new CitaMedica
            //{
            //    medico = new Medico { IdMedico = 8 },
            //    paciente = new Paciente { IdPaciente = 1 },
            //    Fecha = DateTime.Now,
            //    Estado = "Pendiente"
            //});

            //    HorarioMedicoService service = new HorarioMedicoService(new HorarioMedicoRepository());

            //    var horarios = service.Consultar();
            //    foreach (var horario in horarios)
            //    {
            //        Console.WriteLine(
            //            $"ID: {horario.Id}, Hora Inicio: {horario.HoraInicio}, Hora Fin: {horario.HoraFin}"
            //            );
            //    }

            //    MedicoService service = new MedicoService(new MedicoRepository());

            //var horario = service.ObtenerHorarioMedico(1);
            //Console.WriteLine($"ID: {horario.Id}, Hora Inicio: {horario.HoraInicio}, Hora Fin: {horario.HoraFin}");

            //var service = new CitaMedicaService(new CitaMedicaRepository());

            //var cita = new CitaMedica
            //{
            //    medico = new Medico { IdMedico = 1 },
            //    paciente = new Paciente { IdPaciente = 1 },
            //    Fecha = DateTime.Now,
            //    Estado = "Pendiente"
            //};

            //var resultado = await service.Agregar(cita);
            //Console.WriteLine(resultado);

            //var service = new CitaMedicaService(new CitaMedicaRepository());
            //var citas = service.Consultar();
            //if (citas == null || citas.Count == 0)
            //{
            //    Console.WriteLine("No se encontraron citas para el paciente.");
            //    return;
            //}
            //foreach (var cita in citas)
            //{
            //    Console.WriteLine(
            //        $"ID: {cita.Id}, " +
            //        $"Fecha: {cita.Fecha}, Estado: {cita.Estado}"
            //        );
            //}

            //var medicos = service.Consultar();
            //foreach (var m in medicos)
            //{
            //    Console.WriteLine(
            //        $"ID: {m.IdMedico}, Nombre: {m.NombreCompleto}, Especialidad: {m.IdEspecialidad} " +
            //        $"Tipo Doc: {m.TipoDocumento}, Nro Doc: {m.NroDocumento}, Sexo: {m.Sexo}"
            //        +$"id horario: {m.IdHorarioMedico}");
            //}
            //var horarios =  service.ObtenerHorarioMedico(1);
            //Console.WriteLine($"ID: {horarios.Id}, Hora Inicio: {horarios.HoraInicio}, Hora Fin: {horarios.HoraFin}");
            //EspecialidadesRepository Prepo = new EspecialidadesRepository();



            //List<Especialidad> lista = Prepo.Consultar();

            //foreach (var espe in lista)
            //{
            //    Console.WriteLine(
            //        $"ID: {espe.Id}, Paciente: {espe.NombreCompleto}"
            //        );
            //}

            //String idCita = await Prepo.Agregar(new CitaMedica
            //{
            //    FechaHora = new DateTime(2025, 5, 21, 9, 0, 0),
            //    HoraFin = new TimeSpan(10, 0, 0)

            //});

            //Console.WriteLine(idCita);

            //String idMedico = await Prepo.Agregar(new Medico
            //{
            //    NombreCompleto = "Fran Monguito Mongui",
            //    TipoDocumento = "CC",
            //    NroDocumento = 111111,
            //    Sexo = 'M',
            //    Edad = 28,
            //    Telefono = "314",
            //    Correo = "fran@monguitomemé.com",
            //    Direccion = "a",
            //    FechaNacimiento = new DateTime(1998, 01, 27)
            //}, "1", "1");
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

            //List<Medico> lista = Prepo.Consultar();
            //foreach (var medico in lista)
            //{
            //    Console.WriteLine(
            //        $"ID: {medico.IdMedico}, Nombre: {medico.NombreCompleto}, Especialidad: {medico.IdEspecialidad} " +
            //        $"Tipo Doc: {medico.TipoDocumento}, Nro Doc: {medico.NroDocumento}, Sexo: {medico.Sexo}"
            //        );
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

            //String idHorarioMedico = await Prepo.Agregar(new HorarioMedico
            //{
            //    HoraInicio = new TimeSpan(10, 0, 0),
            //    HoraFin = new TimeSpan(18, 0, 0)
            //});

            //await Prepo.Modificar(new Especialidad
            //{
            //    Id = 5,
            //    NombreCompleto = "Oftalmologia"
            //});

            //Console.WriteLine(idHorarioMedico);
            Console.ReadKey();
        }
    }
}
