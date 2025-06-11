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
    public partial class Pedidoproduto : Form
    {
        ClasseConexao con;
        DataTable dt;
        public Pedidoproduto()
        {
            InitializeComponent();
            consulta();
            carregamento();
        }
        private void consulta()
        {
            String idped = Valores.idped;
            con = new ClasseConexao();
            String comandosql = $@"
                SELECT 
                    pp.Id_Produto,
                    p.Nome_Produto,
                    p.Tamanho_Produto,
                    p.Tecido_Produto,
                    p.Cor_Produto,
                    pp.Quantidade_Produto_Pedido
                FROM Produto_Pedido pp
                JOIN Produto p ON pp.Id_Produto = p.Id_Produto
                WHERE pp.Id_Pedido = {idped};
            ";
            dt = con.executarSQL(comandosql);
            label1.Text = "Produtos do Pedido Nº" + idped;
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
            dataGridView1.SelectionChanged += (s, e) => dataGridView1.ClearSelection();

            dataGridView1.Enabled = false;

            // Permite personalizar o grid
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Melhoria: Responsividade automática das colunas
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Melhoria: Permitir redimensionamento manual
            dataGridView1.AllowUserToResizeColumns = true;

            // Alteração da cor das linhas alternadas no grid
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

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
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Alteração do nome das colunas

            dataGridView1.Columns[0].HeaderText = "N° Produto";
            dataGridView1.Columns[1].HeaderText = "Nome";
            dataGridView1.Columns[2].HeaderText = "Tamanho";
            dataGridView1.Columns[3].HeaderText = "Tecido";
            dataGridView1.Columns[4].HeaderText = "Cor";
            dataGridView1.Columns[5].HeaderText = "Qtde Solicitada";



            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
