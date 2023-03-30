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

            break;

        //SEARCH GAME
        case "search":
            var videogame = Manager.SearchGame(1);
            Console.WriteLine(videogame);
            break;

        //ADD GAME
        case "add":

            break;

        //DELETE GAME
        case "delete":

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