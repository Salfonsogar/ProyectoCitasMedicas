using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitasBot
{
    public partial class FormPacientes: Form
    {
        public FormPacientes()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            FormInicio formInicio = new FormInicio();
            FormNavegador.AbrirFormulario(this, formInicio);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_Click(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if(tb != null && tb.SelectionLength == 0)
            {
                tb.SelectAll();
            }
        }
    }
}
