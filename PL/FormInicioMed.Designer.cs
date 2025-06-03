namespace CitasBot
{
    partial class FormInicioMed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInicioMed));
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnCrearHC = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnHC = new System.Windows.Forms.Button();
            this.btnBuscarPaciente = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(152, 132);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(78, 19);
            this.lblWelcome.TabIndex = 3;
            this.lblWelcome.Text = "Bienvenido";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCrearHC
            // 
            this.btnCrearHC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCrearHC.BackColor = System.Drawing.Color.Transparent;
            this.btnCrearHC.BackgroundImage = global::CitasBot.Properties.Resources.btn_app2;
            this.btnCrearHC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCrearHC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearHC.FlatAppearance.BorderSize = 0;
            this.btnCrearHC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCrearHC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCrearHC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearHC.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnCrearHC.ForeColor = System.Drawing.Color.White;
            this.btnCrearHC.Location = new System.Drawing.Point(186, 374);
            this.btnCrearHC.Name = "btnCrearHC";
            this.btnCrearHC.Size = new System.Drawing.Size(250, 40);
            this.btnCrearHC.TabIndex = 5;
            this.btnCrearHC.Text = "Crear Historia Clínica";
            this.btnCrearHC.UseVisualStyleBackColor = false;
            this.btnCrearHC.Click += new System.EventHandler(this.btnCrearHC_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::CitasBot.Properties.Resources.remindiBTN5;
            this.pictureBox2.Location = new System.Drawing.Point(186, 265);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 110);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::CitasBot.Properties.Resources.remindiBTN3;
            this.pictureBox1.Location = new System.Drawing.Point(479, 265);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 110);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnHC
            // 
            this.btnHC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHC.BackColor = System.Drawing.Color.Transparent;
            this.btnHC.BackgroundImage = global::CitasBot.Properties.Resources.btn_app2;
            this.btnHC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHC.FlatAppearance.BorderSize = 0;
            this.btnHC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHC.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnHC.ForeColor = System.Drawing.Color.White;
            this.btnHC.Location = new System.Drawing.Point(479, 374);
            this.btnHC.Name = "btnHC";
            this.btnHC.Size = new System.Drawing.Size(250, 40);
            this.btnHC.TabIndex = 5;
            this.btnHC.Text = "Mis Historias Clínicas";
            this.btnHC.UseVisualStyleBackColor = false;
            this.btnHC.Click += new System.EventHandler(this.btnHC_Click);
            // 
            // btnBuscarPaciente
            // 
            this.btnBuscarPaciente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuscarPaciente.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscarPaciente.BackgroundImage = global::CitasBot.Properties.Resources.btn_app;
            this.btnBuscarPaciente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscarPaciente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarPaciente.FlatAppearance.BorderSize = 0;
            this.btnBuscarPaciente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.btnBuscarPaciente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnBuscarPaciente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarPaciente.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarPaciente.ForeColor = System.Drawing.Color.White;
            this.btnBuscarPaciente.Image = global::CitasBot.Properties.Resources.buscarIcon;
            this.btnBuscarPaciente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarPaciente.Location = new System.Drawing.Point(328, 502);
            this.btnBuscarPaciente.Name = "btnBuscarPaciente";
            this.btnBuscarPaciente.Size = new System.Drawing.Size(250, 40);
            this.btnBuscarPaciente.TabIndex = 7;
            this.btnBuscarPaciente.Text = "Consultar paciente   ";
            this.btnBuscarPaciente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarPaciente.UseVisualStyleBackColor = false;
            this.btnBuscarPaciente.Visible = false;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.BackgroundImage = global::CitasBot.Properties.Resources.btn_app;
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(625, 128);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(130, 27);
            this.btnLogout.TabIndex = 20;
            this.btnLogout.Text = "Cerrar sesión";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(181, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 30);
            this.label6.TabIndex = 21;
            this.label6.Text = "G E S T I O N";
            // 
            // FormInicioMed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(9)))));
            this.BackgroundImage = global::CitasBot.Properties.Resources.remindiapp_bgLogin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(906, 583);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnBuscarPaciente);
            this.Controls.Add(this.btnHC);
            this.Controls.Add(this.btnCrearHC);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblWelcome);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(922, 622);
            this.Name = "FormInicioMed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remindi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormInicioMed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnCrearHC;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnHC;
        private System.Windows.Forms.Button btnBuscarPaciente;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label label6;
    }
}