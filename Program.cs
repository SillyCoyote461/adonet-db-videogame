using System.Data.SqlClient;

var nl = Environment.NewLine;
var line = $"-----------------------------";
bool execute = true;
string connStr = "Data Source=localhost;Initial Catalog=db_videogame;Integrated Security=True";
VideogameManager Manager = new VideogameManager(connStr);

while (execute)
{
    Console.WriteLine(
        $"Lista comandi: {nl}" +
        $"FILTER -> Ricerca giochi per nome.{nl}" +
        $"SEARCH -> Cerca gioco per ID. {nl}" +
        $"ADD ->  Aggiungi gioco alla lista.{nl}" +
        $"DELETE -> Elimina gioco dalla lista.{nl}" +
        $"EXIT -> Chiudi il programma.");

    //INPUT
    Console.Write($"Digita il comando -> ");
    string cmd = Console.ReadLine() ?? "";

    //NO SPACES ALL LOWER
    cmd = cmd.Replace( " ", "" );
    cmd = cmd.ToLower();

    switch(cmd)
    {
        //FILTER GAMES
        case "filter":
            Console.Write(
            $"{line}{nl}" +
            $"Inserisci nome gioco: ");
            cmd = Console.ReadLine() ?? "";

            var videogameList = Manager.FilterGame(cmd);
            foreach (var v in videogameList)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine(line);
            break;

        //SEARCH GAME
        case "search":
            Console.Write(
                $"{line}{nl}" +
                $"Inserisci ID gioco: ");
            cmd = Console.ReadLine() ?? "";

            var videogame = Manager.SearchGame(cmd);
            Console.WriteLine($"{videogame}{nl}{line}");
            break;

        //ADD GAME
        case "add":
            Console.Write(
            $"{line}{nl}" +
            $"Inserisci nome gioco: ");
            string name = Console.ReadLine() ?? "";
            Console.Write($"Inserisci descrizione gioco: ");
            string overview = Console.ReadLine() ?? "";
            Console.Write("Inserisci Id della software house: ");
            long softHouse = Convert.ToInt64(Console.ReadLine());
            var date = DateTime.Now;
            Videogame newVg = new Videogame(null, name, overview, date, softHouse);
            Manager.AddGame(newVg);
            Console.WriteLine(line);
            break;

        //DELETE GAME
        case "delete":
            Console.Write(
            $"{line}{nl}" +
            $"Inserisci nome gioco: ");
            long id = Convert.ToInt64(Console.ReadLine());
            Manager.DeleteGame(id);
            Console.WriteLine(line);    
            break;

        //CLOSE PROGRAM
        case "exit":
            execute = false;
            break;

        default: 
            Console.WriteLine(
                $"{line}{nl}" +
                $"Comando '{cmd}' non riconosciuto. {nl}" +
                $"{line}{nl}");
            break;
    }   

}