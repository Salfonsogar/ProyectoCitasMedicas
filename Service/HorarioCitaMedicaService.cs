using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace Service
{
    public class HorarioCitaMedicaService : GenericService<HorarioCitaMedica, HorarioCitaMedicaRepository>
    {
        public HorarioCitaMedicaService(HorarioCitaMedicaRepository repository) : base(repository)
        {
        }

        public int ObtenerIdPorFecha(DateTime fecha)
        {
            return Consultar()
                .Where(h => h.FechaHora.Date == fecha.Date)
                .Select(h => h.Id)
                .FirstOrDefault();
        }
    }
}
