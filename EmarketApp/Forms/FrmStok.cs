using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EmarketApp.Service;

namespace EmarketApp.Forms
{
    public class FrmStok : Form
    {
        private readonly StokService stokService = new StokService();
        private DataTable stokTable;
        DataGridView grid; TextBox txtAra; Button btnYenile, btnTemizle;

        public FrmStok()
        {
            BackColor = Color.Black;
            txtAra = new TextBox(); txtAra.SetBounds(25, 25, 260, 30); txtAra.TextChanged += (s, e) => Filtrele();
            btnYenile = new Button { Text = "Listele" }; btnYenile.SetBounds(300, 25, 110, 30); btnYenile.Click += (s, e) => Listele();
            btnTemizle = new Button { Text = "Temizle" }; btnTemizle.SetBounds(420, 25, 110, 30); btnTemizle.Click += (s, e) => { txtAra.Clear(); Filtrele(); };
            grid = new DataGridView { ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill }; grid.SetBounds(25, 75, 930, 600);
            Controls.AddRange(new Control[] { txtAra, btnYenile, btnTemizle, grid });
            Load += (s, e) => Listele();
        }

        void Listele()
        {
            try
            {
                stokTable = stokService.Liste();
                grid.DataSource = stokTable;
                if (grid.Columns.Contains("UrunAd")) grid.Columns["UrunAd"].HeaderText = "Ürün";
                if (grid.Columns.Contains("Miktar")) grid.Columns["Miktar"].HeaderText = "Stok Miktarı";
            }
            catch (Exception ex) { MessageBox.Show("Stok verisi alınamadı: " + ex.Message); }
        }

        void Filtrele()
        {
            if (stokTable == null) return;
            stokTable.DefaultView.RowFilter = $"Convert(UrunAd, 'System.String') LIKE '%{txtAra.Text.Replace("'", "''")}%'";
        }
    }
}
