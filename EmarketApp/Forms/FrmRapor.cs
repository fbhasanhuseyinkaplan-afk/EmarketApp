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
        DataGridView grid;
        TextBox txtAra;
        Button btnListele;
        Button btnTemizle;
        Label lblBos;

        public FrmRapor()
        {
            FrmStil.FormAyarla(this);
            TasarimOlustur();
            Load += delegate { Listele(); };
        }

        void TasarimOlustur()
        {
            Label baslik = new Label();
            baslik.Text = "Satış Raporları";
            baslik.Font = new Font("Segoe UI", 20, FontStyle.Bold | FontStyle.Italic);
            baslik.ForeColor = Color.White;
            baslik.SetBounds(24, 20, 320, 40);
            Controls.Add(baslik);

            Panel arac = new Panel();
            arac.SetBounds(24, 74, 980, 72);
            FrmStil.KartStili(arac);
            Controls.Add(arac);

            txtAra = new TextBox();
            txtAra.SetBounds(18, 20, 320, 30);
            FrmStil.InputStili(txtAra);
            txtAra.TextChanged += delegate { Filtrele(); };

            btnListele = FrmStil.Buton("⟳ Listele", 356, 17, 130);
            btnListele.Click += delegate { Listele(); };
            btnTemizle = FrmStil.Buton("✕ Temizle", 498, 17, 130);
            btnTemizle.Click += delegate { txtAra.Clear(); Filtrele(); };

            arac.Controls.AddRange(new Control[] { txtAra, btnListele, btnTemizle });

            Panel listeKart = new Panel();
            listeKart.SetBounds(24, 160, 980, 510);
            FrmStil.KartStili(listeKart);
            Controls.Add(listeKart);

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FrmStil.GridStili(grid);

            lblBos = FrmStil.BosLabel("Rapor verisi bulunamadı.");
            listeKart.Controls.Add(lblBos);
            listeKart.Controls.Add(grid);
        }

        void Listele()
        {
            try
            {
                raporTable = satisService.SatisListe();
                grid.DataSource = raporTable;
                if (grid.Columns.Contains("ToplamTutar")) grid.Columns["ToplamTutar"].HeaderText = "Toplam Tutar (₺)";
                if (grid.Columns.Contains("SatisTarihi")) grid.Columns["SatisTarihi"].HeaderText = "Satış Tarihi";
                if (grid.Columns.Contains("Musteri")) grid.Columns["Musteri"].HeaderText = "Müşteri";
                lblBos.Visible = raporTable == null || raporTable.Rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show("Rapor verisi alınamadı: " + ex.Message); }
        }

        void Filtrele()
        {
            if (raporTable == null) return;
            string araMetni = txtAra.Text == null ? "" : txtAra.Text.Replace("'", "''");
            raporTable.DefaultView.RowFilter = "Convert(Musteri, 'System.String') LIKE '%" + araMetni + "%'";
            lblBos.Visible = raporTable.DefaultView.Count == 0;
        }
    }
}
