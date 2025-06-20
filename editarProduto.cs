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
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace LojaTardigrado
{
    public partial class editarProduto : Form
    {
        ClasseConexao con;
        private int idProduto;
        private List<string> listaImagens = new List<string>();

        public editarProduto(int idProduto, string nomeProduto, string descricao, decimal preco, int quantidade, string tecido, string cor, decimal custo, string tamanho, string fornecedor, string categoria, int desconto)
        {
            InitializeComponent();
            con = new ClasseConexao();
            ExbirFornecedor();
            CarregarCategorias();

            this.idProduto = idProduto;
            CarregarImagensProduto();

            string categoriaDetectada = tamanhosPorCategoria
                .FirstOrDefault(kvp => kvp.Value.Contains(tamanho)).Key;

            if (!string.IsNullOrEmpty(categoriaDetectada))
            {
                cmbCategoria.SelectedItem = categoriaDetectada;
                cmbCategoria_SelectedIndexChanged(null, null);
                cmbTamanho.SelectedItem = tamanho;
            }
            else
            {
                cmbCategoria.SelectedItem = categoria;
                cmbCategoria_SelectedIndexChanged(null, null);
                cmbTamanho.SelectedItem = tamanho;
            }

           
          

            txtNomeProduto.Text = nomeProduto;
            txtDescricao.Text = descricao;
            txtPreco.Text = preco.ToString("N2");
            txtQuantidade.Text = quantidade.ToString();
            txtTecido.Text = tecido;
            txtCor.Text = cor;
            txtCusto.Text = custo.ToString("N2");
            cmbFornecedor.SelectedItem = fornecedor;
            txtDesconto.Text = desconto.ToString();
        }

      
        private void CarregarCategorias()
        {
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.AddRange(new string[] { "Camisa", "Calça" });
            cmbCategoria.SelectedIndex = 0;
        }

        private Dictionary<string, string[]> tamanhosPorCategoria = new Dictionary<string, string[]>
        {
            { "Camisa", new[] { "PP", "P", "M", "G", "GG" } },
            { "Calça", new[] { "36", "38", "40", "42", "44", "46" } }
        };

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSelecionada = cmbCategoria.SelectedItem.ToString();

            if (tamanhosPorCategoria.TryGetValue(categoriaSelecionada, out string[] tamanhos))
            {
                cmbTamanho.Items.Clear();
                cmbTamanho.Items.AddRange(tamanhos);
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


        private void CarregarImagensProduto()
        {
            try
            {
                listaImagens.Clear();

                string query = "SELECT Url_ImgProduto FROM Imagem_Produto WHERE Id_Produto = @id ORDER BY Ordem_ImgProduto";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@id", idProduto);

                DataTable dt = con.exSQLParametros(cmd);

                foreach (DataRow row in dt.Rows)
                {
                    string nomeArquivo = row["Url_ImgProduto"].ToString();

                    // Constrói a URL completa
                    string urlCompleta = "http://"+Valores.ipserver+"/Tcc-Web/"+nomeArquivo;
                    listaImagens.Add(urlCompleta);
                }

                // Habilita RadioButtons conforme a quantidade de imagens
                radioButton1.Enabled = listaImagens.Count > 0;
                radioButton2.Enabled = listaImagens.Count > 1;
                radioButton3.Enabled = listaImagens.Count > 2;
                radioButton4.Enabled = listaImagens.Count > 3;

                if (listaImagens.Count > 0)
                {
                    radioButton1.Checked = true; 
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar imagens do produto: " + ex.Message);
            }
        }




        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb) || !rb.Checked)
                return;

            int index = -1;

            if (rb == radioButton1) index = 0;
            else if (rb == radioButton2) index = 1;
            else if (rb == radioButton3) index = 2;
            else if (rb == radioButton4) index = 3;

            if (index >= 0 && index < listaImagens.Count)
            {
                try
                {
                    pictureBox1.Load(listaImagens[index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar imagem da URL: " + ex.Message);
                }
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeProduto = txtNomeProduto.Text.Trim();
                string descricao = txtDescricao.Text.Trim();
                decimal valorProduto = decimal.Parse(txtPreco.Text);
                int quantidade = int.Parse(txtQuantidade.Text);
                string tecido = txtTecido.Text.Trim();
                string cor = txtCor.Text.Trim();
                decimal custo = decimal.Parse(txtCusto.Text);
                string tamanho = cmbTamanho.SelectedItem?.ToString();
                string nomeFornecedor = cmbFornecedor.SelectedItem?.ToString();
                int desconto = int.Parse(txtDesconto.Text);

                if (string.IsNullOrEmpty(nomeProduto) || string.IsNullOrEmpty(nomeFornecedor))
                {
                    MessageBox.Show("Preencha todos os campos obrigatórios.");
                    return;
                }

                string queryFornecedor = $"SELECT Id_Fornecedor FROM Fornecedor WHERE Nome_Fornecedor = '{nomeFornecedor}'";
                DataTable dtFornecedor = con.executarSQL(queryFornecedor);
                if (dtFornecedor.Rows.Count == 0)
                {
                    MessageBox.Show("Fornecedor não encontrado.");
                    return;
                }
                int idFornecedor = Convert.ToInt32(dtFornecedor.Rows[0]["ID_Fornecedor"]);

                string query = @"UPDATE Produto SET
                            Nome_Produto = @Nome,
                            Descricao_Produto = @Descricao,
                            Valor_Produto = @Valor,
                            Quantidade_Produto = @Quantidade,
                            Tecido_Produto = @Tecido,
                            Cor_Produto = @Cor,
                            Custo_Produto = @Custo,
                            Tamanho_Produto = @Tamanho,
                            Id_Fornecedor = @Fornecedor,
                            Desconto_Produto = @Desconto    
                         WHERE ID_Produto = @ID";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Nome", nomeProduto);
                cmd.Parameters.AddWithValue("@Descricao", descricao);
                cmd.Parameters.AddWithValue("@Valor", valorProduto);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Tecido", tecido);
                cmd.Parameters.AddWithValue("@Cor", cor);
                cmd.Parameters.AddWithValue("@Custo", custo);
                cmd.Parameters.AddWithValue("@Tamanho", tamanho);
                cmd.Parameters.AddWithValue("@Fornecedor", idFornecedor);
                cmd.Parameters.AddWithValue("@Desconto", desconto);
                cmd.Parameters.AddWithValue("@ID", idProduto);

                con.executarScalar(cmd);

                MessageBox.Show("Produto atualizado com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o produto: " + ex.Message);
            }
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            if (txtPreco.Text == "") return;

            string texto = new string(txtPreco.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(texto))
            {
                txtPreco.Text = "";
                return;
            }

            decimal valor = decimal.Parse(texto) / 100;
            txtPreco.Text = valor.ToString("N2");
            txtPreco.SelectionStart = txtPreco.Text.Length;
        }

        private void txtCusto_TextChanged(object sender, EventArgs e)
        {
            if (txtCusto.Text == "") return;

            string texto = new string(txtCusto.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(texto))
            {
                txtCusto.Text = "";
                return;
            }

            decimal valor = decimal.Parse(texto) / 100;
            txtCusto.Text = valor.ToString("N2");
            txtCusto.SelectionStart = txtCusto.Text.Length;
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDesconto.Text, out int value))
            {
                if (value < 0 || value > 100)
                {
                    MessageBox.Show("Valor deve ser entre 0 e 100.");
                    txtDesconto.Text = "";
                }
            }
            else if (!string.IsNullOrEmpty(txtDesconto.Text))
            {
                MessageBox.Show("Insira um número válido.");
                txtDesconto.Text = "";
            }
        }

        private int ObterIndiceImagemSelecionada()
        {
            if (radioButton1.Checked) return 0;
            if (radioButton2.Checked) return 1;
            if (radioButton3.Checked) return 2;
            if (radioButton4.Checked) return 3;
            return -1;
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

                string url = "http://"+Valores.ipserver+"/Tcc-Web/Assets/php/uploadImgProduto.php";

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
        private bool AtualizarImagemNoBanco(string nomeAntigo, string caminhoNovoServidor)
        {
            try
            {
                string query = @"
            UPDATE Imagem_Produto
            SET Url_ImgProduto = @novo
            WHERE Url_ImgProduto = @antigo AND Id_Produto = @idProduto";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@novo", caminhoNovoServidor); 
                cmd.Parameters.AddWithValue("@antigo", "uploads/imgProduto/" + nomeAntigo);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);

                con.executarScalar(cmd);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar banco: " + ex.Message);
                return false;
            }
        }

        private async void btnSubstituir_Click(object sender, EventArgs e)
        {
            int index = ObterIndiceImagemSelecionada();
            if (index < 0 || index >= listaImagens.Count)
            {
                MessageBox.Show("Nenhuma imagem selecionada para substituição.");
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string novoCaminho = ofd.FileName;

                    string urlAntiga = listaImagens[index];
                    string nomeAntigo = Path.GetFileName(urlAntiga);

                    // Apaga imagem antiga no servidor
                    bool apagou = await ApagarImagemNoServidorAsync(nomeAntigo);
                    if (!apagou)
                    {
                        MessageBox.Show("Erro ao apagar imagem antiga.");
                        return;
                    }

                    // Envia a nova imagem
                    string novoCaminhoNoServidor = await EnviarImagemParaServidor(novoCaminho);
                    if (string.IsNullOrEmpty(novoCaminhoNoServidor))
                    {
                        MessageBox.Show("Erro ao enviar nova imagem.");
                        return;
                    }

                    // Atualiza no banco
                    bool atualizou = AtualizarImagemNoBanco(nomeAntigo, novoCaminhoNoServidor);
                    if (!atualizou)
                    {
                        MessageBox.Show("Erro ao atualizar imagem no banco.");
                        return;
                    }

                    // Atualiza imagem na tela
                    string novaUrlCompleta = "http://"+Valores.ipserver+"/Tcc-Web/"+novoCaminhoNoServidor;
                    listaImagens[index] = novaUrlCompleta;

                    try
                    {
                        pictureBox1.Load(novaUrlCompleta);
                        MessageBox.Show("Imagem substituída com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exibir nova imagem: " + ex.Message);
                    }
                }
            }
        }


        private async Task<bool> ApagarImagemNoServidorAsync(string nomeImagem)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // URL do seu endpoint PHP no servidor
                    string url = "http://"+Valores.ipserver+"/Tcc-Web/Assets/php/deletarImgProduto.php";

                    // Monta o JSON que será enviado
                    var dados = new { imagem = nomeImagem };

                    // Serializa o JSON com Newtonsoft
                    string json = JsonConvert.SerializeObject(dados);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Envia POST
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Lê a resposta
                    string respostaJson = await response.Content.ReadAsStringAsync();
                  
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Imagem excluída do servidor com sucesso!");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Erro do servidor: " + respostaJson);
                      
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao servidor: " + ex.Message);
                return false;
            }
        }


        private bool ExcluirImagemDoBanco(string nomeImagem)
        {
            try
            {
                string caminhoRelativo = "uploads/imgProduto/" + nomeImagem;

                string query = @"DELETE FROM Imagem_Produto
                         WHERE Url_ImgProduto = @url AND Id_Produto = @idProduto";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@url", caminhoRelativo);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);

                con.executarScalar(cmd); 
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir imagem do banco: " + ex.Message);
                return false;
            }
        }


        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            int index = ObterIndiceImagemSelecionada();
            if (index < 0 || index >= listaImagens.Count)
            {
                MessageBox.Show("Nenhuma imagem selecionada para exclusão.");
                return;
            }

            var confirmar = MessageBox.Show("Deseja realmente excluir esta imagem?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirmar != DialogResult.Yes) return;

            string urlCompleta = listaImagens[index];
            

            string nomeImagem = urlCompleta.Substring(urlCompleta.LastIndexOf("/") + 1);

            bool deletado = await ApagarImagemNoServidorAsync(nomeImagem);

            if (!deletado)
            {
                MessageBox.Show("A exclusão no servidor falhou.");
                return;
            }

            
            // Excluir do banco
            if (!ExcluirImagemDoBanco(nomeImagem))
            {
                MessageBox.Show("A imagem foi apagada do servidor, mas não foi removida do banco.");
            }

            // Continua removendo da lista 
            listaImagens.RemoveAt(index);

            switch (index)
            {
                case 0: radioButton1.Enabled = false; break;
                case 1: radioButton2.Enabled = false; break;
                case 2: radioButton3.Enabled = false; break;
                case 3: radioButton4.Enabled = false; break;
            }

            if (listaImagens.Count > 0)
            {
                int novoIndex = Math.Min(index, listaImagens.Count - 1);

                if (novoIndex == 0 && radioButton1.Enabled) radioButton1.Checked = true;
                else if (novoIndex == 1 && radioButton2.Enabled) radioButton2.Checked = true;
                else if (novoIndex == 2 && radioButton3.Enabled) radioButton3.Checked = true;
                else if (novoIndex == 3 && radioButton4.Enabled) radioButton4.Checked = true;
                else
                {
                    if (radioButton1.Enabled) radioButton1.Checked = true;
                    else if (radioButton2.Enabled) radioButton2.Checked = true;
                    else if (radioButton3.Enabled) radioButton3.Checked = true;
                    else if (radioButton4.Enabled) radioButton4.Checked = true;
                }

                pictureBox1.Load(listaImagens[novoIndex]);
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

    }
}
