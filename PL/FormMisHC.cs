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
    public partial class FormMisHC: Form
    {
        public FormMisHC()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            FormInicioMed formInicioMed = new FormInicioMed();
            FormNavegador.AbrirFormulario(this, formInicioMed);
        }

        private void FormMisHC_Load(object sender, EventArgs e)
        {
            MedicoRepository medicoRepo = new MedicoRepository();
            HistoriaClinicaRepository historiaRepo = new HistoriaClinicaRepository();

            int idMedico = medicoRepo.ObtenerIdMedicoPorIdPersona(Sesion.UsuarioActual.IdPersona);
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            string nombrePersona = usuarioRepository.ObtenerNombrePersonaPorIdUsuario(Sesion.UsuarioActual.Id);
            lblMedico.Text = $"Historias clínicas de {nombrePersona}";

            // cargar al datagridview
            List<HistoriaClinica> historias = historiaRepo.ConsultarPorMedico(idMedico);
            dgvHistoriasClinicas.DataSource = historias;

            dgvHistoriasClinicas.Columns["Id"].Visible = false;
            dgvHistoriasClinicas.Columns["IdMedico"].Visible = false;

            dgvHistoriasClinicas.Columns["IdPaciente"].HeaderText = "ID Paciente";
            dgvHistoriasClinicas.Columns["NombrePaciente"].HeaderText = "Paciente";
            dgvHistoriasClinicas.Columns["NombreMedico"].HeaderText = "Médico";
            dgvHistoriasClinicas.Columns["MotivoConsulta"].HeaderText = "Motivo Consulta";
            dgvHistoriasClinicas.Columns["Descripcion"].HeaderText = "Descripción";
            dgvHistoriasClinicas.Columns["Evolucion"].HeaderText = "Evolución";
            dgvHistoriasClinicas.Columns["CausaExterna"].HeaderText = "Causa Externa";
            dgvHistoriasClinicas.Columns["SignosVitales"].HeaderText = "Signos Vitales";
            dgvHistoriasClinicas.Columns["ExamenFisico"].HeaderText = "Examen Físico";
            dgvHistoriasClinicas.Columns["Diagnostico"].HeaderText = "Diagnóstico";

            dgvHistoriasClinicas.Columns["IdPaciente"].DisplayIndex = 0;
            dgvHistoriasClinicas.Columns["NombrePaciente"].DisplayIndex = 1;
            dgvHistoriasClinicas.Columns["MotivoConsulta"].DisplayIndex = 2;
            dgvHistoriasClinicas.Columns["Descripcion"].DisplayIndex = 3;
            dgvHistoriasClinicas.Columns["Evolucion"].DisplayIndex = 4;
            dgvHistoriasClinicas.Columns["CausaExterna"].DisplayIndex = 5;
            dgvHistoriasClinicas.Columns["SignosVitales"].DisplayIndex = 6;
            dgvHistoriasClinicas.Columns["ExamenFisico"].DisplayIndex = 7;
            dgvHistoriasClinicas.Columns["Diagnostico"].DisplayIndex = 8;


        }
    }
}
