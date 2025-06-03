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
    public partial class FormHistoriasClinicas : Form
    {
        HistoriaClinicaRepository historiaRepo = new HistoriaClinicaRepository();
        public FormHistoriasClinicas()
        {
            InitializeComponent();
        }

        private void CargarHistoriasClinicas()
        {
            dgvHClinicas.DataSource = null;
            List<HistoriaClinica> historias = historiaRepo.Consultar(); // historiaRepo debe estar instanciado
            dgvHClinicas.DataSource = historias;

            // Configurar encabezados
            dgvHClinicas.Columns["Id"].HeaderText = "ID";
            dgvHClinicas.Columns["IdPaciente"].HeaderText = "ID Paciente";
            dgvHClinicas.Columns["MotivoConsulta"].HeaderText = "Motivo";
            dgvHClinicas.Columns["Descripcion"].HeaderText = "Descripción";
            dgvHClinicas.Columns["Evolucion"].HeaderText = "Evolución";
            dgvHClinicas.Columns["CausaExterna"].HeaderText = "Causa";
            dgvHClinicas.Columns["SignosVitales"].HeaderText = "Signos Vitales";
            dgvHClinicas.Columns["Diagnostico"].HeaderText = "Diagnósticos";
            dgvHClinicas.Columns["ExamenFisico"].HeaderText = "Exámenes";
        }


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            FormInicio formInicio = new FormInicio();
            FormNavegador.AbrirFormulario(this, formInicio);
        }

        private void dgvHClinicas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormHistoriasClinicas_Load(object sender, EventArgs e)
        {
            CargarHistoriasClinicas();
        }
    }
}
