using System;
using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class PersonelDAL
    {
        Db db = new Db();

        public DataTable Liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_PersonelListe", db.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public bool GirisDogrula(string kullanici, string sifre)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM PersonelTb WHERE Email=@k AND Telefon=@s", db.conn);
            cmd.Parameters.AddWithValue("@k", kullanici.Trim());
            cmd.Parameters.AddWithValue("@s", sifre.Trim());

            db.conn.Open();
            int sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            db.conn.Close();

            return sonuc > 0;
        }

    }
}
