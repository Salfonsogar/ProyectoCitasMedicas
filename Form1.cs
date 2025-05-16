using System;
using System.Windows.Forms;
using ServiceBot;

namespace CitasBot
{
    public partial class Form1 : Form
    {
        private BotService _botService;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
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
