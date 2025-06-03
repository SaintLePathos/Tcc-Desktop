using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace LojaTardigrado
{
    public partial class Form4 : Form
    {

        ClasseConexao con;
        string caminhoRelativoImagem = "";

        public Form4()
        {
            InitializeComponent();

            con = new ClasseConexao();

            ExibirProdutos();
            ExbirFornecedor();
            CarregarTamanhos();


        }

        private void CarregarTamanhos()
        {
            string[] tamanhos = { "PP", "P", "M", "G", "GG" };
            cmbTamanho.Items.AddRange(tamanhos);
            cmbTamanho.SelectedIndex = 0;
        }
        private void ExibirProdutos()
        {

            try
            {
                DataTable dt = con.executarSQL("SELECT * FROM Produto");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exibir produtos: " + ex.Message);
            }


        }
        
        private void ExbirFornecedor()
        {

            try
            {
                cmbFornecedor.Items.Clear();
                DataTable dt = con.executarSQL("SELECT Nome_Fornecedor FROM Fornecedor");

                foreach (DataRow row in dt.Rows)
                {
                    cmbFornecedor.Items.Add(row["Nome_Fornecedor"].ToString());
                }

                if (cmbFornecedor.Items.Count > 0)
                    cmbFornecedor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message);
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private bool CamposValidos()
        {
            return !string.IsNullOrWhiteSpace(txtDescricao.Text) &&
                   !string.IsNullOrWhiteSpace(txtNomeProduto.Text) &&
                   !string.IsNullOrWhiteSpace(txtPreco.Text) &&
                   !string.IsNullOrWhiteSpace(txtQuantidade.Text) &&
                   !string.IsNullOrWhiteSpace(txtTecido.Text) &&
                   !string.IsNullOrWhiteSpace(txtCor.Text) &&
                   cmbTamanho.SelectedIndex >= 0 &&
                   cmbFornecedor.SelectedIndex >= 0;
        }
        private void LimparCampos()
        {
            txtNomeProduto.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
            txtQuantidade.Clear();
            txtTecido.Clear();
            txtCor.Clear();
            cmbTamanho.SelectedIndex = 0;
            cmbFornecedor.SelectedIndex = 0;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (CamposValidos())
            {
                if (string.IsNullOrEmpty(caminhoRelativoImagem))
                {
                    MessageBox.Show("Selecione uma imagem antes de adicionar o produto.");
                    return;
                }

                try
                {
                    string nomeProduto = txtNomeProduto.Text.Trim();
                    string descricao = txtDescricao.Text.Trim();
                    decimal preco = decimal.Parse(txtPreco.Text);
                    int quantidade = int.Parse(txtQuantidade.Text);
                    string tecido = txtTecido.Text.Trim();
                    string cor = txtCor.Text.Trim();
                    string tamanho = cmbTamanho.Text;
                    string nomeFornecedor = cmbFornecedor.Text;

                    SqlCommand cmdSelect = new SqlCommand("SELECT Id_Fornecedor FROM Fornecedor WHERE Nome_Fornecedor = @nome");
                    cmdSelect.Parameters.AddWithValue("@nome", nomeFornecedor);

                    DataTable dtFornecedor = con.exSQLParametros(cmdSelect);

                    if (dtFornecedor.Rows.Count == 0)
                    {
                        MessageBox.Show("Fornecedor não encontrado!");
                        return;
                    }

                    int idFornecedor = Convert.ToInt32(dtFornecedor.Rows[0]["Id_Fornecedor"]);

                    SqlCommand cmdInsert = new SqlCommand(@"
                    INSERT INTO Produto 
                    (Id_Fornecedor, Nome_Produto, Descricao_Produto, Valor_Produto, 
                     Tamanho_Produto, Quantidade_Produto, Tecido_Produto, Cor_Produto,Img_Produto)
                    VALUES
                    (@idFornecedor, @nomeProduto, @descricao, @preco, 
                     @tamanho, @quantidade, @tecido, @cor, @imagemPath)");

                    cmdInsert.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                    cmdInsert.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                    cmdInsert.Parameters.AddWithValue("@descricao", descricao);
                    cmdInsert.Parameters.AddWithValue("@preco", preco);
                    cmdInsert.Parameters.AddWithValue("@tamanho", tamanho);
                    cmdInsert.Parameters.AddWithValue("@quantidade", quantidade);
                    cmdInsert.Parameters.AddWithValue("@tecido", tecido);
                    cmdInsert.Parameters.AddWithValue("@cor", cor);
                    cmdInsert.Parameters.AddWithValue("@imagemPath", caminhoRelativoImagem);

                    int resultado = con.manutencaoDB_Parametros(cmdInsert);

                    if (resultado > 0)
                    {
                        LimparCampos();
                        MessageBox.Show("Produto inserido com sucesso!");
                        ExibirProdutos();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao inserir produto.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos obrigatórios!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnSelecionarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagens (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string origem = ofd.FileName;
                    string pastaWeb = @"C:\xampp\htdocs\Tcc-Web\uploads\imgProduto";

                    if (!Directory.Exists(pastaWeb))
                    {
                        Directory.CreateDirectory(pastaWeb);
                    }

                    string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(origem);
                    string destino = Path.Combine(pastaWeb, nomeArquivo);

                    File.Copy(origem, destino, true);

                    // Salvar o caminho relativo para uso posterior no INSERT
                    caminhoRelativoImagem = "uploads/imgProduto/" + nomeArquivo;

                    // Exibir imagem no PictureBox, se quiser:
                    //pictureBox1.ImageLocation = destino;

                    MessageBox.Show("Imagem salva com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao copiar imagem: " + ex.Message);
                }
            }

        }
    }
}

