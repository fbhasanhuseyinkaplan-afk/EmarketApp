using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EmarketApp.Service;

namespace EmarketApp.Forms
{
    public class FrmRapor : Form
    {
        private readonly SatisService satisService = new SatisService();
        private DataTable raporTable;
        DataGridView grid; TextBox txtAra; Button btnListele, btnTemizle;

        public FrmRapor()
        {
            BackColor = Color.Black;
            txtAra = new TextBox(); txtAra.SetBounds(25, 25, 260, 30); txtAra.TextChanged += (s, e) => Filtrele();
            btnListele = new Button { Text = "Listele" }; btnListele.SetBounds(300, 25, 110, 30); btnListele.Click += (s, e) => Listele();
            btnTemizle = new Button { Text = "Temizle" }; btnTemizle.SetBounds(420, 25, 110, 30); btnTemizle.Click += (s, e) => { txtAra.Clear(); Filtrele(); };
            grid = new DataGridView { ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill }; grid.SetBounds(25, 75, 930, 600);
            Controls.AddRange(new Control[] { txtAra, btnListele, btnTemizle, grid });
            Load += (s, e) => Listele();
        }

        void Listele()
        {
            try
            {
                raporTable = satisService.SatisListe();
                grid.DataSource = raporTable;
                if (grid.Columns.Contains("ToplamTutar")) grid.Columns["ToplamTutar"].HeaderText = "Toplam Tutar";
                if (grid.Columns.Contains("SatisTarihi")) grid.Columns["SatisTarihi"].HeaderText = "Satış Tarihi";
            }
            catch (Exception ex) { MessageBox.Show("Rapor verisi alınamadı: " + ex.Message); }
        }

        void Filtrele()
        {
            if (raporTable == null) return;
            string araMetni = txtAra.Text == null ? "" : txtAra.Text.Replace("'", "''");
            raporTable.DefaultView.RowFilter = "Convert(Musteri, 'System.String') LIKE '%" + araMetni + "%'";
        }
    }
}
