using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Usuario : BaseEntity
    {
        public string Username { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        public int IdPersona { get; set; }
    }
}
