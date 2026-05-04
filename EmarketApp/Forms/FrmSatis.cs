using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EmarketApp.DAL;

namespace EmarketApp.Forms
{
    public class FrmSatis : Form
    {
        ComboBox comboUrun, comboMusteri;
        NumericUpDown numAdet;
        Button btnSepeteEkle, btnSatisYap;
        DataGridView gridSepet;

        UrunDAL udal = new UrunDAL();
        MusteriDAL mdal = new MusteriDAL();
        SatisDAL sdal = new SatisDAL();
        Label lblToplam;

        decimal genelToplam = 0;


        

        public FrmSatis()
        {
            TasarimOlustur();
            this.Load += FrmSatis_Load;


        }

        void TasarimOlustur()
        {
            this.BackColor = Color.Black;

            Label baslik = new Label();
            baslik.Text = "Satış Ekranı";
            baslik.ForeColor = Color.White;
            baslik.Font = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic);
            baslik.SetBounds(30, 25, 300, 40);
            this.Controls.Add(baslik);

            Panel kart = new Panel();
            kart.SetBounds(30, 90, 900, 150);
            kart.BackColor = Color.FromArgb(32, 26, 46);
            this.Controls.Add(kart);

            comboUrun = ComboOlustur(30, 30);
            comboMusteri = ComboOlustur(250, 30);

            numAdet = new NumericUpDown();
            numAdet.SetBounds(470, 30, 100, 28);
            numAdet.Minimum = 1;
            numAdet.Maximum = 100;
            numAdet.BackColor = Color.FromArgb(18, 12, 30);
            numAdet.ForeColor = Color.White;
            kart.Controls.Add(numAdet);

            btnSepeteEkle = Buton("Sepete Ekle", 600, 25);
            btnSatisYap = Buton("Satışı Tamamla", 750, 25);

            kart.Controls.Add(comboUrun);
            kart.Controls.Add(comboMusteri);
            kart.Controls.Add(btnSepeteEkle);
            kart.Controls.Add(btnSatisYap);

            btnSepeteEkle.Click += BtnSepeteEkle_Click;
            btnSatisYap.Click += BtnSatisYap_Click;

            gridSepet = new DataGridView();
            gridSepet.SetBounds(30, 270, 900, 400);
            gridSepet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSepet.BackgroundColor = Color.FromArgb(20, 16, 32);
            gridSepet.ForeColor = Color.White;
            gridSepet.EnableHeadersVisualStyles = false;

            gridSepet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 20, 75);
            gridSepet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            gridSepet.Columns.Add("Urun", "Ürün");
            gridSepet.Columns.Add("Adet", "Adet");
            gridSepet.Columns.Add("Fiyat", "Fiyat");
            gridSepet.Columns.Add("Toplam", "Toplam");

            gridSepet.DefaultCellStyle.BackColor = Color.FromArgb(30, 24, 45);
            gridSepet.DefaultCellStyle.ForeColor = Color.White;
            gridSepet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 44, 170);
            gridSepet.DefaultCellStyle.SelectionForeColor = Color.White;

            gridSepet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(24, 18, 38);
            gridSepet.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            gridSepet.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 24, 45);
            gridSepet.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            gridSepet.RowsDefaultCellStyle.BackColor = Color.FromArgb(30, 24, 45);
            gridSepet.RowsDefaultCellStyle.ForeColor = Color.White;

            gridSepet.GridColor = Color.FromArgb(70, 60, 95);
            gridSepet.BorderStyle = BorderStyle.None;
            gridSepet.RowTemplate.Height = 32;
            gridSepet.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            this.Controls.Add(gridSepet);
            gridSepet.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hit = gridSepet.HitTest(e.X, e.Y);

                    if (hit.RowIndex >= 0)
                    {
                        gridSepet.ClearSelection();
                        gridSepet.Rows[hit.RowIndex].Selected = true;

                        ContextMenu cm = new ContextMenu();
                        cm.MenuItems.Add("Sil", (sender, ev) =>
                        {
                            decimal satirToplam = Convert.ToDecimal(gridSepet.Rows[hit.RowIndex].Cells["Toplam"].Value);
                            genelToplam -= satirToplam;

                            gridSepet.Rows.RemoveAt(hit.RowIndex);

                            lblToplam.Text = "Toplam: ₺" + genelToplam;
                        });

                        cm.Show(gridSepet, new Point(e.X, e.Y));
                    }
                }
            };



            lblToplam = new Label();
            lblToplam.ForeColor = Color.Lime;
            lblToplam.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblToplam.Text = "Toplam: ₺0";
            lblToplam.SetBounds(700, 580, 250, 40);
            this.Controls.Add(lblToplam);


        }



        ComboBox ComboOlustur(int x, int y)
        {
            ComboBox combo = new ComboBox();
            combo.SetBounds(x, y, 200, 28);
            combo.BackColor = Color.FromArgb(18, 12, 30);
            combo.ForeColor = Color.White;
            combo.FlatStyle = FlatStyle.Flat;
            return combo;
        }

        Button Buton(string text, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.SetBounds(x, y, 130, 38);
            btn.BackColor = Color.FromArgb(100, 44, 170);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            return btn;
        }

        private void FrmSatis_Load(object sender, EventArgs e)
        {
            comboUrun.DataSource = udal.Liste();
            comboUrun.DisplayMember = "UrunAd";
            comboUrun.ValueMember = "UrunID";

            comboMusteri.DataSource = mdal.Liste();
            comboMusteri.DisplayMember = "Musteri";
            comboMusteri.ValueMember = "MusteriID";
        }

        private void BtnSepeteEkle_Click(object sender, EventArgs e)
        {
            decimal fiyat = 100; 
            int adet = (int)numAdet.Value;
            decimal toplam = fiyat * adet;

            gridSepet.Rows.Add(comboUrun.Text, adet, fiyat, toplam);

            genelToplam += toplam;
            lblToplam.Text = "Toplam: ₺" + genelToplam;
        }

        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            if (gridSepet.Rows.Count == 0)
            {
                MessageBox.Show("Sepet boş.");
                return;
            }

            MessageBox.Show("Satış tamamlandı: ₺" + genelToplam);

            gridSepet.Rows.Clear();
            genelToplam = 0;

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmSatis
            // 
            this.ClientSize = new System.Drawing.Size(377, 322);
            this.Name = "FrmSatis";
            this.ResumeLayout(false);

        }


    }
}