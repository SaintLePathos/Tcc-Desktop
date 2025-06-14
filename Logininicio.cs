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
    public partial class Logininicio : Form
    {
        ClasseConexao con;
        DataTable dt;
        public Logininicio()
        {
            InitializeComponent();
        }

        private void Logininicio_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            String usu = txtbUsuario.Text;
            String sen = txtbSenha.Text;
            if(usu == "admin" && sen == "admin")
            {
                this.Hide();
                Valores.cargo = "Desenvolvedor";
            }
            else
            {
                String sql = @"
                    SELECT 
                    Id_Funcionario,
                    Cargo_Funcionario,
                    Usuario_Funcionario,
                    Senha_Funcionario 
                    FROM Funcionario 
                    WHERE Usuario_Funcionario = @usuario 
                    AND Senha_Funcionario = @senha 
                    AND Ativo = 1;
                    ";
                con = new ClasseConexao();
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@usuario", usu);
                cmd.Parameters.AddWithValue("@senha", sen);
                try
                {
                    dt = con.exSQLParametros(cmd);
                    if(dt.Rows.Count > 0)
                    {
                        Valores.idusuario = dt.Rows[0]["Id_Funcionario"].ToString();
                        Valores.cargo = dt.Rows[0]["Cargo_Funcionario"].ToString();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Dados incorretos");
                    }
                    
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro em acessar" + ex);
                }
            }
        }
    }
}
