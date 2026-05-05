using System;
using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class UrunDAL
    {
        public DataTable Liste()
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_UrunListe", conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Ekle(string urunAd, int kategoriID)
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlCommand cmd = new SqlCommand("sp_Urun_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UrunAd", urunAd);
                cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                cmd.Parameters.AddWithValue("@KampanyaID", DBNull.Value);
                cmd.Parameters.AddWithValue("@AktifMi", 1);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Guncelle(int urunID, string urunAd, int kategoriID)
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlCommand cmd = new SqlCommand("sp_Urun_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UrunID", urunID);
                cmd.Parameters.AddWithValue("@UrunAd", urunAd);
                cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                cmd.Parameters.AddWithValue("@KampanyaID", DBNull.Value);
                cmd.Parameters.AddWithValue("@AktifMi", 1);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Sil(int urunID)
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlCommand cmd = new SqlCommand("sp_Urun_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UrunID", urunID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public decimal SonFiyatGetir(int urunID)
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Fiyat FROM FiyatTb WHERE UrunID=@id ORDER BY BaslangicTarih DESC", conn))
            {
                cmd.Parameters.AddWithValue("@id", urunID);
                conn.Open();
                object sonuc = cmd.ExecuteScalar();
                return sonuc == null || sonuc == DBNull.Value ? 0 : Convert.ToDecimal(sonuc);
            }
        }
    }
}
