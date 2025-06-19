
namespace LojaTardigrado
{
    partial class Fornecedor
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
            this.txtCnpj = new System.Windows.Forms.MaskedTextBox();
            this.txtNomeFornecedor = new System.Windows.Forms.TextBox();
            this.txtEmailFornecedor = new System.Windows.Forms.TextBox();
            this.txtTelefoneFornecedor = new System.Windows.Forms.MaskedTextBox();
            this.lblITitulo = new System.Windows.Forms.Label();
            this.lblCNPJ = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.dgvFornecedores = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCnpj
            // 
            this.txtCnpj.Location = new System.Drawing.Point(100, 140);
            this.txtCnpj.Mask = "00.000.000/0000-00";
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(180, 30);
            this.txtCnpj.TabIndex = 2;
            // 
            // txtNomeFornecedor
            // 
            this.txtNomeFornecedor.Location = new System.Drawing.Point(320, 140);
            this.txtNomeFornecedor.Name = "txtNomeFornecedor";
            this.txtNomeFornecedor.Size = new System.Drawing.Size(180, 30);
            this.txtNomeFornecedor.TabIndex = 4;
            // 
            // txtEmailFornecedor
            // 
            this.txtEmailFornecedor.Location = new System.Drawing.Point(100, 230);
            this.txtEmailFornecedor.Name = "txtEmailFornecedor";
            this.txtEmailFornecedor.Size = new System.Drawing.Size(180, 30);
            this.txtEmailFornecedor.TabIndex = 6;
            // 
            // txtTelefoneFornecedor
            // 
            this.txtTelefoneFornecedor.Location = new System.Drawing.Point(319, 230);
            this.txtTelefoneFornecedor.Mask = "(00) 00000-0000";
            this.txtTelefoneFornecedor.Name = "txtTelefoneFornecedor";
            this.txtTelefoneFornecedor.Size = new System.Drawing.Size(180, 30);
            this.txtTelefoneFornecedor.TabIndex = 8;
            // 
            // lblITitulo
            // 
            this.lblITitulo.AutoSize = true;
            this.lblITitulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblITitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblITitulo.ForeColor = System.Drawing.Color.Black;
            this.lblITitulo.Location = new System.Drawing.Point(250, 30);
            this.lblITitulo.Name = "lblITitulo";
            this.lblITitulo.Size = new System.Drawing.Size(345, 41);
            this.lblITitulo.TabIndex = 0;
            this.lblITitulo.Text = "Cadastro de Fornecedor";
            // 
            // lblCNPJ
            // 
            this.lblCNPJ.AutoSize = true;
            this.lblCNPJ.Location = new System.Drawing.Point(100, 110);
            this.lblCNPJ.Name = "lblCNPJ";
            this.lblCNPJ.Size = new System.Drawing.Size(49, 23);
            this.lblCNPJ.TabIndex = 1;
            this.lblCNPJ.Text = "CNPJ";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(320, 110);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(57, 23);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "Nome";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(100, 200);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 23);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email";
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(320, 200);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(73, 23);
            this.lblTelefone.TabIndex = 7;
            this.lblTelefone.Text = "Telefone";
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.BackColor = System.Drawing.Color.Black;
            this.btnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCadastrar.ForeColor = System.Drawing.Color.White;
            this.btnCadastrar.Location = new System.Drawing.Point(557, 538);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(200, 50);
            this.btnCadastrar.TabIndex = 9;
            this.btnCadastrar.Text = "Cadastrar Fornecedor";
            this.btnCadastrar.UseVisualStyleBackColor = false;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // dgvFornecedores
            // 
            this.dgvFornecedores.AllowUserToAddRows = false;
            this.dgvFornecedores.AllowUserToDeleteRows = false;
            this.dgvFornecedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFornecedores.ColumnHeadersHeight = 29;
            this.dgvFornecedores.Location = new System.Drawing.Point(97, 330);
            this.dgvFornecedores.Name = "dgvFornecedores";
            this.dgvFornecedores.ReadOnly = true;
            this.dgvFornecedores.RowHeadersWidth = 51;
            this.dgvFornecedores.Size = new System.Drawing.Size(625, 179);
            this.dgvFornecedores.TabIndex = 0;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Black;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(71, 538);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(200, 50);
            this.btnEditar.TabIndex = 10;
            this.btnEditar.Text = "Editar ";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(542, 140);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(180, 30);
            this.textBox3.TabIndex = 25;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 294);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(180, 30);
            this.textBox2.TabIndex = 24;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(542, 227);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 30);
            this.textBox1.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(538, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 23);
            this.label6.TabIndex = 22;
            this.label6.Text = "nome contato";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 23);
            this.label5.TabIndex = 21;
            this.label5.Text = "endereço";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(538, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "razão social";
            // 
            // Fornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.dgvFornecedores);
            this.Controls.Add(this.lblITitulo);
            this.Controls.Add(this.lblCNPJ);
            this.Controls.Add(this.txtCnpj);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNomeFornecedor);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmailFornecedor);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefoneFornecedor);
            this.Controls.Add(this.btnCadastrar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "Fornecedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Fornecedor";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFornecedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txtCnpj;
        private System.Windows.Forms.TextBox txtNomeFornecedor;
        private System.Windows.Forms.TextBox txtEmailFornecedor;
        private System.Windows.Forms.MaskedTextBox txtTelefoneFornecedor;
        private System.Windows.Forms.Label lblITitulo;
        private System.Windows.Forms.Label lblCNPJ;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.DataGridView dgvFornecedores;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}