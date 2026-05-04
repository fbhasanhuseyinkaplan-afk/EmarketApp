using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class MusteriDAL
    {
        Db db = new Db();

        public DataTable Liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_MusteriListe", db.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
