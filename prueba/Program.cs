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

            //String idPaciente = await Prepo.Agregar(new Paciente
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
            //});
            //Console.WriteLine(idPaciente);

            //List<Paciente> lista = Prepo.Consultar();
            //Console.WriteLine(lista.ToString());

            Prepo.Modificar(new Paciente
            {
                Id = "14",
                NombreCompleto = "Fran Monguito Mongui",
                TipoDocumento = "CC",
                NroDocumento = 111111,
                Sexo = 'M',
                Edad = 28,
                Telefono = "314",
                Correo = "fran@monguitomemé.com",
                Direccion = "a",
                FechaNacimiento = new DateTime(1998, 01, 27)
            });
            Console.ReadKey();
        }
    }
}
