using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EmarketApp.Forms
{
    public static class FrmStil
    {
        public static Color ArkaPlan1 = Color.FromArgb(7, 6, 14);
        public static Color ArkaPlan2 = Color.FromArgb(36, 12, 58);
        public static Color Kart = Color.FromArgb(24, 20, 38);
        public static Color Kenar = Color.FromArgb(92, 62, 140);
        public static Color Vurgu = Color.FromArgb(120, 65, 210);

        public static void FormAyarla(Form frm)
        {
            frm.BackColor = Color.Black;
            frm.ForeColor = Color.White;
            frm.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            frm.Padding = new Padding(24);
            frm.Resize += delegate { frm.Invalidate(); };
            frm.Paint += FormPaint;
        }

        static void FormPaint(object sender, PaintEventArgs e)
        {
            Form frm = sender as Form;
            if (frm == null) return;
            using (LinearGradientBrush brush = new LinearGradientBrush(frm.ClientRectangle, ArkaPlan1, ArkaPlan2, LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, frm.ClientRectangle);
            }
        }

        public static void KartStili(Panel panel)
        {
            panel.BackColor = Kart;
            panel.Paint += delegate(object s, PaintEventArgs e)
            {
                Rectangle r = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                using (Pen p = new Pen(Kenar, 1)) e.Graphics.DrawRectangle(p, r);
            };
        }

        public static void InputStili(Control c)
        {
            c.BackColor = Color.FromArgb(16, 14, 28);
            c.ForeColor = Color.White;
            c.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        public static void GridStili(DataGridView g)
        {
            g.BackgroundColor = Color.FromArgb(18, 16, 30);
            g.BorderStyle = BorderStyle.None;
            g.GridColor = Color.FromArgb(80, 70, 105);
            g.RowHeadersVisible = false;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersHeight = 38;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 32, 120);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            g.DefaultCellStyle.BackColor = Color.FromArgb(25, 22, 38);
            g.DefaultCellStyle.ForeColor = Color.White;
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 65, 210);
            g.DefaultCellStyle.SelectionForeColor = Color.White;
            g.DefaultCellStyle.Padding = new Padding(4);
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 26, 45);
        }

        public static Button Buton(string text, int x, int y, int w)
        {
            Button b = new Button();
            b.Text = text;
            b.SetBounds(x, y, w, 38);
            b.BackColor = Vurgu;
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Color.White;
            b.Cursor = Cursors.Hand;
            b.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            b.MouseEnter += delegate { b.BackColor = Color.FromArgb(148, 88, 240); };
            b.MouseLeave += delegate { b.BackColor = Vurgu; };
            return b;
        }

        public static Label BosLabel(string text)
        {
            Label l = new Label();
            l.Text = text;
            l.ForeColor = Color.FromArgb(205, 190, 235);
            l.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Dock = DockStyle.Fill;
            l.Visible = false;
            return l;
        }
    }
}
