using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaTardigrado
{
    public partial class Form5 : Form
    {
        private List<Produto> produtos;
        public Form5()
        {
            InitializeComponent();
            InicializarProdutos();
            CarregarCategorias();
            AtualizarGrid(produtos);
        }

        private void InicializarProdutos()
        {
            produtos = new List<Produto>
            {
                new Produto { Nome = "Camiseta", Preco = 59.90m, Categoria = "Roupas" },
                new Produto { Nome = "Tênis", Preco = 199.90m, Categoria = "Calçados" },
                new Produto { Nome = "Boné", Preco = 39.90m, Categoria = "Acessórios" },
                new Produto { Nome = "Calça Jeans", Preco = 129.90m, Categoria = "Roupas" },
                new Produto { Nome = "Fone Bluetooth", Preco = 149.90m, Categoria = "Eletrônicos" }
            };
        }

        private void CarregarCategorias()
        {
            comboBox1.Items.Add("Todas");
            comboBox1.Items.AddRange(new string[] { "Roupas", "Calçados", "Acessórios", "Eletrônicos" });
            comboBox1.SelectedIndex = 0;
        }

        private void AtualizarGrid(List<Produto> lista)
        {
            dataGridView1.Rows.Clear();
            foreach (var p in lista)
            {
                dataGridView1.Rows.Add(p.Nome, p.Preco.ToString("C"), p.Categoria);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string nomeBusca = textBox1.Text.Trim().ToLower();
            string categoriaSelecionada = comboBox1.SelectedItem.ToString();

            var filtrados = produtos.Where(p =>
                (string.IsNullOrEmpty(nomeBusca) || p.Nome.ToLower().Contains(nomeBusca)) &&
                (categoriaSelecionada == "Todas" || p.Categoria == categoriaSelecionada)
            ).ToList();

            AtualizarGrid(filtrados);
        }
    }
    public class Produto
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
    }
}
