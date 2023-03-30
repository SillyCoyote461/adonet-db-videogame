using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


public record class Videogame
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Overview { get; set; }
    public DateTime ReleaseDate { get; set; }
    public long SoftwareHouse { get; set; }

    public Videogame(long? id, string name, string overview, DateTime releaseDate, long softwareHouse)
    {
        Id = id;
        Name = name;
        Overview = overview;
        ReleaseDate = releaseDate;
        SoftwareHouse = softwareHouse;
    }
}

