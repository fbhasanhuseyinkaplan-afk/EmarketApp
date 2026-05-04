using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EmarketApp.DAL;

namespace EmarketApp.DAL
{
    public class DashboardDAL
    {
        Db db = new Db();

        public decimal ToplamCiro()
        {
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(ToplamTutar),0) FROM SatisTb", db.conn);
            db.conn.Open();
            decimal sonuc = Convert.ToDecimal(cmd.ExecuteScalar());
            db.conn.Close();
            return sonuc;
        }

        public int ToplamSiparis()
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SatisTb", db.conn);
            db.conn.Open();
            int sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            db.conn.Close();
            return sonuc;
        }

        public int ToplamUrun()
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UrunlerTb WHERE AktifMi = 1", db.conn);
            db.conn.Open();
            int sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            db.conn.Close();
            return sonuc;
        }

        public int ToplamMusteri()
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MusterilerTb", db.conn);
            db.conn.Open();
            int sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            db.conn.Close();
            return sonuc;
        }

        public DataTable SonSiparisler()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT TOP 3
            SatisID,
            Musteri,
            ToplamTutar,
            Tarih
        FROM vw_SatisListe
        ORDER BY Tarih DESC", db.conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable EnCokSatanUrunler()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT TOP 3
            u.UrunAd,
            SUM(sd.Adet) AS ToplamAdet
        FROM SatisDetayTb sd
        INNER JOIN UrunlerTb u ON sd.UrunID = u.UrunID
        GROUP BY u.UrunAd
        ORDER BY SUM(sd.Adet) DESC", db.conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int ToplamSatisDetayAdet()
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(SUM(Adet),0) FROM SatisDetayTb", db.conn);

            db.conn.Open();
            int sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            db.conn.Close();

            return sonuc;
        }

        public DataTable KategoriSatisOranlari()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT TOP 3
            k.KategoriAdi,
            SUM(sd.Adet) AS ToplamAdet
        FROM SatisDetayTb sd
        INNER JOIN UrunlerTb u ON sd.UrunID = u.UrunID
        INNER JOIN KategoriTb k ON u.KategoriID = k.KategoriID
        GROUP BY k.KategoriAdi
        ORDER BY SUM(sd.Adet) DESC", db.conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable Son7GunSatis()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT 
            CAST(Tarih AS DATE) AS Gun,
            COUNT(*) AS SatisAdet
        FROM SatisTb
        WHERE Tarih >= DATEADD(DAY, -7, GETDATE())
        GROUP BY CAST(Tarih AS DATE)
        ORDER BY Gun", db.conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}