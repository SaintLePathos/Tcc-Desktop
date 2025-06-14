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
    public partial class Funcionariogerenciamento : Form
    {
        ClasseConexao con;
        DataTable dt;
        String idfuncionario;
        public Funcionariogerenciamento()
        {
            InitializeComponent();
            consulta();
            carregamento();
        }
        private void consulta()
        {
            con = new ClasseConexao();
            String comandosql = @"
                SELECT 
                    Id_Funcionario,
                    Nome_Funcionario,
                    Cargo_Funcionario,
	                Data_Cadastro,
                    CASE 
                        WHEN Ativo = 1 THEN 'Ativado'
                        WHEN Ativo = 0 THEN 'Desativado'
                        ELSE 'Desconhecido' -- caso haja algum valor inesperado
                    END AS Status_Funcionario
                FROM Funcionario;
            ";
            dt = con.executarSQL(comandosql);

        }
        private void carregamento()
        {


            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = true; // esconde a coluna 0
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridView1.AllowUserToAddRows = false; // Esconde a nova linha do grid
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false; // Esconde o ponteiro do grid
            dataGridView1.ReadOnly = true;
            dataGridView1.BorderStyle = BorderStyle.None;

            // Evitar que células ou linhas sejam selecionadas
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false; // Impede a seleção de múltiplas linhas


            // Limpar qualquer seleção que ocorra
            //dataGridView1.SelectionChanged += (s, e) => dataGridView1.ClearSelection();

            //dataGridView1.Enabled = false;

            // Permite personalizar o grid
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Melhoria: Responsividade automática das colunas
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Melhoria: Permitir redimensionamento manual
            dataGridView1.AllowUserToResizeColumns = true;

            // Alteração da cor das linhas alternadas no grid
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 8, FontStyle.Bold);

            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#E9E7F4");

            // Personalização do cabeçalho das colunas
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Ajustando o alinhamento de texto
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Alteração do nome das colunas

            dataGridView1.Columns[0].HeaderText = "N° Funcionario";
            dataGridView1.Columns[1].HeaderText = "Nome Funcionari0";
            dataGridView1.Columns[2].HeaderText = "Cargo";
            dataGridView1.Columns[3].HeaderText = "Data de Cadastro";



            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            String id = dataGridView1.Rows[0].Cells[0].Value.ToString();
            consultaid(id);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                consultaid(id);
            }
        }
        private void consultaid(String id)
        {
            Valores.idfuncionario = id;
            idfuncionario = id;
            con = new ClasseConexao();
            String comandosql = $@"
                SELECT 
                    Id_Funcionario,
                    Nome_Funcionario,
                    Cargo_Funcionario,
	                Data_Cadastro,
                    CASE 
                        WHEN Ativo = 1 THEN 'Ativado'
                        WHEN Ativo = 0 THEN 'Desativado'
                        ELSE 'Desconhecido' -- caso haja algum valor inesperado
                    END AS Status_Funcionario
                FROM Funcionario 
                WHERE Id_Funcionario = {id};
            ";
            dt = con.executarSQL(comandosql);
            String numid = dt.Rows[0]["Id_Funcionario"].ToString();
            String nome = dt.Rows[0]["Nome_Funcionario"].ToString();
            String cargo = dt.Rows[0]["Cargo_Funcionario"].ToString();
            String data = dt.Rows[0]["Data_Cadastro"].ToString();
            String status = dt.Rows[0]["Status_Funcionario"].ToString();

            String txt = $"" +
                $"Nº Funcionario: {numid}\n\n" +
                $"Nome: {nome}\n\n" +
                $"Cargo: {cargo}\n\n" +
                $"Data de Cadastro: {data}\n\n" +
                $"Status da Conta: {status}";

            label1.Text = txt;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    String idp = textBox1.Text;
                    con = new ClasseConexao();
                    String comandosql = $@"
                        SELECT 
                            Id_Funcionario,
                            Nome_Funcionario,
                            Cargo_Funcionario,
	                        Data_Cadastro, 
                            CASE  
                                WHEN Ativo = 1 THEN 'Ativado' 
                                WHEN Ativo = 0 THEN 'Desativado' 
                                ELSE 'Desconhecido' -- caso haja algum valor inesperado 
                            END AS Status_Funcionario 
                        FROM Funcionario 
                        ORDER BY 
                            CASE WHEN Id_Funcionario = {idp} THEN 0 ELSE 1 END,   -- Prioriza o pedido específico
                            Id_Funcionario DESC;
                    ";
                    dt = con.executarSQL(comandosql);
                    carregamento();

                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }

            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Funcionarioatualizar fm = new Funcionarioatualizar();
            fm.Show();

        }
    }
}
