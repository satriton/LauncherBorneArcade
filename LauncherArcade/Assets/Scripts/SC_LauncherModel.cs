using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

public class SC_LauncherModel
{
    private string pathToGames;

    public SC_LauncherModel(string pathToGames)
    {
        this.pathToGames = pathToGames;
    }

    // Charge le contenue de "news.txt" du dossier Games/ dans la zone de news
    public string GetNews()
    {
        var pathNewsTxt = pathToGames + "news.txt";
        var strBuilder = new StringBuilder();


        if (File.Exists(pathNewsTxt))
        {
            StreamReader reader = new StreamReader(pathNewsTxt);

            while (!reader.EndOfStream)
            {
                strBuilder.Append(reader.ReadLine() + "\n");
            }

            reader.Close();
        }
        else
        {
            strBuilder.Append("Pas de news");
        }

        return strBuilder.ToString();
    }


    // Get all games in Games/ directory and fill games with all the info
    public List<Game> GetGamesList()
    {
        var dirInfo = new DirectoryInfo(pathToGames);
        var directoryInfo = dirInfo.GetDirectories();

        List<Game> games = new List<Game>();

        foreach (var directory in directoryInfo)
        {
            string pathToGameDir = pathToGames + directory.Name;

            var files = System.IO.Directory.GetFiles(pathToGameDir, "*.exe");

            // TODO check si c'est le bon .exe
            string pathToExe = files[0];

            UnityEngine.Debug.Log(files[0].ToString());
            string name = files[0].Split('\\')[1].Split('.')[0];

            var pathToGameMeta = pathToGameDir + "/GameMeta";
            // Create the directory only if it don't existe 
            System.IO.Directory.CreateDirectory(pathToGameMeta);
            
            games.Add(new Game(pathToGameDir, pathToExe, pathToGameMeta, name));
        }

        return games;
    }
}
