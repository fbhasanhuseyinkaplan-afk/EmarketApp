using System;
using System.Drawing;
using System.Windows.Forms;
using EmarketApp.Service;

namespace EmarketApp.Forms
{
    public class FrmUrunYonetim : Form
    {
        TextBox txtUrunAdi;
        ComboBox comboKategori;
        Button btnEkle, btnSil, btnGuncelle, btnTemizle;
        TextBox txtAra;
        DataGridView grid;
        Label lblBos;

        KategoriService kategoriService = new KategoriService();
        UrunService urunService = new UrunService();

        public FrmUrunYonetim()
        {
            FrmStil.FormAyarla(this);
            TasarimOlustur();
            this.Load += FrmUrunYonetim_Load;
        }

        void TasarimOlustur()
        {
            Label baslik = new Label();
            baslik.Text = "Ürün Yönetimi";
            baslik.ForeColor = Color.White;
            baslik.Font = new Font("Segoe UI", 20, FontStyle.Bold | FontStyle.Italic);
            baslik.SetBounds(24, 20, 350, 40);
            Controls.Add(baslik);

            Panel kart = new Panel();
            kart.SetBounds(24, 74, 980, 140);
            FrmStil.KartStili(kart);
            Controls.Add(kart);

            Label lblAd = LabelOlustur("Ürün Adı", 18, 20);
            kart.Controls.Add(lblAd);
            txtUrunAdi = TextBoxOlustur(18, 46);
            txtUrunAdi.Width = 240;
            kart.Controls.Add(txtUrunAdi);

            Label lblKat = LabelOlustur("Kategori", 270, 20);
            kart.Controls.Add(lblKat);
            comboKategori = new ComboBox();
            comboKategori.SetBounds(270, 46, 240, 30);
            comboKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            comboKategori.FlatStyle = FlatStyle.Flat;
            FrmStil.InputStili(comboKategori);
            kart.Controls.Add(comboKategori);

            txtAra = TextBoxOlustur(18, 94);
            txtAra.Width = 240;
            txtAra.TextChanged += delegate { Filtrele(); };
            kart.Controls.Add(txtAra);

            btnEkle = FrmStil.Buton("＋ Ekle", 560, 23, 110);
            btnGuncelle = FrmStil.Buton("↻ Güncelle", 680, 23, 120);
            btnSil = FrmStil.Buton("🗑 Sil", 810, 23, 90);
            btnTemizle = FrmStil.Buton("✕ Temizle", 560, 71, 140);
            kart.Controls.AddRange(new Control[] { btnEkle, btnGuncelle, btnSil, btnTemizle });

            btnEkle.Click += btnEkle_Click;
            btnGuncelle.Click += btnGuncelle_Click;
            btnSil.Click += btnSil_Click;
            btnTemizle.Click += delegate { txtUrunAdi.Clear(); comboKategori.SelectedIndex = -1; txtAra.Clear(); Listele(); };

            Panel listeKart = new Panel();
            listeKart.SetBounds(24, 228, 980, 442);
            FrmStil.KartStili(listeKart);
            Controls.Add(listeKart);

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FrmStil.GridStili(grid);
            grid.CellClick += grid_CellClick;

            lblBos = FrmStil.BosLabel("Listelenecek ürün bulunamadı.");
            listeKart.Controls.Add(lblBos);
            listeKart.Controls.Add(grid);
        }

        Label LabelOlustur(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.SetBounds(x, y, 160, 22);
            return lbl;
        }

        TextBox TextBoxOlustur(int x, int y)
        {
            TextBox txt = new TextBox();
            txt.SetBounds(x, y, 250, 30);
            txt.BorderStyle = BorderStyle.FixedSingle;
            FrmStil.InputStili(txt);
            return txt;
        }

        private void FrmUrunYonetim_Load(object sender, EventArgs e)
        {
            comboKategori.DataSource = kategoriService.Liste();
            comboKategori.DisplayMember = "KategoriAdi";
            comboKategori.ValueMember = "KategoriID";
            Listele();
        }

        void Filtrele()
        {
            if (grid.DataSource == null) return;
            System.Data.DataTable dt = (System.Data.DataTable)grid.DataSource;
            string araMetni = txtAra.Text == null ? "" : txtAra.Text.Replace("'", "''");
            dt.DefaultView.RowFilter = "UrunAd LIKE '%" + araMetni + "%'";
            lblBos.Visible = dt.DefaultView.Count == 0;
        }

        void Listele()
        {
            grid.DataSource = urunService.Liste();
            if (grid.Columns.Contains("UrunID")) grid.Columns["UrunID"].HeaderText = "Ürün No";
            if (grid.Columns.Contains("UrunAd")) grid.Columns["UrunAd"].HeaderText = "Ürün Adı";
            if (grid.Columns.Contains("KategoriAdi")) grid.Columns["KategoriAdi"].HeaderText = "Kategori";
            if (grid.Columns.Contains("EklenmeTarihi"))
            {
                grid.Columns["EklenmeTarihi"].HeaderText = "Eklenme Tarihi";
                grid.Columns["EklenmeTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }
            lblBos.Visible = grid.Rows.Count == 0;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUrunAdi.Text.Trim() == "") { MessageBox.Show("Ürün adı boş olamaz."); return; }
                urunService.Ekle(txtUrunAdi.Text.Trim(), Convert.ToInt32(comboKategori.SelectedValue));
                Listele(); txtUrunAdi.Clear(); MessageBox.Show("Ürün eklendi.");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.CurrentRow == null) { MessageBox.Show("Ürün seç."); return; }
                if (txtUrunAdi.Text.Trim() == "") { MessageBox.Show("Ürün adı boş olamaz."); return; }
                string eskiAd = grid.CurrentRow.Cells["UrunAd"].Value.ToString();
                string eskiKategori = grid.CurrentRow.Cells["KategoriAdi"].Value.ToString();
                if (eskiAd == txtUrunAdi.Text.Trim() && eskiKategori == comboKategori.Text) { MessageBox.Show("Hiçbir değişiklik yapılmadı."); return; }
                int id = Convert.ToInt32(grid.CurrentRow.Cells["UrunID"].Value);
                urunService.Guncelle(id, txtUrunAdi.Text.Trim(), Convert.ToInt32(comboKategori.SelectedValue));
                Listele(); txtUrunAdi.Clear(); MessageBox.Show("Ürün güncellendi.");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.CurrentRow == null) { MessageBox.Show("Silinecek ürün seç."); return; }
                DialogResult cevap = MessageBox.Show("Seçili ürünü silmek istiyor musun?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == DialogResult.No) return;
                int id = Convert.ToInt32(grid.CurrentRow.Cells["UrunID"].Value);
                urunService.Sil(id); Listele(); txtUrunAdi.Clear(); MessageBox.Show("Ürün silindi.");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtUrunAdi.Text = grid.Rows[e.RowIndex].Cells["UrunAd"].Value.ToString();
                if (grid.Columns.Contains("KategoriAdi")) comboKategori.Text = grid.Rows[e.RowIndex].Cells["KategoriAdi"].Value.ToString();
            }
        }
    }
}
