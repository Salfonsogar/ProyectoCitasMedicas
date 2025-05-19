using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T>
    {
        Task<string> Agregar(T entity);
        List<T> Consultar();
        string Modificar(T entity);
        Task<string> Eliminar(int id);
    }
}
