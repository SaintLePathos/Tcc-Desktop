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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

    

        private void Button4_Click(object sender, EventArgs e)
        {
            Pedidos pedidos = new Pedidos();
            pedidos.Show();
            this.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form4 CadastraProduto = new Form4();
            CadastraProduto.Show();
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form5 Produtos = new Form5();
            Produtos.Show();
            this.Hide();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form6 StatusPedidos = new Form6();
            StatusPedidos.Show();
            this.Hide();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Form7 Financa = new Form7();
            Financa.Show();
            this.Hide();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Form8 ContaUsuarios = new Form8();
            ContaUsuarios.Show();
            this.Hide();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }

        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Show();
            this.Hide();
        }
    }
}
