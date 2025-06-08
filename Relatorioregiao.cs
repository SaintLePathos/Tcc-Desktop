using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;

namespace LojaTardigrado
{
    public partial class Relatorioregiao : Form
    {
        ClasseConexao con;
        DataTable dt;
        public Relatorioregiao()
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
                    e.Estado_Cliente,
                    COUNT(p.Id_Pedido) AS Total_Pedidos,
                    SUM(CASE WHEN p.Status_Pedido = 1 THEN 1 ELSE 0 END) AS Pedidos_Ativos,
                    SUM(CASE WHEN p.Status_Pedido = 0 THEN 1 ELSE 0 END) AS Pedidos_Cancelados,
                    SUM(pp.Valor_Produto_Pedido * pp.Quantidade_Produto_Pedido) AS Valor_Total_Pedidos,
                    AVG(Mensal.Total_Pedidos_Mes) AS Media_Pedidos_Mensal
                FROM Endereco_Cliente e
                JOIN Pedido p ON e.Id_Endereco_Cliente = p.Id_Endereco_Cliente
                JOIN Produto_Pedido pp ON p.Id_Pedido = pp.Id_Pedido
                LEFT JOIN (
                    SELECT 
                        YEAR(Data_Pedido) AS Ano,
                        MONTH(Data_Pedido) AS Mes,
                        COUNT(Id_Pedido) AS Total_Pedidos_Mes
                    FROM Pedido
                    WHERE Data_Pedido >= DATEADD(YEAR, -1, GETDATE()) -- Limita aos últimos 12 meses
                    GROUP BY YEAR(Data_Pedido), MONTH(Data_Pedido)
                ) Mensal ON YEAR(p.Data_Pedido) = Mensal.Ano AND MONTH(p.Data_Pedido) = Mensal.Mes
                GROUP BY e.Estado_Cliente;
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

            // Permite personalizar o grid
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Melhoria: Responsividade automática das colunas
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Melhoria: Permitir redimensionamento manual
            dataGridView1.AllowUserToResizeColumns = true;

            // Alteração da cor das linhas alternadas no grid
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue; // Alterado para azul claro

            // Personalização do cabeçalho das colunas
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Ajustando o alinhamento de texto
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Exibir tooltip nas células
            //dgvVendas.ShowCellToolTips = true;

            /*
            // Habilitar ordenação ao clicar nos cabeçalhos das colunas
            foreach (DataGridViewColumn col in dgvVendas.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }*/

            // Alteração do nome das colunas

            dataGridView1.Columns[0].HeaderText = "UF";
            dataGridView1.Columns[1].HeaderText = "Pedidos Totais";
            dataGridView1.Columns[2].HeaderText = "Qtde Pedidos";
            dataGridView1.Columns[3].HeaderText = "Qtde Pedidos Cancelados";
            dataGridView1.Columns[4].HeaderText = "Valor em Pedidos";
            dataGridView1.Columns[5].HeaderText = "Media de Pedidos (1 ano)";

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Formatar a coluna de Preço Médio dos Pedidos com duas casas decimais
            //dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";

            // Formatar a coluna de Valor de Vendas como moeda (R$)
            //dataGridView1.Columns[1].DefaultCellStyle.Format = "C2";
            //dataGridView1.Columns[3].DefaultCellStyle.Format = "C2";

            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            // Não permite seleção de múltiplas linhas    
            dataGridView1.MultiSelect = false;

            // Ao clicar, seleciona a linha inteira
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Melhoria: Alteração do estilo da borda das células
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Expande a célula automaticamente
            //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Melhoria: Habilitar rolagem suave
            //dataGridView1.ScrollBars = ScrollBars.Both;

            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        String caminho_escolhido = "";
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Salvar PDF como";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // Obtém o caminho do arquivo escolhido pelo usuário
            caminho_escolhido = saveFileDialog1.FileName;
            MessageBox.Show("Salvo em: " + caminho_escolhido); // Exibe mensagem informando onde o arquivo foi salvo

            Document doc = null;
            try
            {
                // Cria um novo documento PDF no tamanho A4
                doc = new Document(PageSize.A4);
                doc.SetMargins(10, 10, 10, 10); // Define margens do documento
                doc.AddCreationDate(); // Adiciona a data de criação

                // Cria o escritor do PDF e associa ao arquivo escolhido
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho_escolhido, FileMode.Create));

                doc.Open(); // Abre o documento para escrita

                // Cria uma tabela com 5 colunas
                PdfPTable tableLayout = new PdfPTable(6);

                // Define fonte para o título do documento
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontInvoice = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.NORMAL);

                // Cria um parágrafo com título centralizado
                Paragraph paragraph = new Paragraph("Relação de Clientes", fontInvoice);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                // Adiciona espaço após o título
                Paragraph p3 = new Paragraph();
                p3.SpacingAfter = 6;
                doc.Add(p3);

                // Adiciona o corpo do documento gerado dinamicamente
                doc.Add(corpo_documento(tableLayout));

                doc.Close(); // Fecha o documento PDF
            }
            catch (Exception erro)
            {
                if (doc != null)
                {
                    doc.Close(); // Fecha o documento em caso de erro
                    MessageBox.Show("Erro Arquivo PDF --> " + erro); // Exibe mensagem de erro
                }
            }
        }
        // Método para adicionar cabeçalhos na tabela do PDF
        private static void AddCellHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 10,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        // Método que constrói o corpo da tabela do documento
        protected PdfPTable corpo_documento(PdfPTable tableLayout)
        {
            float[] headers = { 30, 30, 30, 30, 30, 30 }; // Define largura de cada coluna
            tableLayout.SetWidths(headers);
            tableLayout.WidthPercentage = 100; // Define porcentagem da largura total
            tableLayout.HeaderRows = 1; // Define que haverá cabeçalho

            // Adiciona cabeçalhos da tabela
            AddCellHeader(tableLayout, "UF");
            AddCellHeader(tableLayout, "Pedidos Totais)");
            AddCellHeader(tableLayout, "Qtde Pedidos");
            AddCellHeader(tableLayout, "Qtde Pedidos Cancelados");
            AddCellHeader(tableLayout, "Valor em Pedidos");
            AddCellHeader(tableLayout, "Media de Pedidos (1 ano)");

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            // Percorre os resultados e adiciona os dados à tabela
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Estado_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Total_Pedidos"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Pedidos_Ativos"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Pedidos_Cancelados"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Valor_Total_Pedidos"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Media_Pedidos_Mensal"].ToString(), i);
            }

            return tableLayout;
        }

        // Método para adicionar células ao corpo da tabela
        private static void AddCellToBody(PdfPTable tableLayout, string cellText, int count)
        {
            // Alterna a cor de fundo das linhas para melhor visualização
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 8,
                BackgroundColor = count % 2 == 0 ? new iTextSharp.text.BaseColor(255, 255, 255) : new iTextSharp.text.BaseColor(230, 230, 230)
            });
        }

    }
}
