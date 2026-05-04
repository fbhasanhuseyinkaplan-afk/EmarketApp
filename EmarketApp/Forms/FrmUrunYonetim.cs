using System;
using System.Drawing;
using System.Windows.Forms;
using EmarketApp.DAL;

namespace EmarketApp.Forms
{
    public class FrmUrunYonetim : Form
    {
        TextBox txtUrunAdi;
        ComboBox comboKategori;
        Button btnEkle, btnSil, btnGuncelle;
        DataGridView grid;

        KategoriDAL kdal = new KategoriDAL();
        UrunDAL udal = new UrunDAL();

        public FrmUrunYonetim()
        {
            TasarimOlustur();
            this.Load += FrmUrunYonetim_Load;
        }

        void TasarimOlustur()
        {
            this.BackColor = Color.Black;

            Label baslik = new Label();
            baslik.Text = "Ürün Yönetimi";
            baslik.ForeColor = Color.White;
            baslik.Font = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic);
            baslik.SetBounds(30, 25, 350, 40);
            this.Controls.Add(baslik);

            Panel kart = new Panel();
            kart.SetBounds(30, 90, 900, 150);
            kart.BackColor = Color.FromArgb(32, 26, 46);
            kart.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(100, 44, 170), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, kart.Width - 1, kart.Height - 1);
                }
            };
            this.Controls.Add(kart);

            Label lblAd = LabelOlustur("Ürün Adı", 30, 35);
            kart.Controls.Add(lblAd);

            txtUrunAdi = TextBoxOlustur(140, 32);
            kart.Controls.Add(txtUrunAdi);

            Label lblKat = LabelOlustur("Kategori", 30, 85);
            kart.Controls.Add(lblKat);

            comboKategori = new ComboBox();
            comboKategori.SetBounds(140, 82, 250, 28);
            comboKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            comboKategori.BackColor = Color.FromArgb(18, 12, 30);
            comboKategori.ForeColor = Color.White;
            comboKategori.FlatStyle = FlatStyle.Flat;
            comboKategori.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            kart.Controls.Add(comboKategori);

            btnEkle = ButonOlustur("Ekle", 450, 35);
            btnGuncelle = ButonOlustur("Güncelle", 570, 35);
            btnSil = ButonOlustur("Sil", 710, 35);

            kart.Controls.Add(btnEkle);
            kart.Controls.Add(btnGuncelle);
            kart.Controls.Add(btnSil);

            btnEkle.Click += btnEkle_Click;
            btnGuncelle.Click += btnGuncelle_Click;
            btnSil.Click += btnSil_Click;

            grid = new DataGridView();
            grid.SetBounds(30, 270, 900, 430);
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            grid.BackgroundColor = Color.FromArgb(20, 16, 32);
            grid.GridColor = Color.FromArgb(70, 60, 95);
            grid.BorderStyle = BorderStyle.None;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 20, 75);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            grid.EnableHeadersVisualStyles = false;

            grid.DefaultCellStyle.BackColor = Color.FromArgb(30, 24, 45);
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 44, 170);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            grid.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 24, 45);
            grid.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            grid.CellClick += grid_CellClick;
            this.Controls.Add(grid);
        }

        Label LabelOlustur(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold | FontStyle.Italic);
            lbl.SetBounds(x, y, 100, 25);
            return lbl;
        }

        TextBox TextBoxOlustur(int x, int y)
        {
            TextBox txt = new TextBox();
            txt.SetBounds(x, y, 250, 28);
            txt.BackColor = Color.FromArgb(18, 12, 30);
            txt.ForeColor = Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return txt;
        }

        Button ButonOlustur(string text, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.SetBounds(x, y, 110, 38);
            btn.BackColor = Color.FromArgb(100, 44, 170);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold | FontStyle.Italic);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += delegate
            {
                btn.BackColor = Color.FromArgb(140, 70, 220);
                btn.FlatAppearance.BorderSize = 2;
            };

            btn.MouseLeave += delegate
            {
                btn.BackColor = Color.FromArgb(100, 44, 170);
                btn.FlatAppearance.BorderSize = 1;
            };

            return btn;
        }

        private void FrmUrunYonetim_Load(object sender, EventArgs e)
        {
            comboKategori.DataSource = kdal.Liste();
            comboKategori.DisplayMember = "KategoriAdi";
            comboKategori.ValueMember = "KategoriID";

            Listele();
        }

        void Listele()
        {
            grid.DataSource = udal.Liste();

            if (grid.Columns.Contains("EklenmeTarihi"))
                grid.Columns["EklenmeTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUrunAdi.Text.Trim() == "")
                {
                    MessageBox.Show("Ürün adı boş olamaz.");
                    return;
                }

                udal.Ekle(txtUrunAdi.Text.Trim(), Convert.ToInt32(comboKategori.SelectedValue));
                Listele();
                txtUrunAdi.Clear();

                MessageBox.Show("Ürün eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.CurrentRow == null)
                {
                    MessageBox.Show("Ürün seç.");
                    return;
                }

                if (txtUrunAdi.Text.Trim() == "")
                {
                    MessageBox.Show("Ürün adı boş olamaz.");
                    return;
                }

                string eskiAd = grid.CurrentRow.Cells["UrunAd"].Value.ToString();
                string eskiKategori = grid.CurrentRow.Cells["KategoriAdi"].Value.ToString();

                if (eskiAd == txtUrunAdi.Text.Trim() && eskiKategori == comboKategori.Text)
                {
                    MessageBox.Show("Hiçbir değişiklik yapılmadı.");
                    return;
                }

                int id = Convert.ToInt32(grid.CurrentRow.Cells["UrunID"].Value);

                udal.Guncelle(id, txtUrunAdi.Text.Trim(), Convert.ToInt32(comboKategori.SelectedValue));
                Listele();
                txtUrunAdi.Clear();

                MessageBox.Show("Ürün güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.CurrentRow == null)
                {
                    MessageBox.Show("Silinecek ürün seç.");
                    return;
                }

                DialogResult cevap = MessageBox.Show(
                    "Seçili ürünü silmek istiyor musun?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (cevap == DialogResult.No)
                    return;

                int id = Convert.ToInt32(grid.CurrentRow.Cells["UrunID"].Value);

                udal.Sil(id);
                Listele();
                txtUrunAdi.Clear();

                MessageBox.Show("Ürün silindi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtUrunAdi.Text = grid.Rows[e.RowIndex].Cells["UrunAd"].Value.ToString();

                if (grid.Columns.Contains("KategoriAdi"))
                    comboKategori.Text = grid.Rows[e.RowIndex].Cells["KategoriAdi"].Value.ToString();
            }
        }
    }
}