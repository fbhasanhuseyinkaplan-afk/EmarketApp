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
        DataGridView grid;
        TextBox txtAra;
        Button btnYenile;
        Button btnTemizle;
        Label lblBos;

        public FrmStok()
        {
            FrmStil.FormAyarla(this);
            TasarimOlustur();
            Load += delegate { Listele(); };
        }

        void TasarimOlustur()
        {
            Label baslik = new Label();
            baslik.Text = "Stok Yönetimi";
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
            txtAra.Text = "Ürün ara...";
            FrmStil.InputStili(txtAra);
            txtAra.TextChanged += delegate { Filtrele(); };

            btnYenile = FrmStil.Buton("⟳ Listele", 356, 17, 130);
            btnYenile.Click += delegate { Listele(); };
            btnTemizle = FrmStil.Buton("✕ Temizle", 498, 17, 130);
            btnTemizle.Click += delegate { txtAra.Clear(); Filtrele(); };

            arac.Controls.AddRange(new Control[] { txtAra, btnYenile, btnTemizle });

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

            lblBos = FrmStil.BosLabel("Gösterilecek stok verisi bulunamadı.");

            listeKart.Controls.Add(lblBos);
            listeKart.Controls.Add(grid);
        }

        void Listele()
        {
            try
            {
                stokTable = stokService.Liste();
                grid.DataSource = stokTable;
                if (grid.Columns.Contains("StokID")) grid.Columns["StokID"].HeaderText = "Stok No";
                if (grid.Columns.Contains("UrunID")) grid.Columns["UrunID"].HeaderText = "Ürün No";
                if (grid.Columns.Contains("UrunAd")) grid.Columns["UrunAd"].HeaderText = "Ürün Adı";
                if (grid.Columns.Contains("Miktar")) grid.Columns["Miktar"].HeaderText = "Stok Miktarı";
                if (grid.Columns.Contains("GuncellenmeTarihi"))
                {
                    grid.Columns["GuncellenmeTarihi"].HeaderText = "Güncellenme Tarihi";
                    grid.Columns["GuncellenmeTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                }
                lblBos.Visible = stokTable == null || stokTable.Rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show("Stok verisi alınamadı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Filtrele()
        {
            if (stokTable == null) return;
            string araMetni = txtAra.Text == null ? "" : txtAra.Text.Replace("'", "''");
            stokTable.DefaultView.RowFilter = "Convert(UrunAd, 'System.String') LIKE '%" + araMetni + "%'";
            lblBos.Visible = stokTable.DefaultView.Count == 0;
        }
    }
}
