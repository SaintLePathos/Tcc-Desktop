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
    public partial class Relatoriopedido : Form
    {
        ClasseConexao con;
        DataTable dt;
        public Relatoriopedido()
        {
            InitializeComponent();
            consulta();
            carregamento();
        }
        private void consulta()
        {
            con = new ClasseConexao();
            String comandosql = @"SELECT 
                    p.Id_Pedido,
                    COALESCE(CONVERT(VARCHAR, p.Data_Pedido, 103), 'Sem registro') AS Data_Pedido,
                    COALESCE(CONVERT(VARCHAR, p.Data_Envio_Pedido, 103), 'Sem registro') AS Data_Envio_Pedido,
                    COALESCE(CONVERT(VARCHAR, p.Data_Entrega_Pedido, 103), 'Sem registro') AS Data_Entrega_Pedido,
                    CASE 
                        WHEN p.Status_Pedido = 0 THEN 'Cancelado'
                        WHEN p.Data_Envio_Pedido IS NULL AND p.Data_Entrega_Pedido IS NULL AND p.Status_Pedido = 1 THEN 'Em Processamento'
                        WHEN p.Data_Envio_Pedido IS NOT NULL AND p.Data_Entrega_Pedido IS NULL AND p.Status_Pedido = 1 THEN 'Enviado'
                        WHEN p.Data_Envio_Pedido IS NOT NULL AND p.Data_Entrega_Pedido IS NOT NULL AND p.Status_Pedido = 1 THEN 'Entregue'
                        ELSE 'Outro'
                    END AS Status_Pedido
                FROM Pedido p;
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
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 


            // Exibir tooltip nas células
            //dgvVendas.ShowCellToolTips = true;

            /*
            // Habilitar ordenação ao clicar nos cabeçalhos das colunas
            foreach (DataGridViewColumn col in dgvVendas.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }*/

            // Alteração do nome das colunas

            dataGridView1.Columns[0].HeaderText = "Nº Pedido";
            dataGridView1.Columns[1].HeaderText = "Data do Pedido";
            dataGridView1.Columns[2].HeaderText = "Data de Envio";
            dataGridView1.Columns[3].HeaderText = "Data de Entrega";
            dataGridView1.Columns[4].HeaderText = "Status";

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Formatar a coluna de Preço Médio dos Pedidos com duas casas decimais
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";

            // Formatar a coluna de Valor de Vendas como moeda (R$)
            dataGridView1.Columns[1].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "C2";

            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
                PdfPTable tableLayout = new PdfPTable(5);

                // Define fonte para o título do documento
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontInvoice = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.NORMAL);

                // Cria um parágrafo com título centralizado
                Paragraph paragraph = new Paragraph("Relatorio de Pedidos", fontInvoice);
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
            float[] headers = { 30, 30, 30, 30, 30 }; // Define largura de cada coluna
            tableLayout.SetWidths(headers);
            tableLayout.WidthPercentage = 100; // Define porcentagem da largura total
            tableLayout.HeaderRows = 1; // Define que haverá cabeçalho

            // Adiciona cabeçalhos da tabela
            AddCellHeader(tableLayout, "Nº Pedido");
            AddCellHeader(tableLayout, "Data do Pedido");
            AddCellHeader(tableLayout, "Data de Envio");
            AddCellHeader(tableLayout, "Data de Entrega");
            AddCellHeader(tableLayout, "Status");

            dataGridView1.Columns[0].HeaderText = "Nº Pedido";
            dataGridView1.Columns[1].HeaderText = "Data do Pedido";
            dataGridView1.Columns[2].HeaderText = "Data de Envio";
            dataGridView1.Columns[3].HeaderText = "Data de Entrega";
            dataGridView1.Columns[4].HeaderText = "Status";
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            // Percorre os resultados e adiciona os dados à tabela
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Id_Pedido"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Data_Pedido"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Data_Envio_Pedido"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Data_Entrega_Pedido"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Status_Pedido"].ToString(), i);
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
