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
namespace CitasBot
{
    public partial class FormInicio: Form
    {
        private BotService _botService;
        public FormInicio()
        {
            InitializeComponent();

        }

        private void FormInicio_Load(object sender, EventArgs e)
        {
            
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FormPacientes formPaciente = new FormPacientes();
            FormNavegador.AbrirFormulario(this, formPaciente);
        }

        private async void btnBot_Click(object sender, EventArgs e)
        {
            string token = "7361314283:AAGKtuu_9u5oEPonIeR8HaUDPV-wmM0buI4";
            _botService = new BotService(token);

            try
            {
                await _botService.StartBotAsync();
                MessageBox.Show("🤖 Bot iniciado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al iniciar el bot:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
