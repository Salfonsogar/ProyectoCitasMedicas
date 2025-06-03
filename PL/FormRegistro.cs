using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Entity;
using Service;

namespace CitasBot
{
    public partial class FormRegistro: Form
    {
        private UsuarioService usuarioService;
        private MedicoService medicoService;
        public FormRegistro()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
            medicoService = new MedicoService(new MedicoRepository());
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            FormNavegador.AbrirFormulario(this, formLogin);
        }

        private void FormRegistro_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string Role;
            string username = txtUsuario.Text.Trim();
            string password = txtClave.Text.Trim();
            string documentoStr = txtDocumento.Text.Trim();

            // Comprobar campos vacios
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(documentoStr))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Comprobar que documento solo sean numeros
            if (!int.TryParse(documentoStr, out int documento))
            {
                MessageBox.Show("El número de documento debe ser un número válido.");
                return;
            }

            // Verificar si ya existe el usuario
            var usuarios = usuarioService.Consultar();
            if (usuarios.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un usuario con ese nombre.");
                return;
            }

            // Obtener el id_persona usando el número de documento
            int idPersona = medicoService.ObtenerIdPersona(documento);  // del GenericPersonaService

            // Si devuelve TRUE entonces Rol es medico, sino, es admin
            if (medicoService.EsMedicoPorIdPersona(idPersona))
            {
                Role = "Medico";
            } else
            {
                Role = "Admin";
            }

            if (idPersona == -1)
            {
                MessageBox.Show("No se encontró una persona con ese número de documento.");
                return;
            }

            // Crea el usuario
            Usuario nuevoUsuario = new Usuario
            {
                Username = username,
                Contraseña = password,
                Rol = Role,
                IdPersona = idPersona
            };
            string resultado = usuarioService.Registrar(nuevoUsuario);
            MessageBox.Show(resultado);
            FormLogin formLogin = new FormLogin();
            FormNavegador.AbrirFormulario(this, formLogin);
        }
    }
}
