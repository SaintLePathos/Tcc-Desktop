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
          
            CarregarCategorias();


        }
        private void CarregarCategorias()
        {
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.AddRange(new string[] { "Camisa", "Calça", "Tênis" });
            cmbCategoria.SelectedIndex = 0; // Padrão: Camisa
        }


       

        private Dictionary<string, string[]> tamanhosPorCategoria = new Dictionary<string, string[]>
{
    { "Camisa", new[] { "PP", "P", "M", "G", "GG" } },
    { "Calça", new[] { "36", "38", "40", "42", "44", "46" } },
    { "Tênis", new[] { "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44" } }
};
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSelecionada = cmbCategoria.SelectedItem.ToString();

            if (tamanhosPorCategoria.TryGetValue(categoriaSelecionada, out string[] tamanhos))
            {
                cmbTamanho.Items.Clear();
                cmbTamanho.Items.AddRange(tamanhos);
                cmbTamanho.SelectedIndex = 0;
            }
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

     

        private bool CamposValidos()
        {
            return !string.IsNullOrWhiteSpace(txtDescricao.Text) &&
                   !string.IsNullOrWhiteSpace(txtNomeProduto.Text) &&
                   !string.IsNullOrWhiteSpace(txtPreco.Text) &&
                   !string.IsNullOrWhiteSpace(txtQuantidade.Text) &&
                   !string.IsNullOrWhiteSpace(txtTecido.Text) &&
                   !string.IsNullOrWhiteSpace(txtCor.Text) &&
                   !string.IsNullOrWhiteSpace(txtCusto.Text) &&
                   cmbTamanho.SelectedIndex >= 0 &&
                   cmbFornecedor.SelectedIndex >= 0;
        }
        private void LimparCampos()
        {
            txtNomeProduto.Clear();
            txtDescricao.Clear();
            txtCusto.Clear();
            txtPreco.Clear();
            txtQuantidade.Clear();
            txtTecido.Clear();
            txtCor.Clear();
            cmbTamanho.SelectedIndex = 0;
            cmbFornecedor.SelectedIndex = 0;
            caminhosLocaisImagens.Clear();
        }

        private string BuscarNomeFornecedorPorId(int idFornecedor)
        {
            SqlCommand cmd = new SqlCommand("SELECT Nome_Fornecedor FROM Fornecedor WHERE Id_Fornecedor = @id");
            cmd.Parameters.AddWithValue("@id", idFornecedor);
            DataTable dt = con.exSQLParametros(cmd);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["Nome_Fornecedor"].ToString();

            return string.Empty;
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
                    decimal custo = decimal.Parse(txtCusto.Text);

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
                     Tamanho_Produto, Quantidade_Produto, Tecido_Produto, Cor_Produto, Custo_Produto)
                    OUTPUT INSERTED.Id_Produto
                    VALUES
                    (@idFornecedor, @nomeProduto, @descricao, @preco, 
                     @tamanho, @quantidade, @tecido, @cor, @custo);
                ");


                    cmdInsert.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                    cmdInsert.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                    cmdInsert.Parameters.AddWithValue("@descricao", descricao);
                    cmdInsert.Parameters.AddWithValue("@preco", preco);
                    cmdInsert.Parameters.AddWithValue("@tamanho", tamanho);
                    cmdInsert.Parameters.AddWithValue("@quantidade", quantidade);
                    cmdInsert.Parameters.AddWithValue("@tecido", tecido);
                    cmdInsert.Parameters.AddWithValue("@cor", cor);
                    cmdInsert.Parameters.AddWithValue("@custo", custo);
                    


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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um produto para editar.");
                return;
            }

            DataGridViewRow linha = dataGridView1.SelectedRows[0];

            int idProduto = Convert.ToInt32(linha.Cells["Id_Produto"].Value);
            string nomeProduto = linha.Cells["Nome_Produto"].Value.ToString();
            string descricao = linha.Cells["Descricao_Produto"].Value.ToString();
            decimal preco = Convert.ToDecimal(linha.Cells["Valor_Produto"].Value);
            int quantidade = Convert.ToInt32(linha.Cells["Quantidade_Produto"].Value);
            string tecido = linha.Cells["Tecido_Produto"].Value.ToString();
            string cor = linha.Cells["Cor_Produto"].Value.ToString();
            decimal custo = Convert.ToDecimal(linha.Cells["Custo_Produto"].Value);
            string tamanho = linha.Cells["Tamanho_Produto"].Value.ToString();

            // Buscar nome do fornecedor (se só tiver Id no grid)
            int idFornecedor = Convert.ToInt32(linha.Cells["Id_Fornecedor"].Value);
            string fornecedor = BuscarNomeFornecedorPorId(idFornecedor);

            editarProduto formEditar = new editarProduto(idProduto, nomeProduto, descricao, preco, quantidade, tecido, cor, custo, tamanho, fornecedor);
            formEditar.ShowDialog();

            // Depois que fechar o formEditar, atualize a lista:
            ExibirProdutos();


        }



        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um produto para excluir.");
                return;
            }

            try
            {
                int idProduto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id_Produto"].Value);

                // 1. Verifica se o produto está em Produto_Pedido
                SqlCommand cmdCheckPedido = new SqlCommand("SELECT COUNT(*) FROM Produto_Pedido WHERE Id_Produto = @id");
                cmdCheckPedido.Parameters.AddWithValue("@id", idProduto);
                object pedidos = con.executarScalar(cmdCheckPedido);

                if (Convert.ToInt32(pedidos) > 0)
                {
                    MessageBox.Show("Este produto está vinculado a um pedido e não pode ser excluído.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Verifica se a quantidade é maior que 0
                SqlCommand cmdCheckQuantidade = new SqlCommand("SELECT Quantidade_Produto FROM Produto WHERE Id_Produto = @id");
                cmdCheckQuantidade.Parameters.AddWithValue("@id", idProduto);
                object quantidadeObj = con.executarScalar(cmdCheckQuantidade);

                if (quantidadeObj != null && Convert.ToInt32(quantidadeObj) > 0)
                {
                    MessageBox.Show("A quantidade deste produto ainda é maior que 0. Reduza a quantidade antes de excluí-lo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Confirma exclusão
                DialogResult confirm = MessageBox.Show("Deseja realmente excluir este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                // 4. Exclui imagens associadas primeiro (para respeitar a FK)
                SqlCommand cmdDeleteImgs = new SqlCommand("DELETE FROM Imagem_Produto WHERE Id_Produto = @id");
                cmdDeleteImgs.Parameters.AddWithValue("@id", idProduto);
                con.manutencaoDB_Parametros(cmdDeleteImgs);

                // 5. Exclui o produto
                SqlCommand cmdDeleteProduto = new SqlCommand("DELETE FROM Produto WHERE Id_Produto = @id");
                cmdDeleteProduto.Parameters.AddWithValue("@id", idProduto);
                int linhasAfetadas = con.manutencaoDB_Parametros(cmdDeleteProduto);

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Produto excluído com sucesso.");
                    ExibirProdutos();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir produto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir só números e tecla backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // bloqueia o caractere
            }
        }


        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            if (txtPreco.Text == "") return;

            string texto = txtPreco.Text;

            // Remove tudo que não for número
            texto = new string(texto.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(texto))
            {
                txtPreco.Text = "";
                return;
            }

            // Converte para decimal, divide por 100 para considerar duas casas decimais
            decimal valor = decimal.Parse(texto) / 100;

            // Formata com vírgula (ex: 1234 => 12,34)
            txtPreco.Text = valor.ToString("N2");

            // Move o cursor para o final
            txtPreco.SelectionStart = txtPreco.Text.Length;
        }

        private void txtCusto_TextChanged(object sender, EventArgs e)
        {
            if (txtCusto.Text == "") return;

            string texto = txtCusto.Text;

            // Remove tudo que não for número
            texto = new string(texto.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(texto))
            {
                txtCusto.Text = "";
                return;
            }

            // Converte para decimal, divide por 100 para considerar duas casas decimais
            decimal valor = decimal.Parse(texto) / 100;

            // Formata com vírgula (ex: 1234 => 12,34)
            txtCusto.Text = valor.ToString("N2");

            // Move o cursor para o final
            txtCusto.SelectionStart = txtCusto.Text.Length;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}

