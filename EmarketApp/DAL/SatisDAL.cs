using System;
using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class SatisDAL
    {
        public int SatisEkle(int musteriID, int personelID, decimal toplamTutar)
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlCommand cmd = new SqlCommand("sp_Satis_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MusteriID", musteriID);
                cmd.Parameters.AddWithValue("@PersonelID", personelID);
                cmd.Parameters.AddWithValue("@ToplamTutar", toplamTutar);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void SatisDetayEkle(int satisID, int urunID, int adet, decimal birimFiyat)
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlCommand cmd = new SqlCommand("sp_SatisDetay_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SatisID", satisID);
                cmd.Parameters.AddWithValue("@UrunID", urunID);
                cmd.Parameters.AddWithValue("@Adet", adet);
                cmd.Parameters.AddWithValue("@BirimFiyat", birimFiyat);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable SatisListe()
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_SatisListe", conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
