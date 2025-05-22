using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace Service
{
    public class EspecialidadesService : GenericService<Especialidad, EspecialidadesRepository>
    {
        public EspecialidadesService(EspecialidadesRepository repository) : base(repository)
        {
        }
        public bool EsUnaEspecialidad(int id)
        {
            var especialidades = Consultar();
            return especialidades.Any(e => e.Id == id);
        }
        public string ObtenerNombre(int id)
        {
            var especialidad = Consultar().FirstOrDefault(e => e.Id == id);
            return especialidad?.NombreCompleto ?? "Desconocido";
        }
    }
}
