using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitasBot
{
    public static class FormNavegador
    {
        public static void AbrirFormulario(Form formularioActual, Form formularioNuevo)
        {
            formularioNuevo.FormClosed += (s, e) => Application.Exit();
            formularioNuevo.Show();
            formularioActual.Hide();
        }
    }
}
