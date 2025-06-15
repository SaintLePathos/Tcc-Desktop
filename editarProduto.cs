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

namespace LojaTardigrado
{
    public partial class editarProduto : Form
    {
        ClasseConexao con;
        private int idProduto;
      

        public editarProduto(int idProduto, string nomeProduto, string descricao, decimal preco, int quantidade, string tecido, string cor, decimal custo, string tamanho, string fornecedor)
        {
            InitializeComponent();
            con = new ClasseConexao();
            ExbirFornecedor();
            CarregarCategorias();
           


            this.idProduto = idProduto;

            // Preenche os campos com os dados recebidos
            txtNomeProduto.Text = nomeProduto;
            txtDescricao.Text = descricao;
            txtPreco.Text = preco.ToString("N2");
            txtQuantidade.Text = quantidade.ToString();
            txtTecido.Text = tecido;
            txtCor.Text = cor;
            txtCusto.Text = custo.ToString("N2");
            cmbTamanho.SelectedItem = tamanho;
            cmbFornecedor.SelectedItem = fornecedor;

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtem os valores dos campos
                string nomeProduto = txtNomeProduto.Text.Trim();
                string descricao = txtDescricao.Text.Trim();
                decimal valorProduto = decimal.Parse(txtPreco.Text);
                int quantidade = int.Parse(txtQuantidade.Text);
                string tecido = txtTecido.Text.Trim();
                string cor = txtCor.Text.Trim();
                decimal custo = decimal.Parse(txtCusto.Text);
                string tamanho = cmbTamanho.SelectedItem?.ToString();
                string nomeFornecedor = cmbFornecedor.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(nomeProduto) || string.IsNullOrEmpty(nomeFornecedor))
                {
                    MessageBox.Show("Preencha todos os campos obrigatórios.");
                    return;
                }

                // Consulta para pegar o ID do fornecedor pelo nome
                string queryFornecedor = $"SELECT Id_Fornecedor FROM Fornecedor WHERE Nome_Fornecedor = '{nomeFornecedor}'";
                DataTable dtFornecedor = con.executarSQL(queryFornecedor);
                if (dtFornecedor.Rows.Count == 0)
                {
                    MessageBox.Show("Fornecedor não encontrado.");
                    return;
                }
                int idFornecedor = Convert.ToInt32(dtFornecedor.Rows[0]["ID_Fornecedor"]);

                // Query de UPDATE
                string query = @"UPDATE Produto SET
                            Nome_Produto = @Nome,
                            Descricao_Produto = @Descricao,
                            Valor_Produto = @Valor,
                            Quantidade_Produto = @Quantidade,
                            Tecido_Produto = @Tecido,
                            Cor_Produto = @Cor,
                            Custo_Produto = @Custo,
                            Tamanho_Produto = @Tamanho,
                            Id_Fornecedor = @Fornecedor
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
                cmd.Parameters.AddWithValue("@ID", idProduto);

                con.executarScalar(cmd);

                MessageBox.Show("Produto atualizado com sucesso!");
                this.Close(); // Fecha a janela de edição
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o produto: " + ex.Message);
            }
        }

    }
}
