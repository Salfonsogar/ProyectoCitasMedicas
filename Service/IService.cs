using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<T>
    {
        Task<string> Agregar(T entity);
        List<T> Consultar();
        Task<string> Modificar(T entity);
        Task<string> Eliminar(int id);
    }
}
