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
    public partial class Metricasresumo : Form
    {
        ClasseConexao con;
        DataTable dt;
        private Timer meuTimer;
        public Metricasresumo()
        {
            InitializeComponent();
            tudo();
            /*
            meuTimer = new Timer();
            meuTimer.Interval = 1000; // Executar a cada 2 segundos
            meuTimer.Tick += (s, e) => {
                tudo();
                // Chame outros métodos conforme necessário
            };
            meuTimer.Start();
            */
        }
        private void tudo()
        {
            consulta();
            carregamento();
            carregachartum();
            carregachartdois();
            carregalbl();
        }
        private void consulta()
        {
            con = new ClasseConexao();
            String comandosql = @"
                SELECT 
                    p.Id_Produto,
                    f.Nome_Fornecedor, 
                    p.Nome_Produto,
                    p.Quantidade_Produto AS Estoque,
                    COALESCE(SUM(pp.Quantidade_Produto_Pedido), 0) AS Produtos_Pedidos,
                    CASE 
                        WHEN COALESCE(SUM(pp.Quantidade_Produto_Pedido), 0) > (p.Quantidade_Produto - 20) 
                        THEN 'Falta de estoque' 
                        ELSE 'Com estoque' 
                    END AS Status_Estoque
                FROM Produto p
                LEFT JOIN Fornecedor f ON p.Id_Fornecedor = f.Id_Fornecedor
                LEFT JOIN Produto_Pedido pp ON p.Id_Produto = pp.Id_Produto
                LEFT JOIN Pedido ped ON pp.Id_Pedido = ped.Id_Pedido
                WHERE ped.Status_Pedido = 1 OR ped.Status_Pedido IS NULL
                GROUP BY p.Id_Produto, p.Nome_Produto, p.Quantidade_Produto, f.Nome_Fornecedor;
            ";
            dt = con.executarSQL(comandosql);

        }
        private void carregamento()
        {
            dataGridView1.ClearSelection();

            dataGridView1.DataSource = dt;
            //dataGridView1.Columns[0].Visible = true; // esconde a coluna 0
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridView1.AllowUserToAddRows = false; // Esconde a nova linha do grid
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false; // Esconde o ponteiro do grid
            dataGridView1.ReadOnly = true;
            dataGridView1.BorderStyle = BorderStyle.None;

            // Evitar que células ou linhas sejam selecionadas
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;

            // Limpar qualquer seleção que ocorra
            dataGridView1.SelectionChanged += (s, e) => dataGridView1.ClearSelection();

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
            dataGridView1.Columns[1].HeaderText = "Fornecedor";
            dataGridView1.Columns[2].HeaderText = "Nome do Produto";
            dataGridView1.Columns[3].HeaderText = "Em Estoque";
            dataGridView1.Columns[4].HeaderText = "Qtde p/ Pedidos Pendendtes";
            dataGridView1.Columns[5].HeaderText = "Situação";


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
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void carregachartum()
        {
            String sql = @"
                SELECT 
                    DATENAME(WEEKDAY, ped.Data_Pedido) AS Dia_Semana,
                    COUNT(ped.Id_Pedido) AS Quantidade_Pedidos
                FROM Pedido ped
                WHERE ped.Data_Pedido >= DATEADD(DAY, -7, GETDATE()) 
                GROUP BY DATENAME(WEEKDAY, ped.Data_Pedido)
                ORDER BY MIN(ped.Data_Pedido); -- Ordena corretamente por data
            ";

            SqlCommand cmd = new SqlCommand(sql);

            dt = con.exSQLParametros(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                chart1.Series.Clear();
                chart1.Palette = ChartColorPalette.None;
                chart1.Titles.Clear();
                chart1.Titles.Add("Quantidade de Pedidos (Últimos 7 Dias)");

                Series srs = chart1.Series.Add("Qtde Pedidos");
                srs.ChartType = SeriesChartType.Bar; // Alterado para gráfico de pizza

                // Obtendo os menores e maiores valores da série de dados
                int min = dt.AsEnumerable().Min(row => Convert.ToInt32(row["Quantidade_Pedidos"]));
                int max = dt.AsEnumerable().Max(row => Convert.ToInt32(row["Quantidade_Pedidos"]));

                foreach (DataRow row in dt.Rows)
                {
                    srs.Points.AddXY(row["Dia_Semana"], row["Quantidade_Pedidos"]);
                }
                // Definindo os limites do eixo X no gráfico
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = max;
                chart1.ChartAreas[0].AxisY.Interval = 1; // Garante que cada ano será mostrado corretamente

                srs.IsValueShownAsLabel = true; // Exibe os valores no gráfico
            }
            else
            {
                //MessageBox.Show("Nenhum dado encontrado para gerar o gráfico.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void carregachartdois()
        {
            String sql = @"
                SELECT TOP 10 
                    p.Id_Produto,
                    p.Nome_Produto,
                    SUM(pp.Quantidade_Produto_Pedido) AS Total_Quantidade_Pedida
                FROM Produto_Pedido pp
                JOIN Pedido ped ON pp.Id_Pedido = ped.Id_Pedido
                JOIN Produto p ON pp.Id_Produto = p.Id_Produto
                WHERE ped.Data_Pedido >= DATEADD(DAY, -7, GETDATE()) 
                GROUP BY p.Id_Produto, p.Nome_Produto
                ORDER BY Total_Quantidade_Pedida DESC;
            ";

            SqlCommand cmd = new SqlCommand(sql);

            dt = con.exSQLParametros(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                chart2.Series.Clear();
                chart2.Palette = ChartColorPalette.None;
                chart2.Titles.Clear();
                chart2.Titles.Add("Produtos Mais Vendidos (Últimos 7 Dias)");

                Series srs = chart2.Series.Add("Vendas");
                srs.ChartType = SeriesChartType.Pie; // Alterado para gráfico de pizza

                foreach (DataRow row in dt.Rows)
                {
                    srs.Points.AddXY(row["Nome_Produto"], row["Total_Quantidade_Pedida"]);
                }

                srs.IsValueShownAsLabel = true; // Exibe os valores no gráfico
            }
            else
            {
                //MessageBox.Show("Nenhum dado encontrado para gerar o gráfico.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void carregalbl()
        {

            String sql = @"
                SELECT 
                    'ped_pendente_envio' AS Categoria, 
                    COUNT(*) AS Quantidade 
                FROM Pedido 
                WHERE Status_Pedido = 1 
                AND Data_Envio_Pedido IS NULL

                UNION ALL

                SELECT 
                    'ped_cancel_semana', 
                    COUNT(*) 
                FROM Pedido 
                WHERE Status_Pedido = 0 
                AND Data_Pedido >= DATEADD(DAY, -7, GETDATE()) 
                AND Data_Envio_Pedido IS NULL

                UNION ALL

                SELECT 
                    'ped_atrasados', 
                    COUNT(*) 
                FROM Pedido 
                WHERE Status_Pedido = 1 
                AND Data_Envio_Pedido IS NULL 
                AND Data_Pedido < DATEADD(DAY, -5, GETDATE());
                ";

            SqlCommand cmd = new SqlCommand(sql);

            dt = con.exSQLParametros(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                String v1 = dt.Rows[0]["Quantidade"].ToString();
                String v2 = dt.Rows[1]["Quantidade"].ToString();
                String v3 = dt.Rows[2]["Quantidade"].ToString();
                label1.Text = "Resumo Semanal\n \nPedidos Por Enviar: " + v1 + " \n \nPedidos em Cancelados: " + v2 + " \n \nPedidos Envio Urgente: " + v3;
            }
            else
            {
                //MessageBox.Show("Nenhum dado encontrado para gerar o gráfico.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
