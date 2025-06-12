using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace LojaTardigrado
{
    public partial class Pedidogerenciamento : Form
    {
        ClasseConexao con;
        DataTable dt;
        string dtprabd;
        public Pedidogerenciamento()
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
                    FORMAT(Data_Pedido, 'dd/MM/yyyy') AS Data_Pedido,
                    COALESCE(FORMAT(Data_Envio_Pedido, 'dd/MM/yyyy'), 'Data não registrada') AS Data_Envio_Pedido,
                    COALESCE(FORMAT(Data_Entrega_Pedido, 'dd/MM/yyyy'), 'Data não registrada') AS Data_Entrega_Pedido,
                    CASE 
                        WHEN Status_Pedido = 0 THEN 'Cancelado'
                        WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NULL AND Data_Entrega_Pedido IS NULL THEN 'Não Enviado'
                        WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NOT NULL AND Data_Entrega_Pedido IS NULL THEN 'Enviado'
                        WHEN Status_Pedido = 1 AND Data_Envio_Pedido IS NOT NULL AND Data_Entrega_Pedido IS NOT NULL THEN 'Finalizado'
                        ELSE 'Desconhecido'
                    END AS Status_Pedido_Descricao
                FROM Pedido
                ORDER BY Id_Pedido DESC;
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

            dataGridView1.Columns[0].HeaderText = "N° Pedido";
            dataGridView1.Columns[1].HeaderText = "Data Pedido";
            dataGridView1.Columns[2].HeaderText = "Data Envio";
            dataGridView1.Columns[3].HeaderText = "Data Entrega";
            dataGridView1.Columns[4].HeaderText = "Status";



            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



            // Define o tamanho de largura das colunas
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            // Config ordenação das linhas
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            String id = dataGridView1.Rows[0].Cells[0].Value.ToString();
            consultaid(id);
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
                            FORMAT(Data_Pedido, 'dd/MM/yyyy') AS Data_Pedido,
                            COALESCE(FORMAT(Data_Envio_Pedido, 'dd/MM/yyyy'), 'Data não registrada') AS Data_Envio_Pedido,
                            COALESCE(FORMAT(Data_Entrega_Pedido, 'dd/MM/yyyy'), 'Data não registrada') AS Data_Entrega_Pedido,
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
                            Id_Pedido DESC;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                consultaid(id);
            }
        }

        private void btnpreenchedata_Click(object sender, EventArgs e)
        {
            string dtaparenc = DateTime.Now.ToString("dd/MM/yyyy");
            dtprabd = DateTime.Now.ToString("yyyy-MM-dd");
            maskedTextBox1.Text = dtaparenc;


        }

        private void btnAtualizarped_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            string dataOriginal = maskedTextBox1.Text;
            DateTime dataConvertida = DateTime.ParseExact(dataOriginal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string novaData = dataConvertida.ToString("yyyy-MM-dd");

            String sql = $@"UPDATE Pedido
                SET Data_Envio_Pedido = @data
                WHERE Id_Pedido = @idpedido;
                ";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@data", novaData);
            cmd.Parameters.AddWithValue("@idpedido", Valores.idped);
            con.exSQLParametros(cmd);
            consulta();
            carregamento();
        }

        private void btnCancelarped_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            String sql = $@"UPDATE Pedido
                SET Status_Pedido = 0
                WHERE Id_Pedido = @idpedido;
                ";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@idpedido", Valores.idped);
            con.exSQLParametros(cmd);
            consulta();
            carregamento();
        }
    }
}
