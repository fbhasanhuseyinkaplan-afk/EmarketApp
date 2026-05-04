using System.Drawing;
using System.Windows.Forms;
using EmarketApp.DAL;

namespace EmarketApp.Forms
{
    public class FrmStok : Form
    {
        DataGridView grid;

        public FrmStok()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);

            Label baslik = new Label();
            baslik.Text = "Stok Listesi";
            baslik.Font = new Font("Arial", 18, FontStyle.Bold);
            baslik.SetBounds(25, 20, 300, 35);
            this.Controls.Add(baslik);

            grid = new DataGridView();
            grid.SetBounds(25, 75, 850, 430);
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(grid);

            this.Load += delegate { grid.DataSource = new StokDAL().Liste(); };
        }
    }
}
