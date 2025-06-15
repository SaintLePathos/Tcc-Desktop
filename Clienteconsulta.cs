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
    public partial class Clienteconsulta : Form
    {
        ClasseConexao con;
        DataTable dt;
        String idcliente;
        public Clienteconsulta()
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
                    Id_Cliente,
                    CPF_Cliente,
                    Nome_Cliente,
                    Email_Cliente,
                    CASE 
                        WHEN Conta_Ativa = 1 THEN 'Ativado'
                        WHEN Conta_Ativa = 0 THEN 'Desativado'
                        ELSE 'Desconhecido' -- caso haja algum valor inesperado
                    END AS Status_Conta
                FROM Cliente;
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
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Alteração do nome das colunas

            dataGridView1.Columns[0].HeaderText = "N° Cliente";
            dataGridView1.Columns[1].HeaderText = "CPF";
            dataGridView1.Columns[2].HeaderText = "Nome";
            dataGridView1.Columns[3].HeaderText = "Email";
            dataGridView1.Columns[4].HeaderText = "Status da Conta";



            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

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
            idcliente = id.ToString();
            con = new ClasseConexao();
            String comandosql = $@"
                SELECT 
                    c.Id_Cliente,
                    c.Nome_Cliente,
                    c.CPF_Cliente,
                    c.Email_Cliente,
                    c.Telefone_Cliente,

                    CASE 
                        WHEN Conta_Ativa = 1 THEN 'Ativado'
                        WHEN Conta_Ativa = 0 THEN 'Desativado'
                        ELSE 'Desconhecido' -- caso haja algum valor inesperado
                    END AS Status_Conta,

                    (SELECT COUNT(*) 
                     FROM Endereco_Cliente ec 
                     WHERE ec.Id_Cliente = c.Id_Cliente) AS Quantidade_Enderecos,

                    (SELECT COUNT(*) 
                     FROM Endereco_Cliente ec
                     JOIN Pedido p ON p.Id_Endereco_Cliente = ec.Id_Endereco_Cliente
                     WHERE ec.Id_Cliente = c.Id_Cliente) AS Quantidade_Pedidos,

                    (SELECT ISNULL(SUM(pp.Quantidade_Produto_Pedido), 0)
                     FROM Endereco_Cliente ec
                     JOIN Pedido p ON p.Id_Endereco_Cliente = ec.Id_Endereco_Cliente
                     JOIN Produto_Pedido pp ON pp.Id_Pedido = p.Id_Pedido
                     WHERE ec.Id_Cliente = c.Id_Cliente) AS Quantidade_Produtos_Comprados

                FROM Cliente c
                WHERE c.Id_Cliente = {id};
            ";
            dt = con.executarSQL(comandosql);
            String numid = dt.Rows[0]["Id_Cliente"].ToString();
            String nome = dt.Rows[0]["Nome_Cliente"].ToString();
            String cpf = dt.Rows[0]["CPF_Cliente"].ToString();
            String email = dt.Rows[0]["Email_Cliente"].ToString();
            String telefone = dt.Rows[0]["Telefone_Cliente"].ToString();
            String status = dt.Rows[0]["Status_Conta"].ToString();
            String qtdeendereco = dt.Rows[0]["Quantidade_Enderecos"].ToString();
            String qtdepedido = dt.Rows[0]["Quantidade_Pedidos"].ToString();
            String qtdprodutos = dt.Rows[0]["Quantidade_Produtos_Comprados"].ToString();

            String txt = $"" +
                $"Nº Funcionario: {numid}\n\n" +
                $"Nome: {nome}\n\n" +
                $"CPF: {cpf}\n\n" +
                $"Email: {email}\n\n" +
                $"Contato: {telefone}\n\n" +
                $"Status da Conta: {status}\n\n" +
                $"Quantidade de Endereços: {qtdeendereco}\n\n" +
                $"Quantidade de Pedidos: {qtdepedido}\n\n" +
                $"Quantidade de Produtos: {qtdprodutos}";

            label1.Text = txt;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbPesquisar.Text))
            {
                try
                {
                    string cpfPesquisa = txtbPesquisar.Text.Trim();
                    con = new ClasseConexao();

                    string comandosql = $@"
                SELECT 
                    Id_Cliente,
                    CPF_Cliente,
                    Nome_Cliente,
                    Email_Cliente,
                    CASE 
                        WHEN Conta_Ativa = 1 THEN 'Ativado'
                        WHEN Conta_Ativa = 0 THEN 'Desativado'
                        ELSE 'Desconhecido'
                    END AS Status_Conta
                FROM Cliente
                WHERE CPF_Cliente LIKE '{cpfPesquisa}%'
                ORDER BY Id_Cliente DESC;
            ";

                    dt = con.executarSQL(comandosql);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Nenhum cliente encontrado com esse CPF.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // Não chama carregamento se não houver resultados
                    }

                    carregamento();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao pesquisar: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Digite um CPF para pesquisar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnDesativar_Click(object sender, EventArgs e)
        {
            try
            {
                con = new ClasseConexao();
                String comandosql = $@"
                    UPDATE Cliente
                    SET Conta_Ativa = 0
                    WHERE Id_Cliente = {idcliente};
                    ";
                con.executarSQL(comandosql);
                consulta();
                carregamento();

            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void btnAtivar_Click(object sender, EventArgs e)
        {
            try
            {
                con = new ClasseConexao();
                String comandosql = $@"
                    UPDATE Cliente
                    SET Conta_Ativa = 1
                    WHERE Id_Cliente = {idcliente};
                    ";
                con.executarSQL(comandosql);
                consulta();
                carregamento();

            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void btnvoltar_Click(object sender, EventArgs e)
        {
            try
            {
                txtbPesquisar.Clear(); // Limpa o campo de pesquisa (opcional)
                consulta();            // Recarrega todos os dados da tabela
                carregamento();        // Atualiza o DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao voltar para a visualização padrão: " + ex.Message);
            }
        }

    }
}
