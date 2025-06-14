using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LojaTardigrado
{
    public partial class Funcionariocadastro : Form
    {
        public Funcionariocadastro()
        {
            InitializeComponent();
            List<string> opcoes = new List<string> { "Administrador", "Operador", "Gerente", "Financeiro" };
            cmbbCargo.DataSource = opcoes;

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            String nome = txtbNome.Text;
            String cargo = cmbbCargo.SelectedItem.ToString();
            String usuario = txtbUsuario.Text;
            String senha = txtbSenha.Text;
            String confsenha = txtbConfirmasenha.Text;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cargo) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confsenha))
            {
                MessageBox.Show("Preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (senha == confsenha)
                {

                    try
                    {
                        ClasseConexao con = new ClasseConexao();
                        String sql = @"INSERT INTO Funcionario (Nome_Funcionario, Usuario_Funcionario, Senha_Funcionario, Cargo_Funcionario) VALUES (@nome, @usu, @sen, @cargo)";
                        SqlCommand cmd = new SqlCommand(sql);

                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@usu", usuario);
                        cmd.Parameters.AddWithValue("@sen", senha);
                        cmd.Parameters.AddWithValue("@cargo", cargo);

                        con.exSQLParametros(cmd);
                        txtbNome.Text = "";
                        txtbUsuario.Text = "";
                        txtbSenha.Text = "";
                        txtbConfirmasenha.Text = "";
                        MessageBox.Show("Funcionario cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Senhas não são iguais");
                }
            }
        }
    }
}
