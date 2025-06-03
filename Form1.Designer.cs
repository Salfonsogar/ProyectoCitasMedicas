namespace CitasBot
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            this.lblCitaEps = new System.Windows.Forms.Label();
            this.lblCitaEspecialidad = new System.Windows.Forms.Label();
            this.lblCitaFechaHora = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombrePaciente
            // 
            this.lblNombrePaciente.AutoSize = true;
            this.lblNombrePaciente.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombrePaciente.ForeColor = System.Drawing.Color.White;
            this.lblNombrePaciente.Location = new System.Drawing.Point(314, 283);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(130, 21);
            this.lblNombrePaciente.TabIndex = 4;
            this.lblNombrePaciente.Text = "nombre_paciente";
            this.lblNombrePaciente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCitaEps
            // 
            this.lblCitaEps.AutoSize = true;
            this.lblCitaEps.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblCitaEps.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCitaEps.ForeColor = System.Drawing.Color.White;
            this.lblCitaEps.Location = new System.Drawing.Point(312, 253);
            this.lblCitaEps.Name = "lblCitaEps";
            this.lblCitaEps.Size = new System.Drawing.Size(34, 21);
            this.lblCitaEps.TabIndex = 5;
            this.lblCitaEps.Text = "eps";
            this.lblCitaEps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCitaEspecialidad
            // 
            this.lblCitaEspecialidad.AutoSize = true;
            this.lblCitaEspecialidad.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblCitaEspecialidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCitaEspecialidad.ForeColor = System.Drawing.Color.White;
            this.lblCitaEspecialidad.Location = new System.Drawing.Point(312, 222);
            this.lblCitaEspecialidad.Name = "lblCitaEspecialidad";
            this.lblCitaEspecialidad.Size = new System.Drawing.Size(95, 21);
            this.lblCitaEspecialidad.TabIndex = 6;
            this.lblCitaEspecialidad.Text = "especialidad";
            this.lblCitaEspecialidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCitaFechaHora
            // 
            this.lblCitaFechaHora.AutoSize = true;
            this.lblCitaFechaHora.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblCitaFechaHora.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCitaFechaHora.ForeColor = System.Drawing.Color.White;
            this.lblCitaFechaHora.Location = new System.Drawing.Point(312, 192);
            this.lblCitaFechaHora.Name = "lblCitaFechaHora";
            this.lblCitaFechaHora.Size = new System.Drawing.Size(86, 21);
            this.lblCitaFechaHora.TabIndex = 7;
            this.lblCitaFechaHora.Text = "fecha_hora";
            this.lblCitaFechaHora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(352, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Próxima cita:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.RoyalBlue;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox2.Image = global::CitasBot.Properties.Resources.bell_icon;
            this.pictureBox2.Location = new System.Drawing.Point(316, 147);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblNombrePaciente);
            this.Controls.Add(this.lblCitaEps);
            this.Controls.Add(this.lblCitaEspecialidad);
            this.Controls.Add(this.lblCitaFechaHora);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.Label lblCitaEps;
        private System.Windows.Forms.Label lblCitaEspecialidad;
        private System.Windows.Forms.Label lblCitaFechaHora;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

