
namespace LojaTardigrado
{
    partial class editarProduto
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
            this.txtNomeProduto = new System.Windows.Forms.TextBox();
            this.txtTecido = new System.Windows.Forms.TextBox();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.txtCor = new System.Windows.Forms.TextBox();
            this.txtCusto = new System.Windows.Forms.TextBox();
            this.cmbTamanho = new System.Windows.Forms.ComboBox();
            this.cmbFornecedor = new System.Windows.Forms.ComboBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.lblPreco = new System.Windows.Forms.Label();
            this.lblTecido = new System.Windows.Forms.Label();
            this.lblCor = new System.Windows.Forms.Label();
            this.lblCusto = new System.Windows.Forms.Label();
            this.lblTamanho = new System.Windows.Forms.Label();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.lblITitulo = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.btnEditar = new System.Windows.Forms.Button();
            this.editInfo = new System.Windows.Forms.GroupBox();
            this.txtDescricao = new RoundedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnSubstituir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.editInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNomeProduto
            // 
            this.txtNomeProduto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeProduto.Location = new System.Drawing.Point(13, 59);
            this.txtNomeProduto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNomeProduto.Name = "txtNomeProduto";
            this.txtNomeProduto.Size = new System.Drawing.Size(148, 29);
            this.txtNomeProduto.TabIndex = 0;
            // 
            // txtTecido
            // 
            this.txtTecido.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTecido.Location = new System.Drawing.Point(206, 384);
            this.txtTecido.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTecido.Name = "txtTecido";
            this.txtTecido.Size = new System.Drawing.Size(76, 29);
            this.txtTecido.TabIndex = 1;
            // 
            // txtPreco
            // 
            this.txtPreco.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreco.Location = new System.Drawing.Point(110, 256);
            this.txtPreco.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(76, 29);
            this.txtPreco.TabIndex = 3;
            this.txtPreco.TextChanged += new System.EventHandler(this.txtPreco_TextChanged);
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Location = new System.Drawing.Point(17, 256);
            this.txtQuantidade.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(76, 29);
            this.txtQuantidade.TabIndex = 4;
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // txtCor
            // 
            this.txtCor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCor.Location = new System.Drawing.Point(110, 384);
            this.txtCor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCor.Name = "txtCor";
            this.txtCor.Size = new System.Drawing.Size(76, 29);
            this.txtCor.TabIndex = 5;
            // 
            // txtCusto
            // 
            this.txtCusto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCusto.Location = new System.Drawing.Point(206, 256);
            this.txtCusto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCusto.Name = "txtCusto";
            this.txtCusto.Size = new System.Drawing.Size(76, 29);
            this.txtCusto.TabIndex = 6;
            this.txtCusto.TextChanged += new System.EventHandler(this.txtCusto_TextChanged);
            // 
            // cmbTamanho
            // 
            this.cmbTamanho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTamanho.FormattingEnabled = true;
            this.cmbTamanho.Location = new System.Drawing.Point(124, 319);
            this.cmbTamanho.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbTamanho.Name = "cmbTamanho";
            this.cmbTamanho.Size = new System.Drawing.Size(67, 29);
            this.cmbTamanho.TabIndex = 7;
            // 
            // cmbFornecedor
            // 
            this.cmbFornecedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFornecedor.FormattingEnabled = true;
            this.cmbFornecedor.Location = new System.Drawing.Point(206, 319);
            this.cmbFornecedor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbFornecedor.Name = "cmbFornecedor";
            this.cmbFornecedor.Size = new System.Drawing.Size(100, 29);
            this.cmbFornecedor.TabIndex = 8;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblNome.Location = new System.Drawing.Point(10, 29);
            this.lblNome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(121, 19);
            this.lblNome.TabIndex = 9;
            this.lblNome.Text = "Nome do Produto";
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblQuantidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblQuantidade.Location = new System.Drawing.Point(15, 232);
            this.lblQuantidade.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(82, 19);
            this.lblQuantidade.TabIndex = 11;
            this.lblQuantidade.Text = "Quantidade";
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPreco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblPreco.Location = new System.Drawing.Point(108, 232);
            this.lblPreco.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(44, 19);
            this.lblPreco.TabIndex = 12;
            this.lblPreco.Text = "Preco";
            // 
            // lblTecido
            // 
            this.lblTecido.AutoSize = true;
            this.lblTecido.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblTecido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTecido.Location = new System.Drawing.Point(202, 365);
            this.lblTecido.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTecido.Name = "lblTecido";
            this.lblTecido.Size = new System.Drawing.Size(50, 19);
            this.lblTecido.TabIndex = 13;
            this.lblTecido.Text = "Tecido";
            // 
            // lblCor
            // 
            this.lblCor.AutoSize = true;
            this.lblCor.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblCor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCor.Location = new System.Drawing.Point(108, 365);
            this.lblCor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCor.Name = "lblCor";
            this.lblCor.Size = new System.Drawing.Size(31, 19);
            this.lblCor.TabIndex = 14;
            this.lblCor.Text = "Cor";
            // 
            // lblCusto
            // 
            this.lblCusto.AutoSize = true;
            this.lblCusto.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblCusto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCusto.Location = new System.Drawing.Point(204, 238);
            this.lblCusto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusto.Name = "lblCusto";
            this.lblCusto.Size = new System.Drawing.Size(45, 19);
            this.lblCusto.TabIndex = 15;
            this.lblCusto.Text = "Custo";
            // 
            // lblTamanho
            // 
            this.lblTamanho.AutoSize = true;
            this.lblTamanho.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblTamanho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTamanho.Location = new System.Drawing.Point(121, 302);
            this.lblTamanho.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTamanho.Name = "lblTamanho";
            this.lblTamanho.Size = new System.Drawing.Size(66, 19);
            this.lblTamanho.TabIndex = 16;
            this.lblTamanho.Text = "Tamanho";
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblFornecedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblFornecedor.Location = new System.Drawing.Point(202, 302);
            this.lblFornecedor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(79, 19);
            this.lblFornecedor.TabIndex = 17;
            this.lblFornecedor.Text = "Fornecedor";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(14, 319);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(107, 29);
            this.cmbCategoria.TabIndex = 19;
            this.cmbCategoria.SelectedIndexChanged += new System.EventHandler(this.cmbCategoria_SelectedIndexChanged);
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCategoria.Location = new System.Drawing.Point(14, 298);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(69, 19);
            this.lblCategoria.TabIndex = 20;
            this.lblCategoria.Text = "Categoria";
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblDesconto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblDesconto.Location = new System.Drawing.Point(14, 365);
            this.lblDesconto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(68, 19);
            this.lblDesconto.TabIndex = 22;
            this.lblDesconto.Text = "Desconto";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesconto.Location = new System.Drawing.Point(16, 384);
            this.txtDesconto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(76, 29);
            this.txtDesconto.TabIndex = 21;
            this.txtDesconto.TextChanged += new System.EventHandler(this.txtDesconto_TextChanged);
            this.txtDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDesconto_KeyPress);
            // 
            // lblITitulo
            // 
            this.lblITitulo.AutoSize = true;
            this.lblITitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblITitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblITitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblITitulo.Location = new System.Drawing.Point(282, 15);
            this.lblITitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblITitulo.Name = "lblITitulo";
            this.lblITitulo.Size = new System.Drawing.Size(173, 32);
            this.lblITitulo.TabIndex = 36;
            this.lblITitulo.Text = "Editar Produto";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::LojaTardigrado.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(232, 7);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 79;
            this.pictureBox2.TabStop = false;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblDescricao.Location = new System.Drawing.Point(10, 98);
            this.lblDescricao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(124, 19);
            this.lblDescricao.TabIndex = 81;
            this.lblDescricao.Text = "Descrição Produto";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(47, 59);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(197, 188);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 82;
            this.pictureBox1.TabStop = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(193, 273);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(37, 25);
            this.radioButton4.TabIndex = 88;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "4";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(79, 271);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(37, 25);
            this.radioButton1.TabIndex = 85;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(153, 272);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(37, 25);
            this.radioButton3.TabIndex = 87;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(113, 271);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(37, 25);
            this.radioButton2.TabIndex = 86;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(13, 441);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(135, 33);
            this.btnEditar.TabIndex = 89;
            this.btnEditar.Text = "Editar Informações";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // editInfo
            // 
            this.editInfo.BackColor = System.Drawing.Color.White;
            this.editInfo.Controls.Add(this.txtDescricao);
            this.editInfo.Controls.Add(this.txtNomeProduto);
            this.editInfo.Controls.Add(this.btnEditar);
            this.editInfo.Controls.Add(this.txtTecido);
            this.editInfo.Controls.Add(this.txtPreco);
            this.editInfo.Controls.Add(this.txtQuantidade);
            this.editInfo.Controls.Add(this.txtCor);
            this.editInfo.Controls.Add(this.txtCusto);
            this.editInfo.Controls.Add(this.cmbTamanho);
            this.editInfo.Controls.Add(this.cmbFornecedor);
            this.editInfo.Controls.Add(this.lblDescricao);
            this.editInfo.Controls.Add(this.lblNome);
            this.editInfo.Controls.Add(this.lblQuantidade);
            this.editInfo.Controls.Add(this.lblPreco);
            this.editInfo.Controls.Add(this.lblDesconto);
            this.editInfo.Controls.Add(this.lblTecido);
            this.editInfo.Controls.Add(this.txtDesconto);
            this.editInfo.Controls.Add(this.lblCor);
            this.editInfo.Controls.Add(this.lblCategoria);
            this.editInfo.Controls.Add(this.lblCusto);
            this.editInfo.Controls.Add(this.cmbCategoria);
            this.editInfo.Controls.Add(this.lblTamanho);
            this.editInfo.Controls.Add(this.lblFornecedor);
            this.editInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.editInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.editInfo.Location = new System.Drawing.Point(16, 63);
            this.editInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.editInfo.Name = "editInfo";
            this.editInfo.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.editInfo.Size = new System.Drawing.Size(321, 488);
            this.editInfo.TabIndex = 90;
            this.editInfo.TabStop = false;
            this.editInfo.Text = "Informações";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescricao.ForeColor = System.Drawing.Color.Black;
            this.txtDescricao.HintText = null;
            this.txtDescricao.Location = new System.Drawing.Point(13, 119);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.txtDescricao.Size = new System.Drawing.Size(288, 97);
            this.txtDescricao.TabIndex = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnExcluir);
            this.groupBox1.Controls.Add(this.btnSubstituir);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.groupBox1.Location = new System.Drawing.Point(351, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(290, 488);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagem";
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnExcluir.FlatAppearance.BorderSize = 0;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExcluir.ForeColor = System.Drawing.Color.White;
            this.btnExcluir.Location = new System.Drawing.Point(166, 340);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(118, 33);
            this.btnExcluir.TabIndex = 91;
            this.btnExcluir.Text = "Excluir Imagem";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnSubstituir
            // 
            this.btnSubstituir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnSubstituir.FlatAppearance.BorderSize = 0;
            this.btnSubstituir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubstituir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSubstituir.ForeColor = System.Drawing.Color.White;
            this.btnSubstituir.Location = new System.Drawing.Point(16, 340);
            this.btnSubstituir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSubstituir.Name = "btnSubstituir";
            this.btnSubstituir.Size = new System.Drawing.Size(118, 33);
            this.btnSubstituir.TabIndex = 90;
            this.btnSubstituir.Text = "Editar imagem";
            this.btnSubstituir.UseVisualStyleBackColor = false;
            this.btnSubstituir.Click += new System.EventHandler(this.btnSubstituir_Click);
            // 
            // editarProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(231)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(655, 609);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.editInfo);
            this.Controls.Add(this.lblITitulo);
            this.Controls.Add(this.pictureBox2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "editarProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "editarProduto";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.editInfo.ResumeLayout(false);
            this.editInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNomeProduto;
        private System.Windows.Forms.TextBox txtTecido;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.TextBox txtCor;
        private System.Windows.Forms.TextBox txtCusto;
        private System.Windows.Forms.ComboBox cmbTamanho;
        private System.Windows.Forms.ComboBox cmbFornecedor;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.Label lblTecido;
        private System.Windows.Forms.Label lblCor;
        private System.Windows.Forms.Label lblCusto;
        private System.Windows.Forms.Label lblTamanho;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.Label lblITitulo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private RoundedTextBox txtDescricao;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.GroupBox editInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnSubstituir;
    }
}