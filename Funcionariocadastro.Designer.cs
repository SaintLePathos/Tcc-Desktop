
namespace LojaTardigrado
{
    partial class Funcionariocadastro
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
            this.txtbNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbUsuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbSenha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbConfirmasenha = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbbCargo = new System.Windows.Forms.ComboBox();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbNome
            // 
            this.txtbNome.Location = new System.Drawing.Point(141, 33);
            this.txtbNome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbNome.Name = "txtbNome";
            this.txtbNome.Size = new System.Drawing.Size(368, 22);
            this.txtbNome.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usuario:";
            // 
            // txtbUsuario
            // 
            this.txtbUsuario.Location = new System.Drawing.Point(141, 97);
            this.txtbUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbUsuario.Name = "txtbUsuario";
            this.txtbUsuario.Size = new System.Drawing.Size(368, 22);
            this.txtbUsuario.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Senha:";
            // 
            // txtbSenha
            // 
            this.txtbSenha.Location = new System.Drawing.Point(141, 129);
            this.txtbSenha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbSenha.Name = "txtbSenha";
            this.txtbSenha.PasswordChar = '*';
            this.txtbSenha.Size = new System.Drawing.Size(368, 22);
            this.txtbSenha.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 165);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Confirmar Senha:";
            // 
            // txtbConfirmasenha
            // 
            this.txtbConfirmasenha.Location = new System.Drawing.Point(141, 161);
            this.txtbConfirmasenha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbConfirmasenha.Name = "txtbConfirmasenha";
            this.txtbConfirmasenha.PasswordChar = '*';
            this.txtbConfirmasenha.Size = new System.Drawing.Size(368, 22);
            this.txtbConfirmasenha.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cargo:";
            // 
            // cmbbCargo
            // 
            this.cmbbCargo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbCargo.FormattingEnabled = true;
            this.cmbbCargo.Location = new System.Drawing.Point(141, 65);
            this.cmbbCargo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbbCargo.Name = "cmbbCargo";
            this.cmbbCargo.Size = new System.Drawing.Size(193, 24);
            this.cmbbCargo.TabIndex = 10;
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Location = new System.Drawing.Point(460, 197);
            this.btnCadastrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(151, 43);
            this.btnCadastrar.TabIndex = 11;
            this.btnCadastrar.Text = "Cadastrar";
            this.btnCadastrar.UseVisualStyleBackColor = true;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // Funcionariocadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(627, 255);
            this.Controls.Add(this.btnCadastrar);
            this.Controls.Add(this.cmbbCargo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtbConfirmasenha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtbSenha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbUsuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbNome);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Funcionariocadastro";
            this.Text = "Funcionariocadastro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbSenha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbConfirmasenha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbbCargo;
        private System.Windows.Forms.Button btnCadastrar;
    }
}