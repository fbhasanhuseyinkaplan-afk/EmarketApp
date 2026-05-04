
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EmarketApp.DAL;
using System.Data;

namespace EmarketApp.Forms
{
    public partial class FrmAnaPanel : Form
    {
        Panel solPanel;
        Panel sagPanel;
        Panel ustPanel;
        Panel panelContainer;

        Button aktifMenu;
        Timer animTimer;
        int animStep = 0;

        public FrmAnaPanel()
        {
           
            TasarimOlustur();
        }

        void TasarimOlustur()
        {
            this.Text = "Emarket Yönetim Paneli";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            solPanel = new Panel();
            solPanel.Location = new Point(0, 0);
            solPanel.Size = new Size(230, this.ClientSize.Height);
            solPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            solPanel.BackColor = Color.FromArgb(10, 8, 18);
            this.Controls.Add(solPanel);

            sagPanel = new Panel();
            sagPanel.Location = new Point(230, 0);
            sagPanel.Size = new Size(this.ClientSize.Width - 230, this.ClientSize.Height);
            sagPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sagPanel.BackColor = Color.Black;
            this.Controls.Add(sagPanel);

            ustPanel = new Panel();
            ustPanel.Dock = DockStyle.Top;
            ustPanel.Height = 62;
            ustPanel.BackColor = Color.FromArgb(12, 10, 20);
            sagPanel.Controls.Add(ustPanel);

            Label baslik = new Label();
            baslik.Text = "Yönetim Paneli";
            baslik.ForeColor = Color.White;
            baslik.Font = new Font("Segoe UI", 17, FontStyle.Bold | FontStyle.Italic);
            baslik.Dock = DockStyle.Left;
            baslik.Width = 320;
            baslik.TextAlign = ContentAlignment.MiddleLeft;
            baslik.Padding = new Padding(22, 0, 0, 0);
            ustPanel.Controls.Add(baslik);

            Label kullanici = new Label();
            kullanici.Text = "● Yönetici";
            kullanici.ForeColor = Color.FromArgb(180, 140, 255);
            kullanici.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            kullanici.Dock = DockStyle.Right;
            kullanici.Width = 160;
            kullanici.TextAlign = ContentAlignment.MiddleCenter;
            ustPanel.Controls.Add(kullanici);

            panelContainer = new Panel();
            panelContainer.Location = new Point(0, 62);
            panelContainer.Size = new Size(sagPanel.ClientSize.Width, sagPanel.ClientSize.Height - 62);
            panelContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContainer.BackColor = Color.Black;
            panelContainer.Padding = new Padding(18);
            sagPanel.Controls.Add(panelContainer);

            sagPanel.Resize += delegate
            {
                panelContainer.Location = new Point(0, 62);
                panelContainer.Size = new Size(sagPanel.ClientSize.Width, sagPanel.ClientSize.Height - 62);
            };

            Label logo = new Label();
            logo.Text = "EMARKET";
            logo.ForeColor = Color.White;
            logo.Font = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic);
            logo.Height = 95;
            logo.Dock = DockStyle.Bottom;
            logo.TextAlign = ContentAlignment.MiddleCenter;
            solPanel.Controls.Add(logo);

            Button btnCikis = MenuButonu("⏻  Çıkış");
            Button btnRapor = MenuButonu("▣  Raporlar");
            Button btnStok = MenuButonu("▤  Stok");
            Button btnSatis = MenuButonu("₺  Satış");
            Button btnUrun = MenuButonu("◈  Ürün Yönetimi");
            Button btnDashboard = MenuButonu("⌂  Dashboard");

            solPanel.Controls.Add(btnCikis);
            solPanel.Controls.Add(btnRapor);
            solPanel.Controls.Add(btnStok);
            solPanel.Controls.Add(btnSatis);
            solPanel.Controls.Add(btnUrun);
            solPanel.Controls.Add(btnDashboard);

            btnDashboard.Click += delegate { MenuAktif(btnDashboard); DashboardOlustur(); };
            btnUrun.Click += delegate { MenuAktif(btnUrun); FormYukle(new FrmUrunYonetim()); };
            btnSatis.Click += delegate { MenuAktif(btnSatis); FormYukle(new FrmSatis()); };
            btnStok.Click += delegate { MenuAktif(btnStok); FormYukle(new FrmStok()); };
            btnRapor.Click += delegate { MenuAktif(btnRapor); FormYukle(new FrmRapor()); };
            btnCikis.Click += delegate { Application.Exit(); };

