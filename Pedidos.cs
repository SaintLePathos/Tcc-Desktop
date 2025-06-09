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
    public partial class Pedidos : Form
    {
        private Dictionary<string, decimal> produtos;
        private decimal totalPedido = 0;
        public Pedidos()
        {
            InitializeComponent();
            InicializarProdutos();
            CarregarProdutos();
        }

        private void InicializarProdutos()
        {
            produtos = new Dictionary<string, decimal>()
            {
                { "Camiseta", 59.90m },
                { "Tênis", 199.90m },
                { "Boné", 39.90m },
                { "Calça Jeans", 129.90m }
            };
        }

        private void CarregarProdutos()
        {
            comboBox1.Items.Clear();
            foreach (var item in produtos.Keys)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bntFinalizar_Click(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Menu f2 = new Menu();
            f2.Show();
            this.Hide();
        }
    }
}
