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
    public partial class Form7 : Form
    {
        private List<Movimento> movimentos;
        public Form7()
        {
            InitializeComponent();
            InicializarMovimentos();
            AtualizarGrid(movimentos);
        }

        private void InicializarMovimentos()
        {
            movimentos = new List<Movimento>
            {
                new Movimento { Data = DateTime.Today.AddDays(-10), Tipo = "Receita", Descricao = "Venda: Pedido #1001", Valor = 259.90m },
                new Movimento { Data = DateTime.Today.AddDays(-8), Tipo = "Despesa", Descricao = "Frete Correios", Valor = -35.00m },
                new Movimento { Data = DateTime.Today.AddDays(-6), Tipo = "Receita", Descricao = "Venda: Pedido #1002", Valor = 199.90m },
                new Movimento { Data = DateTime.Today.AddDays(-5), Tipo = "Despesa", Descricao = "Compra de Embalagens", Valor = -50.00m },
                new Movimento { Data = DateTime.Today.AddDays(-3), Tipo = "Receita", Descricao = "Venda: Pedido #1003", Valor = 379.90m },
                new Movimento { Data = DateTime.Today.AddDays(-1), Tipo = "Despesa", Descricao = "Taxa plataforma", Valor = -25.00m },
            };
        }

        private void AtualizarGrid(List<Movimento> lista)
        {
            dataGridView1.Rows.Clear();
            decimal totalReceitas = 0;
            decimal totalDespesas = 0;

            foreach (var m in lista)
            {
                dataGridView1.Rows.Add(m.Data.ToShortDateString(), m.Tipo, m.Descricao, m.Valor.ToString("C"));

                if (m.Tipo == "Receita") totalReceitas += m.Valor;
                else totalDespesas += m.Valor; // negativo
            }

            lblReceitas.Text = $"Receitas: {totalReceitas:C}";
            lblDespesas.Text = $"Despesas: {totalDespesas:C}";
            lblSaldo.Text = $"Saldo: {(totalReceitas + totalDespesas):C}";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dataInicial = dateTimeInicio.Value.Date;
            DateTime dataFinal = dateTimeFinal.Value.Date;

            var filtrados = movimentos
                .Where(m => m.Data >= dataInicial && m.Data <= dataFinal)
                .ToList();

            AtualizarGrid(filtrados);
        }
    }
    public class Movimento
    {
        public DateTime Data { get; set; }
        public string Tipo { get; set; } // Receita ou Despesa
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
