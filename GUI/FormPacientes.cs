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

namespace GUI
{
    public partial class FormPacientes : Form
    {
        private int idPacienteSeleccionado = 0;
        public FormPacientes()
        {

            InitializeComponent();

            //id_debug.Text = $"id debug = {idPacienteSeleccionado}";

            AplicarPlaceholder(txtNombre, "Nombre completo");
            AplicarPlaceholder(txtID, "N# Documento");
            AplicarPlaceholder(txtTelefono, "Teléfono");
            AplicarPlaceholder(txtDireccion, "Dirección");
            AplicarPlaceholder(txtCorreo, "Correo electrónico");
            btnEditar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        // crear instancia pacienterepository
        PacienteRepository pacienteRepo = new PacienteRepository();

        // cargar pacientes
        private void CargarPacientes()
        {
            dgvPacientes.DataSource = null;
            List<Paciente> pacientes = pacienteRepo.Consultar();
            dgvPacientes.DataSource = pacientes;

            // ocultar ID
            dgvPacientes.Columns["Id"].Visible = false;

            dgvPacientes.Columns["IdPaciente"].HeaderText = "ID";
            dgvPacientes.Columns["NombreCompleto"].HeaderText = "Nombre";
            dgvPacientes.Columns["TipoDocumento"].HeaderText = "Tipo Doc.";
            dgvPacientes.Columns["NroDocumento"].HeaderText = "N° Documento";
            dgvPacientes.Columns["FechaNacimiento"].HeaderText = "Nacimiento";
            dgvPacientes.Columns["IdPaciente"].DisplayIndex = 0;
            dgvPacientes.Columns["NombreCompleto"].DisplayIndex = 1;

        }


        // limpiar campos
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtID.Clear();
            cmbTipoDoc.SelectedIndex = -1;
            cmbSexo.SelectedIndex = -1;
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            dtFechaNacimiento.Value = DateTime.Today;

            idPacienteSeleccionado = 0;
            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
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

        }

        private void FormPacientes_Load(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        // placeholder casero
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

        private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvPacientes.Rows[e.RowIndex];
                idPacienteSeleccionado = (int)fila.Cells["Id"].Value;
                //id_debug.Text = $"id2 debug = {idPacienteSeleccionado}";
                txtNombre.Text = fila.Cells["NombreCompleto"].Value.ToString();
                cmbTipoDoc.Text = fila.Cells["TipoDocumento"].Value.ToString();
                txtID.Text = fila.Cells["NroDocumento"].Value.ToString();
                string sexo = fila.Cells["Sexo"].Value.ToString();
                if (sexo == "M")
                {
                    cmbSexo.Text = "Masculino";
                }
                else if (sexo == "F")
                {
                    cmbSexo.Text = "Femenino";
                }
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value.ToString();
                dtFechaNacimiento.Value = (DateTime)fila.Cells["FechaNacimiento"].Value;

                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            AplicarPlaceholder(txtNombre, "Nombre completo");
            AplicarPlaceholder(txtID, "N# Documento");
            AplicarPlaceholder(txtTelefono, "Teléfono");
            AplicarPlaceholder(txtDireccion, "Dirección");
            AplicarPlaceholder(txtCorreo, "Correo electrónico");
        }

        // calcular edad con la fecha de nacimiento
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad;
        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            Paciente paciente = new Paciente
            {
                NombreCompleto = txtNombre.Text,
                TipoDocumento = cmbTipoDoc.Text,
                NroDocumento = int.Parse(txtID.Text),
                Sexo = cmbSexo.Text[0],
                Edad = CalcularEdad(dtFechaNacimiento.Value),
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text,
                FechaNacimiento = dtFechaNacimiento.Value
            };

            string mensaje = await pacienteRepo.Agregar(paciente);
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPacientes(); // Refrescar tabla
            LimpiarCampos();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow != null)
            {
                int id = (int)dgvPacientes.CurrentRow.Cells["Id"].Value;
                DialogResult result = MessageBox.Show("¿Deseas eliminar este paciente?", "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string mensaje = await pacienteRepo.Eliminar(id);
                    MessageBox.Show(mensaje);
                    CargarPacientes();
                    LimpiarCampos();
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (idPacienteSeleccionado <= 0)
            {
                MessageBox.Show("Selecciona un paciente para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Paciente paciente = new Paciente
            {
                Id = idPacienteSeleccionado,
                NombreCompleto = txtNombre.Text,
                TipoDocumento = cmbTipoDoc.Text,
                NroDocumento = int.Parse(txtID.Text),
                Sexo = cmbSexo.Text[0],
                Edad = CalcularEdad(dtFechaNacimiento.Value),
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text,
                FechaNacimiento = dtFechaNacimiento.Value
            };

            string mensaje = await pacienteRepo.Modificar(paciente);
            MessageBox.Show(mensaje, "Paciente actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPacientes();
            LimpiarCampos();
            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
        }
    }
}
