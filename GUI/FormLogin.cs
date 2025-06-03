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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            //AplicarPlaceholder(txtUsuario, "Usuario");
            //AplicarPlaceholder(txtClave, "Contraseña");

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
            txtClave.UseSystemPasswordChar = true;

            // Arreglo de imágenes desde los recursos
            Image[] imagenes = {
            Properties.Resources.loginBG1,
            Properties.Resources.loginBG2,
            Properties.Resources.loginBG3
            };

            Random rnd = new Random();
            int indice = rnd.Next(imagenes.Length);

            this.BackgroundImage = imagenes[indice];
            this.BackgroundImageLayout = ImageLayout.Stretch;

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

        // logica de login para reutilizar


        

        private void lblClaveOlvidada_Click(object sender, EventArgs e)
        {
            FormRegistro formRegistro = new FormRegistro();
            FormNavegador.AbrirFormulario(this, formRegistro);
        }

        private async void btnIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string clave = txtClave.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor, ingresa usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario user = await usuarioRepo.ValidarCredenciales(usuario, clave);

            if (user != null)
            {
                if (user.Rol == "Admin")
                {
                    Sesion.UsuarioActual = user;
                    FormInicio formInicio = new FormInicio();
                    FormNavegador.AbrirFormulario(this, formInicio);
                }
                else if (user.Rol == "Medico")
                {
                    Sesion.UsuarioActual = user;
                    FormInicioMed formInicioMed = new FormInicioMed();
                    FormNavegador.AbrirFormulario(this, formInicioMed);
                }
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblClaveOlvidada_Click_1(object sender, EventArgs e)
        {
            FormRegistro formRegistro = new FormRegistro();
            FormNavegador.AbrirFormulario(this, formRegistro);
        }
    }
}
