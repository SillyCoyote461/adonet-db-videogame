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


    //SEARCH BY ID
    public Videogame SearchGame(string idq)
    {
        string query = "SELECT * " +
                "FROM videogames " +
                "WHERE id = @Id";

        List<Videogame> videogame = ReaderSingleParam(query, "Id", idq);
        return videogame[0];
    }

    //FILTER BY NAME
    public List<Videogame> FilterGame(string name)
    {
        string query = "SELECT * " +
            "FROM videogames " +
            $"WHERE name " +
            $"LIKE @Name";
        name = "%" + name + "%";
        var vgList = ReaderSingleParam(query, "Name", name);
        return vgList;
    }

    //query construct
    private List<Videogame> ReaderSingleParam(string query, string param, string value)
    {
        Videogame videogame = null;
        using SqlConnection conn = new SqlConnection(ConnStr);
        List<Videogame> vgList = new List<Videogame>();

        try
        {
            conn.Open();

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue($"@{param}", $"{value}");

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
                vgList.Add(videogame);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return vgList;
    }
}