            MenuAktif(btnDashboard);
            DashboardOlustur();
        }

        Button MenuButonu(string text)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Dock = DockStyle.Top;
            btn.Height = 64;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(28, 12, 52);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold | FontStyle.Italic);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(24, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += delegate
            {
                if (aktifMenu != btn)
                {
                    btn.BackColor = Color.FromArgb(65, 28, 115);
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.White;
                }
            };

            btn.MouseLeave += delegate
            {
                if (aktifMenu != btn)
                {
                    btn.BackColor = Color.FromArgb(28, 12, 52);
                    btn.FlatAppearance.BorderSize = 0;
                }
            };

            return btn;
        }

        void MenuAktif(Button secilen)
        {
            foreach (Control c in solPanel.Controls)
            {
                if (c is Button)
                {
                    Button b = (Button)c;
                    b.BackColor = Color.FromArgb(28, 12, 52);
                    b.FlatAppearance.BorderSize = 0;
                }
            }

            aktifMenu = secilen;
            secilen.BackColor = Color.FromArgb(100, 44, 170);
            secilen.FlatAppearance.BorderSize = 2;
            secilen.FlatAppearance.BorderColor = Color.White;
        }

        void FormYukle(Form frm)
        {
            panelContainer.Controls.Clear();
            frm.BackColor = Color.Black;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(frm);
            frm.Show();
        }

        void DashboardOlustur()
        {
            panelContainer.Controls.Clear();

            DashboardPanel dash = new DashboardPanel();
            DashboardDAL ddal = new DashboardDAL();

            dash.ToplamCiro = ddal.ToplamCiro();
            dash.ToplamSiparis = ddal.ToplamSiparis();
            dash.ToplamUrun = ddal.ToplamUrun();
            dash.ToplamMusteri = ddal.ToplamMusteri();
            dash.SonSiparislerTable = ddal.SonSiparisler();
            dash.EnCokSatanTable = ddal.EnCokSatanUrunler();
            dash.ToplamSatisAdet = ddal.ToplamSatisDetayAdet();
            dash.KategoriOranTable = ddal.KategoriSatisOranlari();
            dash.GunlukSatisTable = ddal.Son7GunSatis();
            dash.Dock = DockStyle.Fill;
            dash.BackColor = Color.Black;
            panelContainer.Controls.Add(dash);

            animStep = 0;
            if (animTimer != null)
            {
                animTimer.Stop();
                animTimer.Dispose();
            }

            animTimer = new Timer();
            animTimer.Interval = 25;
            animTimer.Tick += delegate
            {
                animStep += 5;
                dash.AnimationValue = Math.Min(animStep, 100);
                dash.Invalidate();

                if (animStep >= 100)
                    animTimer.Stop();
            };
            animTimer.Start();
        }

        class DashboardPanel : Panel
        {
            public int AnimationValue = 100;
            public DataTable SonSiparislerTable;
            public DataTable EnCokSatanTable;
            public int ToplamSatisAdet = 0;
            public DataTable KategoriOranTable;
            public decimal ToplamCiro = 0;
            public int ToplamSiparis = 0;
            public int ToplamUrun = 0;
            public int ToplamMusteri = 0;
            public DataTable GunlukSatisTable;

            public DashboardPanel()
            {
                this.DoubleBuffered = true;
                this.ResizeRedraw = true;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                DrawBackground(g);
                DrawHeader(g);
                DrawCards(g);
                DrawChartPanel(g);
                DrawDonutPanel(g);
                DrawTablePanel(g);
                DrawBestProducts(g);
            }

            void DrawBackground(Graphics g)
            {
                using (LinearGradientBrush bg = new LinearGradientBrush(
                    this.ClientRectangle,
                    Color.FromArgb(5, 5, 12),
                    Color.FromArgb(30, 8, 52),
                    LinearGradientMode.ForwardDiagonal))
                {
                    g.FillRectangle(bg, this.ClientRectangle);
                }
            }

            void DrawHeader(Graphics g)
            {
                using (Font f1 = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic))
                using (Font f2 = new Font("Segoe UI", 10, FontStyle.Regular))
                using (Brush b1 = new SolidBrush(Color.White))
                using (Brush b2 = new SolidBrush(Color.FromArgb(170, 160, 190)))
                {
                    g.DrawString("Dashboard", f1, b1, 30, 25);
                    g.DrawString("Bugünkü genel satış ve stok özeti", f2, b2, 33, 62);
                }
            }

            void DrawCards(Graphics g)
            {
                int y = 105;
                int w = 225;
                int h = 100;
                int gap = 22;

                DrawCard(g, new Rectangle(30, y, w, h), "Toplam Ciro", "₺" + ToplamCiro.ToString("N0"), "● Canlı", Color.FromArgb(115, 45, 255));
                DrawCard(g, new Rectangle(30 + (w + gap), y, w, h), "Toplam Sipariş", ToplamSiparis.ToString(), "● Canlı", Color.FromArgb(95, 35, 180));
                DrawCard(g, new Rectangle(30 + 2 * (w + gap), y, w, h), "Toplam Ürün", ToplamUrun.ToString(), "● Canlı", Color.FromArgb(210, 50, 100));
                DrawCard(g, new Rectangle(30 + 3 * (w + gap), y, w, h), "Toplam Müşteri", ToplamMusteri.ToString(), "● Canlı", Color.FromArgb(120, 35, 190));
            }

            void DrawCard(Graphics g, Rectangle r, string title, string value, string rate, Color accent)
            {
                using (GraphicsPath path = RoundedRect(r, 14))
                using (LinearGradientBrush brush = new LinearGradientBrush(r, Color.FromArgb(32, 25, 48), accent, LinearGradientMode.ForwardDiagonal))
                using (Pen pen = new Pen(Color.FromArgb(90, 80, 120), 1))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }

                using (Font ft = new Font("Segoe UI", 10, FontStyle.Regular))
                using (Font fv = new Font("Segoe UI", 19, FontStyle.Bold))
                using (Font fr = new Font("Segoe UI", 9, FontStyle.Bold))
                using (Brush white = new SolidBrush(Color.White))
                using (Brush muted = new SolidBrush(Color.FromArgb(220, 210, 235)))
                using (Brush green = new SolidBrush(Color.FromArgb(55, 230, 150)))
                {
                    g.DrawString(title, ft, muted, r.X + 22, r.Y + 18);
                    g.DrawString(value, fv, white, r.X + 22, r.Y + 45);
                    g.DrawString("↑ " + rate, fr, green, r.X + r.Width - 75, r.Y + 62);
                }
            }

            void DrawChartPanel(Graphics g)
            {
                Rectangle r = new Rectangle(30, 235, 650, 330);
                DrawGlass(g, r);

                using (Font f = new Font("Segoe UI", 12, FontStyle.Bold))
                using (Brush b = new SolidBrush(Color.White))
                    g.DrawString("Satış Analizi", f, b, r.X + 20, r.Y + 18);

                if (GunlukSatisTable == null || GunlukSatisTable.Rows.Count == 0)
                {
                    g.DrawString("Satış verisi yok", new Font("Segoe UI", 10), Brushes.Gray, r.X + 50, r.Y + 150);
                    return;
                }

                int baseY = r.Y + 260;
                int x = r.X + 45;

                int barGenislik = 18;
                int bosluk = 35;
                int maxDeger = 1;

                foreach (DataRow row in GunlukSatisTable.Rows)
                {
                    int val = Convert.ToInt32(row["SatisAdet"]);
                    if (val > maxDeger) maxDeger = val;
                }
                for (int i = 0; i < GunlukSatisTable.Rows.Count; i++)
                {
                    int adet = Convert.ToInt32(GunlukSatisTable.Rows[i]["SatisAdet"]);
                    int maxHeight = 180;
                    int barH = (int)((adet / (float)maxDeger) * maxHeight); 

                    Rectangle bar = new Rectangle(
                        x + i * bosluk,
                        baseY - barH,
                        barGenislik,
                        barH
                    );

                    using (LinearGradientBrush br = new LinearGradientBrush(
                        bar,
                        Color.FromArgb(120, 40, 255),
                        Color.FromArgb(255, 80, 180),
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(br, bar);
                    }

                    
                    string gun = Convert.ToDateTime(GunlukSatisTable.Rows[i]["Gun"]).ToString("dd");
                    g.DrawString(gun, new Font("Segoe UI", 8), Brushes.White, x + i * bosluk, baseY + 5);
                }

                
            }

            void DrawDonutPanel(Graphics g)
            {
                Rectangle r = new Rectangle(710, 235, 410, 330);
                DrawGlass(g, r);

                using (Font f = new Font("Segoe UI", 12, FontStyle.Bold))
                using (Brush b = new SolidBrush(Color.White))
                    g.DrawString("Kategori Satış Oranları", f, b, r.X + 20, r.Y + 18);

                Rectangle pie = new Rectangle(r.X + 55, r.Y + 85, 145, 145);

                Color[] renkler = {
        Color.FromArgb(120, 55, 255),
        Color.FromArgb(255, 70, 140),
        Color.FromArgb(255, 140, 40)
    };

                if (KategoriOranTable == null || KategoriOranTable.Rows.Count == 0 || ToplamSatisAdet == 0)
                {
                    using (Font f2 = new Font("Segoe UI", 10, FontStyle.Bold))
                    using (Brush muted = new SolidBrush(Color.FromArgb(170, 160, 190)))
                    {
                        g.DrawString("Henüz kategori satış verisi yok.", f2, muted, r.X + 55, r.Y + 150);
                    }

                    return;
                }

                float startAngle = -90;

                for (int i = 0; i < KategoriOranTable.Rows.Count && i < 3; i++)
                {
                    int adet = Convert.ToInt32(KategoriOranTable.Rows[i]["ToplamAdet"]);
                    float oran = (float)adet / ToplamSatisAdet;
                    float sweep = 360f * oran * AnimationValue / 100f;

                    using (Pen p = new Pen(renkler[i], 24))
                    {
                        g.DrawArc(p, pie, startAngle, sweep);
                    }

                    startAngle += 360f * oran;
                }

                using (Font f1 = new Font("Segoe UI", 20, FontStyle.Bold))
                using (Font f2 = new Font("Segoe UI", 9))
                using (Brush white = new SolidBrush(Color.White))
                using (Brush muted = new SolidBrush(Color.FromArgb(170, 160, 190)))
                {
                    string toplamText = ToplamSatisAdet.ToString();
                    string adetText = "Adet";

                    SizeF toplamSize = g.MeasureString(toplamText, f1);
                    SizeF adetSize = g.MeasureString(adetText, f2);

                    float merkezX = pie.X + (pie.Width / 2f);
                    float merkezY = pie.Y + (pie.Height / 2f);

                    g.DrawString(
                        toplamText,
                        f1,
                        white,
                        merkezX - (toplamSize.Width / 2f),
                        merkezY - toplamSize.Height + 5
                    );

                    g.DrawString(
                        adetText,
                        f2,
                        muted,
                        merkezX - (adetSize.Width / 2f),
                        merkezY + 10
                    );

                    int textY = r.Y + 95;

                    for (int i = 0; i < KategoriOranTable.Rows.Count && i < 3; i++)
                    {
                        string kategori = KategoriOranTable.Rows[i]["KategoriAdi"].ToString();
                        int adet = Convert.ToInt32(KategoriOranTable.Rows[i]["ToplamAdet"]);
                        decimal yuzde = ToplamSatisAdet == 0 ? 0 : (adet * 100m / ToplamSatisAdet);

                        using (Brush renk = new SolidBrush(renkler[i]))
                        {
                            g.DrawString(
                                "● " + kategori + "  " + adet + " adet (%" + yuzde.ToString("N0") + ")",
                                f2,
                                renk,
                                r.X + 230,
                                textY
                            );
                        }

                        textY += 40;
                    }
                }
            }

            void DrawTablePanel(Graphics g)
            {
                Rectangle r = new Rectangle(30, 575, 710, 245);
                DrawGlass(g, r);

                using (Font title = new Font("Segoe UI", 12, FontStyle.Bold))
                using (Font row = new Font("Consolas", 10, FontStyle.Bold))
                using (Brush white = new SolidBrush(Color.White))
                using (Brush muted = new SolidBrush(Color.FromArgb(180, 170, 200)))
                {
                    g.DrawString("Son Siparişler", title, white, r.X + 20, r.Y + 18);

                    int y = r.Y + 65;
                    g.DrawString("Sipariş No", row, muted, r.X + 20, y);
                    g.DrawString("Müşteri", row, muted, r.X + 140, y);
                    g.DrawString("Tutar", row, muted, r.X + 300, y);
                    g.DrawString("Durum", row, muted, r.X + 440, y);
                    g.DrawString("Tarih", row, muted, r.X + 580, y);
                    y += 35;

                    if (SonSiparislerTable != null && SonSiparislerTable.Rows.Count > 0)
                    {
                        int satirY = y;

                        foreach (DataRow rowData in SonSiparislerTable.Rows)
                        {
                            string no = "#" + rowData["SatisID"].ToString();
                            string musteri = rowData["Musteri"].ToString();
                            string tutar = "₺" + Convert.ToDecimal(rowData["ToplamTutar"]).ToString("N0");
                            string durum = "Tamamlandı";
                            string tarih = Convert.ToDateTime(rowData["Tarih"]).ToString("dd.MM.yyyy");

                            DrawOrderRow(g, r.X + 20, satirY, no, musteri, tutar, durum, tarih);

                            satirY += 35;
                        }
                    }
                    else
                    {
                        g.DrawString("Henüz satış kaydı yok.", row, white, r.X + 20, y);
                    }
                }
            }

            void DrawOrderRow(Graphics g, int x, int y, string no, string musteri, string tutar, string durum, string tarih)
            {
                using (Font f = new Font("Consolas", 10, FontStyle.Bold))
                using (Brush white = new SolidBrush(Color.White))
                using (Brush blue = new SolidBrush(Color.FromArgb(120, 190, 255)))
                using (Brush green = new SolidBrush(Color.FromArgb(80, 230, 150)))
                {
                    g.DrawString(no, f, white, x, y);
                    g.DrawString(musteri, f, white, x + 120, y);
                    g.DrawString(tutar, f, white, x + 280, y);
                    g.DrawString(durum, f, green, x + 420, y);
                    g.DrawString(tarih, f, blue, x + 560, y);
                }
            }

            void DrawBestProducts(Graphics g)
            {
                Rectangle r = new Rectangle(770, 575, 260, 245);
                DrawGlass(g, r);

                using (Font title = new Font("Segoe UI", 12, FontStyle.Bold))
                using (Font item = new Font("Segoe UI", 10, FontStyle.Bold))
                using (Brush white = new SolidBrush(Color.White))
                using (Brush muted = new SolidBrush(Color.FromArgb(180, 170, 200)))
                {
                    g.DrawString("En Çok Satan Ürünler", title, white, r.X + 20, r.Y + 18);

                    if (EnCokSatanTable != null && EnCokSatanTable.Rows.Count > 0)
                    {
                        int y = r.Y + 70;

                        foreach (DataRow rowData in EnCokSatanTable.Rows)
                        {
                            string urunAd = rowData["UrunAd"].ToString();
                            string adet = rowData["ToplamAdet"].ToString() + " adet";

                            g.DrawString(urunAd, item, white, r.X + 25, y);
                            g.DrawString(adet, item, muted, r.X + 150, y);

                            y += 45;
                        }
                    }
                    else
                    {
                        g.DrawString("Satış verisi yok.", item, muted, r.X + 25, r.Y + 75);
                    }
                }
            }

            void DrawGlass(Graphics g, Rectangle r)
            {
                using (GraphicsPath path = RoundedRect(r, 14))
                using (SolidBrush br = new SolidBrush(Color.FromArgb(32, 26, 46)))
                using (Pen pen = new Pen(Color.FromArgb(70, 60, 95), 1))
                {
                    g.FillPath(br, path);
                    g.DrawPath(pen, path);
                }
            }

            GraphicsPath RoundedRect(Rectangle bounds, int radius)
            {
                int d = radius * 2;
                GraphicsPath path = new GraphicsPath();
                path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
                path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
                path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }
        }
    }
}
