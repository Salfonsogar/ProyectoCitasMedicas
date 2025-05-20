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
            MedicoRepository Prepo = new MedicoRepository();

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

            //List<Medico> lista = Prepo.Consultar();
            //foreach (var medico in lista)
            //{
            //    Console.WriteLine(
            //        $"ID: {medico.IdMedico}, Nombre: {medico.NombreCompleto}, " +
            //        $"Tipo Doc: {medico.TipoDocumento}, Nro Doc: {medico.NroDocumento}, Sexo: {medico.Sexo}, Edad: {medico.Edad}, " +
            //        $"Teléfono: {medico.Telefono}, Correo: {medico.Correo}, Dirección: {medico.Direccion}, " +
            //        $"Nacimiento: {medico.FechaNacimiento.ToShortDateString()}, ID Horario: {medico.IdHorarioMedico}, ID Especialidad: {medico.IdEspecialidad}"
            //    );
            //}

            //Console.WriteLine(await Prepo.Eliminar(1));

            await Prepo.Modificar(new Medico
            {
                Id = "16",
                NombreCompleto = "Fran Uvito Mongui",
                TipoDocumento = "CC",
                NroDocumento = 111111,
                Sexo = 'M',
                Edad = 28,
                Telefono = "314",
                Correo = "fran@monguitomemé.com",
                Direccion = "a",
                FechaNacimiento = new DateTime(1998, 01, 27),
                IdEspecialidad = 2,
                IdHorarioMedico = 3
            });
            Console.ReadKey();
        }
    }
}
