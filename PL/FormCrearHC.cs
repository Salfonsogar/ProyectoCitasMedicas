using DAL;
using Entity;
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
    public partial class FormCrearHC: Form
    {
        PacienteRepository pacienteRepo = new PacienteRepository();
        HistoriaClinicaRepository historiaRepo = new HistoriaClinicaRepository();
        Paciente pacienteEncontrado = null;
        public FormCrearHC()
        {
            InitializeComponent();
        }

        // Placeholderrr
        private void AplicarPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Black;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Black;
                }
            };
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            FormInicioMed formInicioMed = new FormInicioMed();
            FormNavegador.AbrirFormulario(this, formInicioMed);
        }

        private void FormCrearHC_Load(object sender, EventArgs e)
        {
            // Mostrar usuario
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            string nombrePersona = usuarioRepository.ObtenerNombrePersonaPorIdUsuario(Sesion.UsuarioActual.Id);
            lblHC.Text = $"Historia clínica de {nombrePersona}";

            AplicarPlaceholder(txtDocumento, "N# Documento");
            cmbCausaExterna.SelectedIndex = 0;
            DeshabilitarCampos();
            
        }


        // Deshabilitar campos

        private void DeshabilitarCampos()
        {
            // Deshabilitar campos de texto
            txtMotivoConsulta.Enabled = false;
            txtDescripcion.Enabled = false;
            txtEvolucion.Enabled = false;
            txtSignosVitales.Enabled = false;
            txtDiagnostico.Enabled = false;
            txtExamenFisico.Enabled = false;

            // Deshabilitar ComboBox y boton de guardado
            cmbCausaExterna.Enabled = false;
            btnGuardar.Enabled = false;
        }
        // Habilitar campos de nuevo
        private void HabilitarCampos()
        {
            txtMotivoConsulta.Enabled = true;
            txtDescripcion.Enabled = true;
            txtEvolucion.Enabled = true;
            txtSignosVitales.Enabled = true;
            txtDiagnostico.Enabled = true;
            txtExamenFisico.Enabled = true;
            cmbCausaExterna.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string documento = txtDocumento.Text.Trim();
            if (string.IsNullOrEmpty(documento))
            {
                MessageBox.Show("Ingrese un número de documento.");
                return;
            }

            // Convert the string 'documento' to an integer to match the type of 'NroDocumento'
            // # ayuda de copilot, convierte id a int para comparar y buscar
            if (!int.TryParse(documento, out int documentoInt))
            {
                MessageBox.Show("El número de documento debe ser un valor numérico.");
                return;
            }

            pacienteEncontrado = pacienteRepo.Consultar().FirstOrDefault(p => p.NroDocumento == documentoInt);

            if (pacienteEncontrado == null)
            {
                MessageBox.Show("Paciente no encontrado.");
                lblNombrePaciente.Text = string.Empty;
                return;
            }

            lblNombrePaciente.Text = pacienteEncontrado.NombreCompleto;
            HabilitarCampos();
        }

        // Limpiar formulario
        private void LimpiarFormulario()
        {
            txtDocumento.Clear();
            lblNombrePaciente.Text = "";
            txtMotivoConsulta.Clear();
            txtDescripcion.Clear();
            txtEvolucion.Clear();
            cmbCausaExterna.SelectedIndex = 0;
            txtSignosVitales.Clear();
            txtExamenFisico.Clear();
            txtDiagnostico.Clear();
            pacienteEncontrado = null;
        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (pacienteEncontrado == null)
            {
                MessageBox.Show("Primero busque un paciente.");
                return;
            }

            MedicoRepository medicoRepo = new MedicoRepository();
            int idMedico = medicoRepo.ObtenerIdMedicoPorIdPersona(Sesion.UsuarioActual.IdPersona);

            if (idMedico == 0)
            {
                MessageBox.Show("No se encontró el médico asociado al usuario actual.");
                return;
            }

            HistoriaClinica hc = new HistoriaClinica
            {
                IdPaciente = pacienteEncontrado.IdPaciente,
                IdMedico = idMedico,
                MotivoConsulta = txtMotivoConsulta.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Evolucion = txtEvolucion.Text.Trim(),
                CausaExterna = cmbCausaExterna.Text.Trim(),
                SignosVitales = txtSignosVitales.Text.Trim(),
                ExamenFisico = txtExamenFisico.Text.Trim(),
                Diagnostico = txtDiagnostico.Text.Trim()
            };

            string resultado = await historiaRepo.Agregar(hc);
            MessageBox.Show(resultado);
            LimpiarFormulario();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            pacienteEncontrado = null;
            lblNombrePaciente.Text = "Nombre del paciente";
            DeshabilitarCampos();
            

        }
    }
}
