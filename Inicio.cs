using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace LojaTardigrado
{
    public partial class Inicio : Form
    {
        ClasseConexao con;
        DataTable dt;

        public Inicio()
        {
            InitializeComponent();

            
        }
        private void abrelogin()
        {
            Logininicio fm = new Logininicio();
            fm.ShowDialog();

        }

        private void trocaform(Form form)
        {
            form.Show();
            //this.Hide();
        }
        private void periodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Relatorioperiodo());
        }

        private void regionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Relatorioregiao());
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Relatorioproduto());
        }

        private void pedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            trocaform(new Relatoriopedido());
        }

        private void conToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Pedidoconsulta());
        }

        private void gerenciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Pedidogerenciamento());
        }

        private void cadastrarProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Produto());
        }

        private void cadastroDeFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Fornecedor());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            abrelogin();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String txtcargo = Valores.cargo;
            String txtidfunc = Valores.idusuario;
            if (txtcargo == "Desenvolvedor")
            {
                DialogResult resultado = MessageBox.Show("Tipo de acesso: " + txtcargo);
            }
            else
            {
                String sql = @"
                    SELECT 
                    Nome_Funcionario 
                    FROM Funcionario 
                    WHERE Id_Funcionario = @id 
                    ";
                con = new ClasseConexao();
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", txtidfunc);
                try
                {
                    dt = con.exSQLParametros(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        String nome = dt.Rows[0]["Nome_Funcionario"].ToString();
                        DialogResult resultado = MessageBox.Show("Nome: " + nome + "\n\nCargo: " + txtcargo);
                    }
                    else
                    {
                        DialogResult resultado = MessageBox.Show("Erro de consulta");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro em acessar" + ex);
                }
            } 
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)

        {
            DialogResult resultado = MessageBox.Show("Deseja realmente sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cadastrarNovoFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Funcionariocadastro());
        }


        private void gerenciarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            trocaform(new Funcionariogerenciamento());
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            trocaform(new Clienteconsulta());
        }

        private void resumoSemanalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocaform(new Metricasresumo());
        }
    }
}
