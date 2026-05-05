using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class StokDAL
    {
        public DataTable Liste()
        {
            using (SqlConnection conn = new Db().conn)
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_StokListe", conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
