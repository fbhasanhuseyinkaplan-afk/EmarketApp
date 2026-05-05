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
        Label lblToplam, lblBos;
        decimal genelToplam;

        public FrmSatis() { FrmStil.FormAyarla(this); TasarimOlustur(); Load += FrmSatis_Load; }

        void TasarimOlustur()
        {
            Label baslik = new Label();
            baslik.Text = "Satış Ekranı";
            baslik.Font = new Font("Segoe UI", 20, FontStyle.Bold | FontStyle.Italic);
            baslik.ForeColor = Color.White;
            baslik.SetBounds(24, 20, 320, 40);
            Controls.Add(baslik);

            Panel p = new Panel();
            p.SetBounds(24, 74, 980, 120);
            FrmStil.KartStili(p);

            comboUrun = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            comboUrun.SetBounds(16, 20, 260, 30); FrmStil.InputStili(comboUrun);
            comboMusteri = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            comboMusteri.SetBounds(286, 20, 260, 30); FrmStil.InputStili(comboMusteri);
            numAdet = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 1 };
            numAdet.SetBounds(556, 20, 90, 30); FrmStil.InputStili(numAdet);

            txtAra = new TextBox(); txtAra.SetBounds(16, 66, 260, 30); FrmStil.InputStili(txtAra); txtAra.TextChanged += delegate { UrunFiltrele(); };
            btnSepeteEkle = FrmStil.Buton("＋ Sepete", 666, 17, 140); btnSepeteEkle.Click += BtnSepeteEkle_Click;
            btnSatisYap = FrmStil.Buton("✔ Satışı Tamamla", 816, 17, 150); btnSatisYap.Click += BtnSatisYap_Click;
            btnTemizle = FrmStil.Buton("✕ Temizle", 286, 63, 120); btnTemizle.Click += delegate { SepetTemizle(); };

            p.Controls.AddRange(new Control[] { comboUrun, comboMusteri, numAdet, txtAra, btnSepeteEkle, btnSatisYap, btnTemizle }); Controls.Add(p);

            Panel listeKart = new Panel();
            listeKart.SetBounds(24, 206, 980, 464);
            FrmStil.KartStili(listeKart);
            Controls.Add(listeKart);

            gridSepet = new DataGridView { ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridSepet.Dock = DockStyle.Fill;
            FrmStil.GridStili(gridSepet);
            gridSepet.Columns.Add("UrunID", "Ürün ID");
            gridSepet.Columns.Add("Urun", "Ürün");
            gridSepet.Columns.Add("Adet", "Adet");
            gridSepet.Columns.Add("Fiyat", "Birim Fiyat (₺)");
            gridSepet.Columns.Add("Toplam", "Toplam (₺)");
            gridSepet.Columns[0].Visible = false;

            lblBos = FrmStil.BosLabel("Sepetiniz boş. Satış için ürün ekleyin.");
            listeKart.Controls.Add(lblBos);
            listeKart.Controls.Add(gridSepet);

            lblToplam = new Label { ForeColor = Color.FromArgb(110, 255, 170), Font = new Font("Segoe UI", 14, FontStyle.Bold), Text = "Toplam: ₺0" };
            lblToplam.SetBounds(790, 676, 220, 35); Controls.Add(lblToplam);
        }

        void FrmSatis_Load(object sender, EventArgs e)
        {
            try
            {
                comboUrun.DataSource = urunService.Liste(); comboUrun.DisplayMember = "UrunAd"; comboUrun.ValueMember = "UrunID";
                comboMusteri.DataSource = musteriService.Liste(); comboMusteri.DisplayMember = "Musteri"; comboMusteri.ValueMember = "MusteriID";
                lblBos.Visible = true;
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
                lblBos.Visible = gridSepet.Rows.Count == 0;
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

        void SepetTemizle() { gridSepet.Rows.Clear(); genelToplam = 0; lblToplam.Text = "Toplam: ₺0"; lblBos.Visible = true; }
    }
}
