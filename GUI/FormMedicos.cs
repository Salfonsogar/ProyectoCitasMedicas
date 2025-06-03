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
    public partial class FormMedicos : Form
    {
        private int idMedicoSeleccionado = 0;
        public FormMedicos()
        {
            InitializeComponent();
            AplicarPlaceholder(txtNombre, "Nombre completo");
            AplicarPlaceholder(txtID, "N# Documento");
            AplicarPlaceholder(txtTelefono, "Teléfono");
            AplicarPlaceholder(txtDireccion, "Dirección");
            AplicarPlaceholder(txtCorreo, "Correo electrónico");
            CargarEspecialidades();
            CargarHorarios();
            btnEditar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        // metodos cargar cmbBoxes

        private void CargarEspecialidades()
        {
            EspecialidadesRepository repo = new EspecialidadesRepository();
            List<Especialidad> listaEspecialidades = repo.Consultar();

            cmbEspecialidad.DataSource = listaEspecialidades;
            cmbEspecialidad.DisplayMember = "NombreCompleto";
            cmbEspecialidad.ValueMember = "Id";
            cmbEspecialidad.SelectedIndex = -1;
        }

        private void CargarHorarios()
        {
            HorarioMedicoRepository repo = new HorarioMedicoRepository();
            List<HorarioMedico> listaHorarios = repo.Consultar();

            cmbHorarioMedico.DataSource = listaHorarios;
            cmbHorarioMedico.DisplayMember = "Descripcion";  // "08:00 - 12:00"
            cmbHorarioMedico.ValueMember = "Id";
            cmbHorarioMedico.SelectedIndex = -1;
        }


        // crear instancia medicorepository
        MedicoRepository medicoRepo = new MedicoRepository();

        private void CargarMedicos()
        {
            MedicoRepository repo = new MedicoRepository();
            List<Medico> medicos = repo.Consultar();
            dgvMedicos.DataSource = medicos;
            // ocultar ID
            dgvMedicos.Columns["Id"].Visible = false;
            dgvMedicos.Columns["FechaNacimiento"].Visible = false;
            dgvMedicos.Columns["Edad"].Visible = false;
            dgvMedicos.Columns["Sexo"].Visible = false;
            dgvMedicos.Columns["Telefono"].Visible = false;
            dgvMedicos.Columns["Correo"].Visible = false;
            dgvMedicos.Columns["Direccion"].Visible = false;
            dgvMedicos.Columns["IdEspecialidad"].Visible = false;
            dgvMedicos.Columns["IdHorarioMedico"].Visible = false;
            dgvMedicos.Columns["HoraInicio"].Visible = false;
            dgvMedicos.Columns["HoraFin"].Visible = false;

            // organizar columnas
            dgvMedicos.Columns["IdMedico"].HeaderText = "ID";
            dgvMedicos.Columns["NombreCompleto"].HeaderText = "Nombre";
            dgvMedicos.Columns["TipoDocumento"].HeaderText = "Tipo Doc.";
            dgvMedicos.Columns["NroDocumento"].HeaderText = "N° Documento";
            dgvMedicos.Columns["FechaNacimiento"].HeaderText = "Nacimiento";
            dgvMedicos.Columns["NombreEspecialidad"].HeaderText = "Especialidad";
            dgvMedicos.Columns["HorarioDescripcion"].HeaderText = "Horario";
            dgvMedicos.Columns["IdMedico"].DisplayIndex = 0;
            dgvMedicos.Columns["NombreCompleto"].DisplayIndex = 1;


        }

        // calcular especialidad

        private void CalcularEspecialidad()
        {

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

            idMedicoSeleccionado = 0;
            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
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

        private void FormMedicos_Load(object sender, EventArgs e)
        {
            CargarMedicos();
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
            Medico medico = new Medico
            {
                NombreCompleto = txtNombre.Text,
                TipoDocumento = cmbTipoDoc.Text,
                NroDocumento = int.Parse(txtID.Text),
                Sexo = cmbSexo.Text[0],
                Edad = CalcularEdad(dtFechaNacimiento.Value),
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text,
                FechaNacimiento = dtFechaNacimiento.Value,
                IdEspecialidad = (int)cmbEspecialidad.SelectedValue,
                IdHorarioMedico = (int)cmbHorarioMedico.SelectedValue
            };

            string mensaje = await medicoRepo.Agregar(medico);
            MessageBox.Show(mensaje);
            CargarMedicos();
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

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            FormInicio formInicio = new FormInicio();
            FormNavegador.AbrirFormulario(this, formInicio);
        }

        private void dgvMedicos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMedicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvMedicos.Rows[e.RowIndex];
                idMedicoSeleccionado = (int)fila.Cells["Id"].Value;
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
                cmbEspecialidad.SelectedValue = fila.Cells["IdEspecialidad"].Value;
                cmbHorarioMedico.SelectedValue = fila.Cells["IdHorarioMedico"].Value;
                dtFechaNacimiento.Value = (DateTime)fila.Cells["FechaNacimiento"].Value;



                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (idMedicoSeleccionado <= 0)
            {
                MessageBox.Show("Selecciona un médico para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Medico medico = new Medico
            {
                Id = idMedicoSeleccionado,
                IdMedico = idMedicoSeleccionado,
                NombreCompleto = txtNombre.Text,
                TipoDocumento = cmbTipoDoc.Text,
                NroDocumento = int.Parse(txtID.Text),
                Sexo = cmbSexo.Text[0],
                Edad = CalcularEdad(dtFechaNacimiento.Value),
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text,
                FechaNacimiento = dtFechaNacimiento.Value,
                IdEspecialidad = (int)cmbEspecialidad.SelectedValue,
                IdHorarioMedico = (int)cmbHorarioMedico.SelectedValue
            };

            string mensaje = await medicoRepo.Modificar(medico);
            MessageBox.Show(mensaje, "Médico actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarMedicos();
            LimpiarCampos();
            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
        }

        private void dgvMedicos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
