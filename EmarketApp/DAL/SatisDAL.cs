using System;
using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class SatisDAL
    {
        Db db = new Db();

        public int SatisEkle(int musteriID, int personelID, decimal toplamTutar)
        {
            SqlCommand cmd = new SqlCommand("sp_Satis_Insert", db.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MusteriID", musteriID);
            cmd.Parameters.AddWithValue("@PersonelID", personelID);
            cmd.Parameters.AddWithValue("@ToplamTutar", toplamTutar);
            db.conn.Open();
            int satisID = Convert.ToInt32(cmd.ExecuteScalar());
            db.conn.Close();
            return satisID;
        }

        public void SatisDetayEkle(int satisID, int urunID, int adet, decimal birimFiyat)
        {
            SqlCommand cmd = new SqlCommand("sp_SatisDetay_Insert", db.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SatisID", satisID);
            cmd.Parameters.AddWithValue("@UrunID", urunID);
            cmd.Parameters.AddWithValue("@Adet", adet);
            cmd.Parameters.AddWithValue("@BirimFiyat", birimFiyat);
            db.conn.Open();
            cmd.ExecuteNonQuery();
            db.conn.Close();
        }

        public DataTable SatisListe()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_SatisListe", db.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
