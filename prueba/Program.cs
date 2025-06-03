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
            Console.ReadKey();
        }
    }
}
