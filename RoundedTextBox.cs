using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedTextBox : UserControl
{
    private TextBox textBox;

    [Browsable(true)]
    public string HintText { get; set; }

    public RoundedTextBox()
    {
        this.DoubleBuffered = true;
        this.BackColor = Color.White;
        this.ForeColor = Color.Black;
        this.Font = new Font("Segoe UI", 10);

        textBox = new TextBox();
        textBox.BorderStyle = BorderStyle.None;
        textBox.BackColor = this.BackColor;
        textBox.ForeColor = this.ForeColor;
        textBox.Font = this.Font;
        textBox.Dock = DockStyle.Fill;
        textBox.Margin = new Padding(5);
        textBox.Multiline = false;

        this.Padding = new Padding(10, 6, 10, 6);
        this.Controls.Add(textBox);

        this.Size = new Size(200, 35);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        int borderRadius = 15;
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, borderRadius))
        using (Pen pen = new Pen(Color.Gray, 1))
        {
            this.Region = new Region(path);
            g.DrawPath(pen, path);
        }
    }

    private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }

    public override string Text
    {
        get => textBox.Text;
        set => textBox.Text = value;
    }

    public TextBox InnerTextBox => textBox;
}
