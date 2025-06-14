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
    public partial class Pedidoconsulta : Form
    {
        ClasseConexao con;
        DataTable dt;
        public Pedidoconsulta()
        {
            InitializeComponent();
            consulta();
            carregamento();
            dataGridView1.ClearSelection();

        }
        private void consulta()
        {
            con = new ClasseConexao();
            String comandosql = @"
                SELECT 
                    Id_Pedido,
                    Data_Pedido,
                    CASE 
                        WHEN Status_Pedido = 0 THEN 'Cancelado'
                        WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NULL AND Data_Entrega_Pedido IS NULL THEN 'Não Enviado'
                        WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NOT NULL AND Data_Entrega_Pedido IS NULL THEN 'Enviado'
                        WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NOT NULL AND Data_Entrega_Pedido IS NOT NULL THEN 'Finalizado'
                        ELSE 'Desconhecido'
                    END AS Status_Pedido_Descricao
                FROM Pedido
                ORDER BY Status_Pedido_Descricao DESC;
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


            // Alteração do nome das colunas

            dataGridView1.Columns[0].HeaderText = "N° Pedido";
            dataGridView1.Columns[1].HeaderText = "Data Pedido";
            dataGridView1.Columns[2].HeaderText = "Status";



            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            try
            {
                String id = dataGridView1.Rows[0].Cells[0].Value.ToString();
                consultaid(id);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

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
            Valores.idped = id;
            con = new ClasseConexao();
            String comandosql = $@"
                SELECT 
                    p.Id_Pedido,
                    FORMAT(p.Data_Pedido, 'dd/MM/yyyy') AS Data_Pedido,
                    COALESCE(FORMAT(p.Data_Envio_Pedido, 'dd/MM/yyyy'), 'Data não registrada') AS Data_Envio_Pedido,
                    COALESCE(FORMAT(p.Data_Entrega_Pedido, 'dd/MM/yyyy'), 'Data não registrada') AS Data_Entrega_Pedido,
                    CASE 
                        WHEN p.Status_Pedido = 0 THEN 'Cancelado'
                        WHEN p.Status_Pedido = 1 AND p.Data_Envio_Pedido IS NULL AND p.Data_Entrega_Pedido IS NULL THEN 'Não Enviado'
                        WHEN p.Status_Pedido = 1 AND p.Data_Envio_Pedido IS NOT NULL AND p.Data_Entrega_Pedido IS NULL THEN 'Enviado'
                        WHEN p.Status_Pedido = 1 AND p.Data_Envio_Pedido IS NOT NULL AND p.Data_Entrega_Pedido IS NOT NULL THEN 'Finalizado'
                        ELSE 'Desconhecido'
                    END AS Status_Pedido_Descricao,
                    e.CEP_Cliente,
                    e.Estado_Cliente,
                    e.Cidade_Cliente,
                    e.Bairro_Cliente,
                    e.Rua_Cliente,
                    e.Numero_Cliente,
                    e.Complemento_Cliente
                FROM Pedido p
                JOIN Endereco_Cliente e ON p.Id_Endereco_Cliente = e.Id_Endereco_Cliente
                WHERE p.Id_Pedido = {id};
            ";
            dt = con.executarSQL(comandosql);
            String npedido = dt.Rows[0]["Id_Pedido"].ToString();
            String datapedido = dt.Rows[0]["Data_Pedido"].ToString();
            String dataenvio = dt.Rows[0]["Data_Envio_Pedido"].ToString();
            String dataentrega = dt.Rows[0]["Data_Entrega_Pedido"].ToString();
            String status = dt.Rows[0]["Status_Pedido_Descricao"].ToString();
            String cep = dt.Rows[0]["CEP_Cliente"].ToString();
            String estado = dt.Rows[0]["Estado_Cliente"].ToString();
            String cidade = dt.Rows[0]["Cidade_Cliente"].ToString();
            String bairro = dt.Rows[0]["Bairro_Cliente"].ToString();
            String rua = dt.Rows[0]["Rua_Cliente"].ToString();
            String numero = dt.Rows[0]["Numero_Cliente"].ToString();
            String complemento = dt.Rows[0]["Complemento_Cliente"].ToString();

            String txt = $"" +
                $"Nº Pedido: {npedido}\n\n" +
                $"Data de Pedido: {datapedido}\n\n" +
                $"Data de Envio: {dataenvio}\n\n" +
                $"Data de Entrega: {dataentrega}\n\n" +
                $"Status: {status}\n\n" +
                $"CEP: {cep}\n\n" +
                $"Estado: {estado}\n\n" +
                $"Cidade: {cidade}\n\n" +
                $"Bairro: {bairro}\n\n" +
                $"Rua: {rua},{numero},{complemento}";

            label1.Text = txt;
        }

        private void btnVerprodutos_Click(object sender, EventArgs e)
        {
            Pedidoproduto fm = new Pedidoproduto();
            fm.Show();
            //this.Hide();
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
                            Id_Pedido,
                            Data_Pedido,
                            CASE 
                                WHEN Status_Pedido = 0 THEN 'Cancelado'
                                WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NULL AND Data_Entrega_Pedido IS NULL THEN 'Não Enviado'
                                WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NOT NULL AND Data_Entrega_Pedido IS NULL THEN 'Enviado'
                                WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NOT NULL AND Data_Entrega_Pedido IS NOT NULL THEN 'Finalizado'
                                ELSE 'Desconhecido'
                            END AS Status_Pedido_Descricao
                        FROM Pedido
                        ORDER BY 
                            CASE WHEN Id_Pedido = {idp} THEN 0 ELSE 1 END,  -- Prioriza o pedido específico
                            Status_Pedido_Descricao DESC;
                    ";
                    dt = con.executarSQL(comandosql);
                    carregamento();
                    
                }catch(FormatException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }

            }

        }
        String caminho_escolhido = "";
        private void btnConteudo_Click(object sender, EventArgs e)
        {
            saveFileDialog2.Title = "Salvar PDF como";
            saveFileDialog2.DefaultExt = "pdf";
            saveFileDialog2.ShowDialog();
        }
        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            // Obtém o caminho do arquivo escolhido pelo usuário
            caminho_escolhido = saveFileDialog2.FileName;
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

                // Cria uma tabela com X colunas
                PdfPTable tableLayout = new PdfPTable(6);

                // Define fonte para o título do documento
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontInvoice = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.NORMAL);

                // Cria um parágrafo com título centralizado
                Paragraph paragraph = new Paragraph("Desempenho do Meses", fontInvoice);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                // Adiciona espaço após o título
                Paragraph p3 = new Paragraph();
                p3.SpacingAfter = 6;
                doc.Add(p3);

                // Adiciona o corpo do documento gerado dinamicamente
                doc.Add(corpoconteudo(tableLayout));

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
        protected PdfPTable corpoconteudo(PdfPTable tableLayout)
        {
            float[] headers = { 30, 30, 30, 30, 30, 30 }; // Define largura de cada coluna
            tableLayout.SetWidths(headers);
            tableLayout.WidthPercentage = 100; // Define porcentagem da largura total
            tableLayout.HeaderRows = 1; // Define que haverá cabeçalho

            // Adiciona cabeçalhos da tabela
            AddCellHeader(tableLayout, "N° Produto");
            AddCellHeader(tableLayout, "Nome");
            AddCellHeader(tableLayout, "Tamanho");
            AddCellHeader(tableLayout, "Tecido");
            AddCellHeader(tableLayout, "Cor");
            AddCellHeader(tableLayout, "Quantidade Solicitada");

            DataSet ds = new DataSet();
            String numped = Valores.idped;
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
                WHERE pp.Id_Pedido = {numped};
            ";
            dt = con.executarSQL(comandosql);
            ds.Tables.Add(dt);

            // Percorre os resultados e adiciona os dados à tabela
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Id_Produto"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Nome_Produto"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Tamanho_Produto"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Tecido_Produto"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Cor_Produto"].ToString(), i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Quantidade_Produto_Pedido"].ToString(), i);
            }

            return tableLayout;
        }


        private void btnDadosenvio_Click(object sender, EventArgs e)
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

                // Cria uma tabela com X colunas
                PdfPTable tableLayout = new PdfPTable(2);

                // Define fonte para o título do documento
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontInvoice = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.NORMAL);

                // Cria um parágrafo com título centralizado
                String numped = Valores.idped;
                Paragraph paragraph = new Paragraph("Conteudo do Pedido Nº"+numped, fontInvoice);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                // Adiciona espaço após o título
                Paragraph p3 = new Paragraph();
                p3.SpacingAfter = 6;
                doc.Add(p3);

                // Adiciona o corpo do documento gerado dinamicamente
                doc.Add(corpodocenvio(tableLayout));

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

        // Método que constrói o corpo da tabela do documento
        protected PdfPTable corpodocenvio(PdfPTable tableLayout)
        {
            float[] headers = { 30, 30 }; // Define largura de cada coluna
            tableLayout.SetWidths(headers);
            tableLayout.WidthPercentage = 100; // Define porcentagem da largura total
            tableLayout.HeaderRows = 1; // Define que haverá cabeçalho

            DataSet ds = new DataSet();
            String numped = Valores.idped;
            con = new ClasseConexao();
            String comandosql = $@"
                SELECT 
                    p.Id_Pedido,
                    c.Nome_Cliente,
                    c.CPF_Cliente,
                    e.CEP_Cliente,
                    e.Estado_Cliente,
                    e.Cidade_Cliente,
                    e.Bairro_Cliente,
                    e.Rua_Cliente,
                    e.Numero_Cliente,
                    e.Complemento_Cliente
                FROM Pedido p
                JOIN Endereco_Cliente e ON p.Id_Endereco_Cliente = e.Id_Endereco_Cliente
                JOIN Cliente c ON e.Id_Cliente = c.Id_Cliente
                WHERE p.Id_Pedido = {numped};
            ";
            dt = con.executarSQL(comandosql);
            ds.Tables.Add(dt);

            // Percorre os resultados e adiciona os dados à tabela
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AddCellToBody(tableLayout, "Numero do Pedido", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Id_Pedido"].ToString(), i);
                AddCellToBody(tableLayout, "Nome do Destinatario", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Nome_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "CPF do Destinatario", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["CPF_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "CEP", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["CEP_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "Estado", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Estado_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "Cidade", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Cidade_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "Bairro", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Bairro_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "Rua", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Rua_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "Numero de Endereço", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Numero_Cliente"].ToString(), i);
                AddCellToBody(tableLayout, "Complemento", i);
                AddCellToBody(tableLayout, ds.Tables[0].Rows[i]["Complemento_Cliente"].ToString(), i);
            }

            return tableLayout;
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
