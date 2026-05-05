using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EmarketApp.Service;

namespace EmarketApp.Forms
{
    public class FrmSatis : Form
    {
        private readonly UrunService urunService = new UrunService();
        private readonly MusteriService musteriService = new MusteriService();
        private readonly SatisService satisService = new SatisService();

        ComboBox comboUrun, comboMusteri;
        NumericUpDown numAdet;
        TextBox txtAra;
        Button btnSepeteEkle, btnSatisYap, btnTemizle;
        DataGridView gridSepet;
        Label lblToplam;
        decimal genelToplam;

        public FrmSatis() { TasarimOlustur(); Load += FrmSatis_Load; }

        void TasarimOlustur()
        {
            BackColor = Color.Black;
            comboUrun = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            comboUrun.SetBounds(30, 30, 220, 30);
            comboMusteri = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            comboMusteri.SetBounds(260, 30, 220, 30);
            numAdet = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 1 };
            numAdet.SetBounds(490, 30, 80, 30);
            txtAra = new TextBox(); txtAra.SetBounds(30, 75, 220, 30); txtAra.TextChanged += (s, e) => UrunFiltrele();
            btnSepeteEkle = Buton("Sepete Ekle", 600, 26); btnSepeteEkle.Click += BtnSepeteEkle_Click;
            btnSatisYap = Buton("Satışı Tamamla", 730, 26); btnSatisYap.Click += BtnSatisYap_Click;
            btnTemizle = Buton("Temizle", 260, 72); btnTemizle.Click += (s, e) => SepetTemizle();

            Panel p = new Panel();
            p.BackColor = Color.FromArgb(32, 26, 46);
            p.SetBounds(30, 90, 920, 120);
            p.Controls.AddRange(new Control[] { comboUrun, comboMusteri, numAdet, txtAra, btnSepeteEkle, btnSatisYap, btnTemizle }); Controls.Add(p);

            gridSepet = new DataGridView { ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridSepet.SetBounds(30, 230, 920, 420);
            gridSepet.Columns.Add("UrunID", "Ürün ID");
            gridSepet.Columns.Add("Urun", "Ürün");
            gridSepet.Columns.Add("Adet", "Adet");
            gridSepet.Columns.Add("Fiyat", "Birim Fiyat");
            gridSepet.Columns.Add("Toplam", "Toplam");
            gridSepet.Columns[0].Visible = false;
            Controls.Add(gridSepet);

            lblToplam = new Label { ForeColor = Color.Lime, Font = new Font("Segoe UI", 14, FontStyle.Bold), Text = "Toplam: ₺0" };
            lblToplam.SetBounds(720, 660, 230, 35); Controls.Add(lblToplam);
        }

        Button Buton(string text, int x, int y)
        {
            Button b = new Button();
            b.Text = text;
            b.BackColor = Color.FromArgb(100, 44, 170);
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.SetBounds(x, y, 120, 35);
            return b;
        }

        void FrmSatis_Load(object sender, EventArgs e)
        {
            try
            {
                comboUrun.DataSource = urunService.Liste(); comboUrun.DisplayMember = "UrunAd"; comboUrun.ValueMember = "UrunID";
                comboMusteri.DataSource = musteriService.Liste(); comboMusteri.DisplayMember = "Musteri"; comboMusteri.ValueMember = "MusteriID";
            }
            catch (Exception ex) { MessageBox.Show("Veri yüklenemedi: " + ex.Message); }
        }

        void UrunFiltrele()
        {
            if (comboUrun.DataSource == null) return;
            DataTable urunTable = comboUrun.DataSource as DataTable;
            if (urunTable == null) return;
            string araMetni = txtAra.Text == null ? "" : txtAra.Text.Replace("'", "''");
            urunTable.DefaultView.RowFilter = "UrunAd LIKE '%" + araMetni + "%'";
        }

        void BtnSepeteEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboUrun.SelectedValue == null) { MessageBox.Show("Ürün seçiniz."); return; }
                int urunId = Convert.ToInt32(comboUrun.SelectedValue); int adet = (int)numAdet.Value;
                decimal fiyat = urunService.SonFiyatGetir(urunId); if (fiyat <= 0) { MessageBox.Show("Ürün fiyatı bulunamadı."); return; }
                decimal toplam = fiyat * adet;
                gridSepet.Rows.Add(urunId, comboUrun.Text, adet, fiyat.ToString("N2"), toplam.ToString("N2"));
                genelToplam += toplam; lblToplam.Text = string.Format("Toplam: ₺{0:N2}", genelToplam);
            }
            catch (Exception ex) { MessageBox.Show("Sepete ekleme hatası: " + ex.Message); }
        }

        void BtnSatisYap_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboMusteri.SelectedValue == null || gridSepet.Rows.Count == 0) { MessageBox.Show("Müşteri seçin ve sepete ürün ekleyin."); return; }
                int satisId = satisService.SatisEkle(Convert.ToInt32(comboMusteri.SelectedValue), 1, genelToplam);
                foreach (DataGridViewRow row in gridSepet.Rows)
                    satisService.SatisDetayEkle(satisId, Convert.ToInt32(row.Cells["UrunID"].Value), Convert.ToInt32(row.Cells["Adet"].Value), Convert.ToDecimal(row.Cells["Fiyat"].Value));
                MessageBox.Show("Satış tamamlandı.");
                SepetTemizle();
            }
            catch (Exception ex) { MessageBox.Show("Satış işlemi başarısız: " + ex.Message); }
        }

        void SepetTemizle() { gridSepet.Rows.Clear(); genelToplam = 0; lblToplam.Text = "Toplam: ₺0"; }
    }
}
