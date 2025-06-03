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
    public partial class Form8 : Form
    {
        
        ClasseConexao con;
        DataTable dt;
        public Form8()
        {
            InitializeComponent();
           
           
        }


        private void formatarGrid()
        {
            dataGridView1.Columns["id_cliente"].Visible = false; //esconde a coluna senha
            dataGridView1.Columns["senha_cliente"].Visible = false; //esconde a coluna senha
            dataGridView1.Columns["img_perfil_cliente"].Visible = false; //esconde a coluna imgPerfil
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridView1.AllowUserToAddRows = false; //ESCONDE A NOVA LINHA DO GRID
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false; //ESCONDE O PONTEIRO DO GRID
            dataGridView1.ReadOnly = true;
            //permite personalizar o grid
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //altera a cor das linhas alternadas no grid
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Beige;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            //altera o nome das colunas
            dataGridView1.Columns[1].HeaderText = "CPF";
            dataGridView1.Columns[2].HeaderText = "Nome";
            dataGridView1.Columns[3].HeaderText = "Nome Usuario";
            dataGridView1.Columns[4].HeaderText = "Email";
            dataGridView1.Columns[6].HeaderText = "Telefone";
            //grid.Columns[3].HeaderText = "PREÇO UNITÁRIO";
            dataGridView1.Columns[1].Width = 40;
            dataGridView1.Columns[2].Width = 40;
            //não permite seleção de multiplas linhas    
            dataGridView1.MultiSelect = false;
            //ao clicar, seleciona a linha inteira
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //Expande a célula automáticamente
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }



       

        private void Button1_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            dt = con.executarSQL("SELECT * FROM Cliente");
            dataGridView1.DataSource = dt;
            formatarGrid();
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
        
        }
    }

   
}
