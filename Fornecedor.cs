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
    public partial class Fornecedor : Form
    {
        public Fornecedor()
        {
            InitializeComponent();
            CarregarFornecedores();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string CNPJ = txtCnpj.Text.Replace(",", "").Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            string Nome = txtNomeFornecedor.Text.Trim();
            string Email = txtEmailFornecedor.Text.Trim();
            string Telefone = txtTelefoneFornecedor.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

            if (string.IsNullOrEmpty(CNPJ) || string.IsNullOrEmpty(Nome) ||
                string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Telefone))
            {
                MessageBox.Show("Preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ClasseConexao conexao = new ClasseConexao();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Fornecedor (CNPJ_Fornecedor, Nome_Fornecedor, Email_Fornecedor, Telefone_Fornecedor)
                            VALUES (@CNPJ, @Nome, @Email, @Telefone)";

                cmd.Parameters.AddWithValue("@CNPJ", CNPJ);
                cmd.Parameters.AddWithValue("@Nome", Nome);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Telefone", Telefone);

                int resultado = conexao.manutencaoDB_Parametros(cmd);

                if (resultado > 0)
                {
                    MessageBox.Show("Fornecedor cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimparCampos();
                    CarregarFornecedores();

                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar fornecedor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LimparCampos()
        {

            txtCnpj.Text = "";
            txtNomeFornecedor.Text = "";
            txtEmailFornecedor.Text = "";
            txtTelefoneFornecedor.Text = "";
        }
        private void CarregarFornecedores()
        {
            try
            {
                ClasseConexao conexao = new ClasseConexao();
                string sql = "SELECT Id_Fornecedor, CNPJ_Fornecedor, Nome_Fornecedor, Email_Fornecedor, Telefone_Fornecedor FROM Fornecedor";
                DataTable dt = conexao.executarSQL(sql);
                dgvFornecedores.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
