using System;
using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class UrunDAL
    {
        Db db = new Db();

        public DataTable Liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_UrunListe", db.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void Ekle(string urunAd, int kategoriID)
        {
            SqlCommand cmd = new SqlCommand("sp_Urun_Insert", db.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UrunAd", urunAd);
            cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
            cmd.Parameters.AddWithValue("@KampanyaID", DBNull.Value);
            cmd.Parameters.AddWithValue("@AktifMi", 1);
            db.conn.Open();
            cmd.ExecuteNonQuery();
            db.conn.Close();
        }

        public void Guncelle(int urunID, string urunAd, int kategoriID)
        {
            SqlCommand cmd = new SqlCommand("sp_Urun_Update", db.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UrunID", urunID);
            cmd.Parameters.AddWithValue("@UrunAd", urunAd);
            cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
            cmd.Parameters.AddWithValue("@KampanyaID", DBNull.Value);
            cmd.Parameters.AddWithValue("@AktifMi", 1);
            db.conn.Open();
            cmd.ExecuteNonQuery();
            db.conn.Close();
        }

        public void Sil(int urunID)
        {
            SqlCommand cmd = new SqlCommand("sp_Urun_Delete", db.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UrunID", urunID);
            db.conn.Open();
            cmd.ExecuteNonQuery();
            db.conn.Close();
        }

        public decimal SonFiyatGetir(int urunID)
        {
            using (SqlConnection conn = new Db().conn)
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 Fiyat FROM FiyatTb WHERE UrunID=@id ORDER BY BaslangicTarih DESC", conn);
                cmd.Parameters.AddWithValue("@id", urunID);
                conn.Open();
                object sonuc = cmd.ExecuteScalar();

                if (sonuc == null || sonuc == DBNull.Value)
                    return 0;

                return Convert.ToDecimal(sonuc);
            }
        }
        public decimal FiyatGetir(int urunId)
        {
            SqlCommand cmd = new SqlCommand("SELECT Fiyat FROM UrunTb WHERE UrunID=@id", db.conn);
            cmd.Parameters.AddWithValue("@id", urunId);

            db.conn.Open();
            object val = cmd.ExecuteScalar();
            db.conn.Close();

            return Convert.ToDecimal(val);
        }
    }

}
