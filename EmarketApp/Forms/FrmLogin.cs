using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EmarketApp.Service;

namespace EmarketApp.Forms
{
    public class FrmLogin : Form
    {
        TextBox txtKullanici;
        TextBox txtSifre;
        Button btnGiris;

        public FrmLogin()
        {
            TasarimOlustur();
        }

        void TasarimOlustur()
        {
            this.Text = "Emarket Giriş";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(460, 360);
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Panel kart = new Panel();
            kart.SetBounds(45, 35, 360, 260);
            kart.BackColor = Color.FromArgb(28, 12, 52);
            kart.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(120, 55, 255), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, kart.Width - 1, kart.Height - 1);
                }
            };
            this.Controls.Add(kart);

            Label baslik = new Label();
            baslik.Text = "EMARKET";
            baslik.ForeColor = Color.White;
            baslik.Font = new Font("Segoe UI", 24, FontStyle.Bold | FontStyle.Italic);
            baslik.TextAlign = ContentAlignment.MiddleCenter;
            baslik.SetBounds(0, 25, 360, 40);
            kart.Controls.Add(baslik);

            Label altBaslik = new Label();
            altBaslik.Text = "Yönetim Paneli Girişi";
            altBaslik.ForeColor = Color.FromArgb(180, 160, 220);
            altBaslik.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            altBaslik.TextAlign = ContentAlignment.MiddleCenter;
            altBaslik.SetBounds(0, 65, 360, 25);
            kart.Controls.Add(altBaslik);

            txtKullanici = TextBoxOlustur(80, 105, "admin");
            kart.Controls.Add(txtKullanici);

            txtSifre = TextBoxOlustur(80, 145, "1234");
            txtSifre.UseSystemPasswordChar = true;
            kart.Controls.Add(txtSifre);

            btnGiris = new Button();
            btnGiris.Text = "Giriş Yap";
            btnGiris.SetBounds(80, 195, 200, 38);
            btnGiris.BackColor = Color.FromArgb(100, 44, 170);
            btnGiris.ForeColor = Color.White;
            btnGiris.FlatStyle = FlatStyle.Flat;
            btnGiris.FlatAppearance.BorderColor = Color.White;
            btnGiris.Font = new Font("Segoe UI", 10, FontStyle.Bold | FontStyle.Italic);
            btnGiris.Cursor = Cursors.Hand;

            btnGiris.MouseEnter += delegate
            {
                btnGiris.BackColor = Color.FromArgb(140, 70, 220);
                btnGiris.FlatAppearance.BorderSize = 2;
            };

            btnGiris.MouseLeave += delegate
            {
                btnGiris.BackColor = Color.FromArgb(100, 44, 170);
                btnGiris.FlatAppearance.BorderSize = 1;
            };

            btnGiris.Click += btnGiris_Click;
            kart.Controls.Add(btnGiris);
        }

        TextBox TextBoxOlustur(int x, int y, string text)
        {
            TextBox txt = new TextBox();
            txt.SetBounds(x, y, 200, 28);
            txt.Text = text;
            txt.BackColor = Color.FromArgb(18, 12, 30);
            txt.ForeColor = Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return txt;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKullanici.Text.Trim() == "" || txtSifre.Text.Trim() == "")
                {
                    MessageBox.Show("Kullanıcı adı ve şifre alanları boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AuthService authService = new AuthService();
                bool girisBasarili = authService.GirisBasariliMi(txtKullanici.Text.Trim(), txtSifre.Text.Trim());

                if (girisBasarili)
                {
                    FrmAnaPanel frm = new FrmAnaPanel();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Giriş başarısız. Kullanıcı adı veya şifre hatalı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
