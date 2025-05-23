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
            EspecialidadesRepository Prepo = new EspecialidadesRepository();



            List<Especialidad> lista = Prepo.Consultar();

            foreach (var espe in lista)
            {
                Console.WriteLine(
                    $"ID: {espe.Id}, Paciente: {espe.NombreCompleto}"
                    );
            }

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

            List<Medico> lista = Prepo.Consultar();
            foreach (var medico in lista)
            {
                Console.WriteLine(
                    $"ID: {medico.IdMedico}, Nombre: {medico.NombreCompleto}, Especialidad: {medico.IdEspecialidad} " +
                    $"Tipo Doc: {medico.TipoDocumento}, Nro Doc: {medico.NroDocumento}, Sexo: {medico.Sexo}"
                    );
            }


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
