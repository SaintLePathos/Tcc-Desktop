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
    public partial class Funcionarioatualizar : Form
    {
        public Funcionarioatualizar()
        {
            InitializeComponent();
            List<string> opcoes = new List<string> { "Administrador", "Operador", "Gerente", "Financeiro" };
            cmbbCargo.DataSource = opcoes;
            List<string> sts = new List<string> { "Ativada", "Desativada" };
            cmbbStatus.DataSource = sts;
            lblFuncionarionum.Text = "Atualizar Cadastro Funcionario nº " + Valores.idfuncionario;
        }

        private void btnAtualizarcadastro_Click(object sender, EventArgs e)
        {
            String idfuncionario = Valores.idfuncionario;
            String nome = txtbNome.Text;
            String cargo = cmbbCargo.SelectedItem.ToString();
            String usuario = txtbUsuario.Text;
            String senha = txtbSenha.Text;
            String confsenha = txtbConfirmasenha.Text;
            String status = cmbbStatus.SelectedItem.ToString();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cargo) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confsenha) || string.IsNullOrEmpty(status))
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
                        if(status == "Desativada")
                        {
                            status = "0";
                        }
                        else
                        {
                            status = "1";
                        }
                        ClasseConexao con = new ClasseConexao();
                        String sql = @"UPDATE Funcionario
                            SET 
                                Nome_Funcionario = @nome,
                                Usuario_Funcionario = @usu,
                                Senha_Funcionario = @sen,
                                Cargo_Funcionario = @cargo,
                                Ativo = @status
                            WHERE 
                                Id_Funcionario = @id;
                            ";
                        SqlCommand cmd = new SqlCommand(sql);

                        cmd.Parameters.AddWithValue("@id", idfuncionario);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@usu", usuario);
                        cmd.Parameters.AddWithValue("@sen", senha);
                        cmd.Parameters.AddWithValue("@cargo", cargo);
                        cmd.Parameters.AddWithValue("@status", status);

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
