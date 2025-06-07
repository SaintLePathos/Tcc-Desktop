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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace LojaTardigrado
{
    public partial class Form4 : Form
    {

        ClasseConexao con;
        List<string> caminhosLocaisImagens = new List<string>();


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
            caminhosLocaisImagens.Clear();
        }

        private async void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (CamposValidos())
            {
                if (caminhosLocaisImagens.Count == 0)
                {
                    MessageBox.Show("Selecione ao menos uma imagem antes de adicionar o produto.");
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
                         Tamanho_Produto, Quantidade_Produto, Tecido_Produto, Cor_Produto)
                        VALUES
                        (@idFornecedor, @nomeProduto, @descricao, @preco, 
                         @tamanho, @quantidade, @tecido, @cor);
                        SELECT SCOPE_IDENTITY();");

                    cmdInsert.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                    cmdInsert.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                    cmdInsert.Parameters.AddWithValue("@descricao", descricao);
                    cmdInsert.Parameters.AddWithValue("@preco", preco);
                    cmdInsert.Parameters.AddWithValue("@tamanho", tamanho);
                    cmdInsert.Parameters.AddWithValue("@quantidade", quantidade);
                    cmdInsert.Parameters.AddWithValue("@tecido", tecido);
                    cmdInsert.Parameters.AddWithValue("@cor", cor);

                    object novoIdObj = con.executarScalar(cmdInsert);
                    if (novoIdObj == null)
                    {
                        MessageBox.Show("Erro ao inserir produto.");
                        return;
                    }

                    int idProduto = Convert.ToInt32(novoIdObj);
                    bool todasInseridas = true;

                    for (int i = 0; i < caminhosLocaisImagens.Count; i++)
                    {
                        string caminhoRelativo = await EnviarImagemParaServidor(caminhosLocaisImagens[i]);

                        if (!string.IsNullOrEmpty(caminhoRelativo))
                        {
                            SqlCommand cmdImg = new SqlCommand(@"
                                INSERT INTO Imagem_Produto (Id_Produto, Url_ImgProduto, Ordem_ImgProduto)
                                VALUES (@idProduto, @urlImagem, @ordem)");

                            cmdImg.Parameters.AddWithValue("@idProduto", idProduto);
                            cmdImg.Parameters.AddWithValue("@urlImagem", caminhoRelativo);
                            cmdImg.Parameters.AddWithValue("@ordem", i);

                            int imgResult = con.manutencaoDB_Parametros(cmdImg);
                            if (imgResult <= 0)
                                todasInseridas = false;
                        }
                        else
                        {
                            todasInseridas = false;
                        }
                    }

                    if (todasInseridas)
                    {
                        LimparCampos();
                        MessageBox.Show("Produto e imagens inseridos com sucesso!");
                        ExibirProdutos();
                    }
                    else
                    {
                        MessageBox.Show("Produto inserido, mas erro ao salvar uma ou mais imagens.");
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
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileNames.Length > 4)
                {
                    MessageBox.Show("Selecione no máximo 4 imagens.");
                    return;
                }

                caminhosLocaisImagens.Clear();
                caminhosLocaisImagens.AddRange(ofd.FileNames);
                MessageBox.Show("Imagens selecionadas. Elas serão enviadas ao adicionar o produto.");
            }
        }





        private async Task<string> EnviarImagemParaServidor(string caminhoLocal)
        {
            using (var client = new HttpClient())
            {
                var form = new MultipartFormDataContent();
                var conteudo = new ByteArrayContent(File.ReadAllBytes(caminhoLocal));

                string extensao = Path.GetExtension(caminhoLocal).ToLower();
                string contentType;

                switch (extensao)
                {
                    case ".jpg":
                    case ".jpeg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    default:
                        MessageBox.Show("Tipo de imagem não suportado.");
                        return null;
                }

                conteudo.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                form.Add(conteudo, "file", Path.GetFileName(caminhoLocal));

                string url = "http://192.168.0.75/Tcc-Web/Assets/php/upload.php";

                HttpResponseMessage resposta = await client.PostAsync(url, form);
                string respostaJson = await resposta.Content.ReadAsStringAsync();

                if (resposta.IsSuccessStatusCode)
                {
                    dynamic resultado = JsonConvert.DeserializeObject(respostaJson);
                    return (string)resultado.path;
                }
                else
                {
                    MessageBox.Show("Erro ao enviar imagem: " + respostaJson);
                    return null;
                }
            }
        }



    }
}

