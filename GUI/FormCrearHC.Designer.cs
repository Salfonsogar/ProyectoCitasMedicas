﻿namespace GUI
{
    partial class FormCrearHC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCrearHC));
            this.lblHC = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.cmbCausaExterna = new System.Windows.Forms.ComboBox();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.txtExamenFisico = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtSignosVitales = new System.Windows.Forms.TextBox();
            this.txtEvolucion = new System.Windows.Forms.TextBox();
            this.txtMotivoConsulta = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHC
            // 
            this.lblHC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHC.AutoSize = true;
            this.lblHC.BackColor = System.Drawing.Color.Transparent;
            this.lblHC.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblHC.ForeColor = System.Drawing.Color.White;
            this.lblHC.Location = new System.Drawing.Point(509, 169);
            this.lblHC.Name = "lblHC";
            this.lblHC.Size = new System.Drawing.Size(122, 19);
            this.lblHC.TabIndex = 34;
            this.lblHC.Text = "Historia clínica de";
            this.lblHC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.BackgroundImage = global::GUI.Properties.Resources.btn_app;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(716, 488);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(116, 35);
            this.btnGuardar.TabIndex = 32;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNuevo.BackColor = System.Drawing.Color.Transparent;
            this.btnNuevo.BackgroundImage = global::GUI.Properties.Resources.btn_app;
            this.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(589, 488);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(116, 35);
            this.btnNuevo.TabIndex = 33;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // cmbCausaExterna
            // 
            this.cmbCausaExterna.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbCausaExterna.BackColor = System.Drawing.Color.SteelBlue;
            this.cmbCausaExterna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCausaExterna.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCausaExterna.FormattingEnabled = true;
            this.cmbCausaExterna.Items.AddRange(new object[] {
            "Ninguna",
            "Accidente de tránsito",
            "Accidente de trabajo",
            "Accidente doméstico",
            "Otro tipo de accidente",
            "Lesión por agresión",
            "Lesión autoinfligida",
            "Evento catastrófico",
            "Violencia intrafamiliar",
            "Violencia sexual",
            "Conflicto armado",
            "Otra causa externa"});
            this.cmbCausaExterna.Location = new System.Drawing.Point(478, 295);
            this.cmbCausaExterna.Name = "cmbCausaExterna";
            this.cmbCausaExterna.Size = new System.Drawing.Size(354, 25);
            this.cmbCausaExterna.TabIndex = 31;
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDiagnostico.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiagnostico.Location = new System.Drawing.Point(478, 415);
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.Size = new System.Drawing.Size(354, 54);
            this.txtDiagnostico.TabIndex = 29;
            // 
            // txtExamenFisico
            // 
            this.txtExamenFisico.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtExamenFisico.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtExamenFisico.Location = new System.Drawing.Point(120, 445);
            this.txtExamenFisico.Multiline = true;
            this.txtExamenFisico.Name = "txtExamenFisico";
            this.txtExamenFisico.Size = new System.Drawing.Size(330, 54);
            this.txtExamenFisico.TabIndex = 28;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.Location = new System.Drawing.Point(120, 359);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(330, 54);
            this.txtDescripcion.TabIndex = 27;
            // 
            // txtSignosVitales
            // 
            this.txtSignosVitales.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSignosVitales.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSignosVitales.Location = new System.Drawing.Point(478, 353);
            this.txtSignosVitales.Name = "txtSignosVitales";
            this.txtSignosVitales.Size = new System.Drawing.Size(354, 25);
            this.txtSignosVitales.TabIndex = 26;
            // 
            // txtEvolucion
            // 
            this.txtEvolucion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEvolucion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEvolucion.Location = new System.Drawing.Point(478, 236);
            this.txtEvolucion.Name = "txtEvolucion";
            this.txtEvolucion.Size = new System.Drawing.Size(354, 25);
            this.txtEvolucion.TabIndex = 25;
            // 
            // txtMotivoConsulta
            // 
            this.txtMotivoConsulta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMotivoConsulta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMotivoConsulta.Location = new System.Drawing.Point(120, 274);
            this.txtMotivoConsulta.Multiline = true;
            this.txtMotivoConsulta.Name = "txtMotivoConsulta";
            this.txtMotivoConsulta.Size = new System.Drawing.Size(330, 54);
            this.txtMotivoConsulta.TabIndex = 30;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(402, 169);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(83, 25);
            this.btnBuscar.TabIndex = 24;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(474, 387);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 21);
            this.label8.TabIndex = 23;
            this.label8.Text = "Diagnóstico:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(116, 420);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Exámen físico:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(116, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Descripción:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(474, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Causa externa:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(474, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Signos vitales:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(474, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Evolución:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(116, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Motivo consulta:";
            // 
            // lblNombrePaciente
            // 
            this.lblNombrePaciente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNombrePaciente.AutoSize = true;
            this.lblNombrePaciente.BackColor = System.Drawing.Color.Transparent;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblNombrePaciente.ForeColor = System.Drawing.Color.White;
            this.lblNombrePaciente.Location = new System.Drawing.Point(115, 212);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(188, 25);
            this.lblNombrePaciente.TabIndex = 16;
            this.lblNombrePaciente.Text = "Nombre del paciente";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(115, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Paciente:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDocumento
            // 
            this.txtDocumento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDocumento.BackColor = System.Drawing.Color.White;
            this.txtDocumento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDocumento.Location = new System.Drawing.Point(207, 169);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(185, 25);
            this.txtDocumento.TabIndex = 14;
            this.txtDocumento.Text = "N# Documento";
            // 
            // btnRegresar
            // 
            this.btnRegresar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegresar.BackColor = System.Drawing.Color.Transparent;
            this.btnRegresar.BackgroundImage = global::GUI.Properties.Resources.btn_app;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.FlatAppearance.BorderSize = 0;
            this.btnRegresar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.Color.White;
            this.btnRegresar.Location = new System.Drawing.Point(677, 52);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(155, 40);
            this.btnRegresar.TabIndex = 13;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // FormCrearHC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(9)))));
            this.BackgroundImage = global::GUI.Properties.Resources.remindiappCrearHC_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(906, 583);
            this.Controls.Add(this.lblHC);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.cmbCausaExterna);
            this.Controls.Add(this.txtDiagnostico);
            this.Controls.Add(this.txtExamenFisico);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtSignosVitales);
            this.Controls.Add(this.txtEvolucion);
            this.Controls.Add(this.txtMotivoConsulta);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNombrePaciente);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.btnRegresar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(922, 622);
            this.Name = "FormCrearHC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remindi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormCrearHC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHC;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.ComboBox cmbCausaExterna;
        private System.Windows.Forms.TextBox txtDiagnostico;
        private System.Windows.Forms.TextBox txtExamenFisico;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtSignosVitales;
        private System.Windows.Forms.TextBox txtEvolucion;
        private System.Windows.Forms.TextBox txtMotivoConsulta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Button btnRegresar;
    }
}