using System;
using System.Collections;
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

    //ADD GAME
    public void AddGame(Videogame vg)
    {
        using SqlConnection conn = new SqlConnection(ConnStr);
        try
        {
            conn.Open();

            using SqlTransaction tran = conn.BeginTransaction("");

            try
            {
                var query = "INSERT INTO videogames (name, overview, software_house_id, release_date) " +
                    "VALUES (@Name, @Overview, @SoftHouse, @Date);";

                using SqlCommand cmd = new SqlCommand(query, conn, tran);
                cmd.Parameters.AddWithValue("@Name", vg.Name);
                cmd.Parameters.AddWithValue("@Overview", vg.Overview);
                cmd.Parameters.AddWithValue("@SoftHouse", vg.SoftwareHouse);
                cmd.Parameters.AddWithValue("@Date", vg.ReleaseDate);

                cmd.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("Il videogioco é stato aggiunto correttamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tran.Rollback();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //DELETE GAME
    public void DeleteGame(long id)
    {
        using var conn = new SqlConnection(ConnStr);
        conn.Open();
        string query = "DELETE FROM videogames " +
            "WHERE id=@Id;";

        var cmd = new SqlCommand(query, conn);
        cmd.Parameters.Add(new SqlParameter("@Id", id));

        int affectedRows = cmd.ExecuteNonQuery();
        Console.WriteLine($"Record eliminati: {affectedRows}.");
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

                var overIdx = reader.GetOrdinal("overview");
                var over = reader.GetString(nameIdx);


                var dateIdx = reader.GetOrdinal("release_date");
                var date = reader.GetDateTime(dateIdx);

                var houseIdx = reader.GetOrdinal("software_house_id");
                var house = reader.GetInt64(houseIdx);

                videogame = new Videogame(id, name, over, date, house);
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
