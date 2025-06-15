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
    public partial class editarFornecedor : Form
    {
        private int idFornecedor;
        private ClasseConexao con;
        public editarFornecedor(int id, string nome, string email, string telefone)
        {
            InitializeComponent();
            idFornecedor = id;

            txtNome.Text = nome;
            txtEmail.Text = email;
            txtTelefone.Text = telefone;


            con = new ClasseConexao();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string telefone = txtTelefone.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(telefone))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos.");
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand(@"
            UPDATE Fornecedor
            SET Nome_Fornecedor = @nome,
                Email_Fornecedor = @email,
                Telefone_Fornecedor = @telefone
            WHERE Id_Fornecedor = @id");

                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.Parameters.AddWithValue("@id", idFornecedor);

                int linhasAfetadas = con.manutencaoDB_Parametros(cmd);

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Fornecedor atualizado com sucesso!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar fornecedor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
