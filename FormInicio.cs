using ServiceBot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceBot;
using Service;
using DAL;

namespace CitasBot
{
    public partial class FormInicio: Form
    {
        private BotService _botService;
        public bool botRemindi_isOnline = false;
        public FormInicio()
        {
            InitializeComponent();
            if (botRemindi_isOnline == true)
            {
                btnBot.Enabled = false;
            }

        }

        private void FormInicio_Load(object sender, EventArgs e)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            string nombrePersona = usuarioRepository.ObtenerNombrePersonaPorIdUsuario(Sesion.UsuarioActual.Id);
            lblWelcome.Text = $"Bienvenido {nombrePersona}!";
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FormPacientes formPaciente = new FormPacientes();
            FormNavegador.AbrirFormulario(this, formPaciente);
        }

        private async void btnBot_Click(object sender, EventArgs e)
        {
            if (botRemindi_isOnline == false)
            {
                string token = "7361314283:AAGKtuu_9u5oEPonIeR8HaUDPV-wmM0buI4";
                _botService = new BotService(token);

                try
                {
                    await _botService.StartBotAsync();
                    MessageBox.Show("🤖 Bot iniciado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    botRemindi_isOnline = true;
                    btnBot.Enabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error al iniciar el bot:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

            }

        }

        private void btnMedicos_Click(object sender, EventArgs e)
        {
            FormMedicos formMedico = new FormMedicos();
            FormNavegador.AbrirFormulario(this, formMedico);
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

        private void btnHistoriaClinica_Click(object sender, EventArgs e)
        {
            FormHistoriasClinicas formHistoriasClinicas = new FormHistoriasClinicas();
            FormNavegador.AbrirFormulario(this, formHistoriasClinicas);
        }
    }
}
