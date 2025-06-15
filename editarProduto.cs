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

        }
    }
}
