using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SC_Launcher : MonoBehaviour
{
    // TODO parcourir Games/ à la recherche de tous les jeux, les stocke dans un tableau de string,
    // Charge toutes les données qu'il a récupérer dans l'UI et les bouttons

    public GameObject gameBtnPrefab;
    [Range(1, 4)] public int nbGameByPage;
    public static string pathToGames;
    public TextMeshProUGUI newsZone;


    private DirectoryInfo dirInfo;
    private Process process;


    private struct Game
    {
        public string pathToGameDir { get; set; }
        public string pathToExe { get; set; }
        public string pathToGameMeta { get; set; }
        public string name;
    }
    private Game[] games;

    private GameObject[] gamesBtn;
    private int currentGameIndex;



    void Start()
    {
        pathToGames = Application.dataPath + "/../Games/";
        currentGameIndex = 0;


        // Instanciate gamesBtn
        gamesBtn = new GameObject[nbGameByPage];
        for (int i = 0; i < nbGameByPage; i++)
        {
            GameObject gameBtn;
            gameBtn = Instantiate(gameBtnPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            gameBtn.transform.SetParent(transform);
            gameBtn.GetComponent<RectTransform>().localScale = Vector3.one;

            gamesBtn[i] = gameBtn;
        }

        // Charge les jeux trouver dans games
        updateGameList();

        // Charge les nbGameByPage
        loadGameBtnAssets(0);

        //loadNews();

    }


    public void LaunchchGame(string pathToExe)
    {
        //newsZone.text = pathToExe;
        //TODO Not working ----------------------------------------------------------------------
        process = Process.Start(pathToExe);
    }


    public void changerPage(bool goRight)
    {
        if (goRight && currentGameIndex + nbGameByPage < games.Length)
        {
            loadGameBtnAssets(currentGameIndex + nbGameByPage);
        }
        else if (!goRight && currentGameIndex - nbGameByPage >= 0)
        {
            loadGameBtnAssets(currentGameIndex - nbGameByPage);
        }
    }


    // Charge le contenue de "news.txt" du dossier Games/ dans la zone de news
    private void loadNews()
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

        newsZone.text = strBuilder.ToString();
    }


    // Load assets, métadata (and description ?) on the gamesBtn for games[index] à games[index + nbGameByPage]
    private void loadGameBtnAssets(int index)
    {
        currentGameIndex = index;

        for (int i = 0; i < nbGameByPage; i++)
        {
            var gameBtnImage = gamesBtn[i].GetComponent<Image>();
            var scGameBtn = gamesBtn[i].GetComponent<SC_BtnStartGame>();
            scGameBtn.pathToExe = games[i + index].pathToExe;

            if (i + index <= games.Length)
            {
                float PixelsPerUnit = 100.0f;
                string pathToGameLogo = games[i + index].pathToGameMeta + "/logo.png";

                Texture2D SpriteTexture;
                Sprite gameImage;
                if (File.Exists(pathToGameLogo))
                {
                    SpriteTexture = LoadTexture(pathToGameLogo);
                    gameImage = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
                }
                else
                {
                    gameImage = Resources.Load<Sprite>("logo");
                }

                gameBtnImage.sprite = gameImage;
            }
            else
            {
                gameBtnImage.sprite = Resources.Load<Sprite>("empty");
            }

            gameBtnImage.color = Color.white;
        }
    }


    // Load a PNG or JPG file from disk to a Texture2D
    // Returns null if load fails
    public Texture2D LoadTexture(string filePath)
    {
        Texture2D tex2D;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (tex2D.LoadImage(fileData))           // Load the imagedata into the texture (size is set automatically)
                return tex2D;                 // If data = readable -> return texture
        }
        return null;
    }


    // Get all games in Games/ directory and fill games with all the info
    private void updateGameList()
    {
        dirInfo = new DirectoryInfo(pathToGames);
        var directoryInfo = dirInfo.GetDirectories();

        games = new Game[directoryInfo.Length];
        var j = 0;
        foreach (var directory in directoryInfo)
        {
            string pathToGame = pathToGames + directory.Name;
            games[j].pathToGameDir = pathToGame;

            var files = System.IO.Directory.GetFiles(pathToGame, "*.exe");

            // On regarde que ce ne soit pas l'handlerUnity
            // if (files[0].Contains("UnityCrashHandler"))
            // {
            //     games[j].pathToExe = files[1];
            //     games[j].name = files[1].Split('.')[0];
            // }
            // else
            // {
            //     games[j].pathToExe = files[0];
            //     games[j].name = files[0].Split('.')[0];
            // }

            games[j].pathToExe = files[0];
            games[j].name = files[0].Split('.')[0];

            var dir = System.IO.Directory.GetDirectories(pathToGame, "GameMeta");

            //On vérifie que le dossier existe sinon on prend le dossier GameMetaDefault
            if (dir == null)
            {
                games[j].pathToGameMeta = pathToGames + "../GameMetaDefault";
            }
            else
            {
                games[j].pathToGameMeta = dir[0];
            }
            j++;
        }
    }
}
