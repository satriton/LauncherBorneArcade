using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine;

public class SC_Launcher : MonoBehaviour
{
    // TODO parcourir Games/ à la recherche de tous les jeux, les stocke dans un tableau de string,
    // Charge toutes les données qu'il a récupérer dans l'UI et les bouttons

    public GameObject gameBtnPrefab;
    [Range(1, 5)] public int nbGameByPage;
    public static string pathToGames;
    //public TMP_Text




    private DirectoryInfo dirInfo;
    private Process process;



    private struct Game
    {
        public string pathToGameDir { get; set; }
        public string pathToExe { get; set; }
        public string pathToAsset { get; set; }
        public string name;
    }
    private Game[] games;

    private GameObject[] gamesBtn;



    void Start()
    {
        pathToGames = Application.dataPath + "/../../Games/";


        // Instanciate gamesBtn
        gamesBtn = new GameObject[nbGameByPage];
        for (int i = 0; i < nbGameByPage; i++)
        {
            var gameBtn = gamesBtn[i];
            gameBtn = Instantiate(gameBtnPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            gameBtn.transform.parent = transform;
            gameBtn.GetComponent<RectTransform>().localScale = Vector3.one;
        }


        updateGameList();



    }

    void Update()
    {
        // if (process.HasExited)
        // {
        //     process = null;
        // }
    }

    public void LaunchchGame(string pathToExe)
    {
        process = Process.Start(pathToExe);
    }

    private void changePage(bool boolGoRight)
    {

    }

    // Load assets (and description ?) on the gamesBtn for games[index] à games[index + nbGameByPage]
    private void loadGameBtnAssets(int index)
    {

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
            games[j].pathToExe = files[0]; // Il faudrait vérifier que ce soit bien le bon .exe par pas celui de l'handler ---------------------------------------------------
            games[j].name = files[0].Split('.')[0];

            var dir = System.IO.Directory.GetDirectories(pathToGame, "GameMeta");
            games[j].pathToAsset = dir[0]; // Il faudrait vérifier que le dossier existe et mettre un chemin pour ---------------------------------------------------

            // UnityEngine.Debug.Log("gamedir: " + games[j].pathToGameDir + "\n");
            // UnityEngine.Debug.Log("pathexe: " + games[j].pathToExe + "\n");
            // UnityEngine.Debug.Log("GameMeta: " + games[j].pathToAsset + "\n");
            // UnityEngine.Debug.Log("name: " + games[j].name + "\n\n");
            j++;
        }
    }
}
