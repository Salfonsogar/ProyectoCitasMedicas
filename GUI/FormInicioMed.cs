using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormInicioMed : Form
    {
        public FormInicioMed()
        {
            InitializeComponent();
        }

        private void FormInicioMed_Load(object sender, EventArgs e)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            string nombrePersona = usuarioRepository.ObtenerNombrePersonaPorIdUsuario(Sesion.UsuarioActual.Id);
            lblWelcome.Text = $"Bienvenido {nombrePersona}!";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Estás seguro de cerrar sesión?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Sesion.UsuarioActual = null;
                FormLogin formLogin = new FormLogin();
                FormNavegador.AbrirFormulario(this, formLogin);
            }
        }

        private void btnCrearHC_Click(object sender, EventArgs e)
        {
            FormCrearHC formCrearHC = new FormCrearHC();
            FormNavegador.AbrirFormulario(this, formCrearHC);
        }

        private void btnHC_Click(object sender, EventArgs e)
        {
            FormMisHC formMisHC = new FormMisHC();
            FormNavegador.AbrirFormulario(this, formMisHC);
        }
    }
}
