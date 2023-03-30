using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class VideogameManager
{
    public string ConnStr { get; set; }

    public VideogameManager(string connStr)
    {
        ConnStr = connStr;
    }

    public Videogame SearchGame(long idq)
    {
        Videogame videogame = null;
        using SqlConnection conn = new SqlConnection(ConnStr);
        try
        {
            conn.Open();
            var query = "SELECT * " +
                "FROM videogames " +
                $"WHERE id = @Id";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", idq);
            
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var idIdx = reader.GetOrdinal("id");
                var id = reader.GetInt64(idIdx);

                var nameIdx = reader.GetOrdinal("name");
                var name = reader.GetString(nameIdx);

                var dateIdx = reader.GetOrdinal("release_date");
                var date = reader.GetDateTime(dateIdx);

                var houseIdx = reader.GetOrdinal("software_house_id");
                var house = reader.GetInt64(houseIdx);

                videogame = new Videogame(id, name, date, house);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return videogame;
    }
}
