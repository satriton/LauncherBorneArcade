using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SC_LauncherControler : MonoBehaviour, Controls.IArcadeActions
{
    public static string pathToGames;
    public TextMeshProUGUI newsZone;
    public TextMeshProUGUI gameNumberTxt;

    public Transform gameStartedTransfort;
    private TextMeshProUGUI gameStartedText;

    private Process process;
    private SC_LauncherModel model;
    private Controls controls;

    [SerializeField] private GameObject[] gamestiles = new GameObject[3]; 
    private List<Game> games;
    private int nbGames;
    private int currentGameIndex;

    void Awake()
    {
        Application.runInBackground = false;
        Application.targetFrameRate = 60;

        controls = new Controls();

        pathToGames = Application.dataPath + "/../Games/";
        model = new SC_LauncherModel(pathToGames);
        currentGameIndex = 0;

        // Hide GameStarted message
        gameStartedText = gameStartedTransfort.Find("GameStartedBackground").Find("GameStartedText").GetComponent<TextMeshProUGUI>();
        HideGameStartedMessage();

        // Get games list and show them
        games = model.GetGamesList();
        //games = SetupTestEnvironment.getGamesDummy(); // For coding/Debug
        nbGames = games.Count;

        updateGamesPreviews(currentGameIndex);

        // Update texts
        gameNumberTxt.text = "1/" + games.Count;
        newsZone.text = model.GetNews();

        // Bind controls
        controls.Arcade.Enter.performed += OnEnter;
        controls.Arcade.Left.performed += OnLeft;
        controls.Arcade.Right.performed += OnRight;
    }

    private void Update()
    {
        if(process != null && process.HasExited)
        {
            process = null;
            controls.Arcade.Enter.performed += OnEnter;
            HideGameStartedMessage();
        }
    }

    // Gestion of controls
    public void OnEnter(InputAction.CallbackContext context)
    {
        LaunchGame(games[currentGameIndex]);
    }
    public void OnLeft(InputAction.CallbackContext context)
    {
        currentGameIndex = modulo(currentGameIndex - 1, nbGames);
        updateGamesPreviews(currentGameIndex);
    }
    public void OnRight(InputAction.CallbackContext context)
    {
        currentGameIndex = modulo(currentGameIndex + 1, nbGames);
        updateGamesPreviews(currentGameIndex);
    }

    // Launch the game (at the current game index) in the main emplacement 
    private void LaunchGame(Game game)
    {
        process = Process.Start(game.pathToExe);
        ShowGameStartedMessage(game.name);
        controls.Arcade.Enter.performed -= OnEnter;
    }

    // Show the correct game depending on the currenGameIndex
    private void updateGamesPreviews(int currentGameIndex)
    {
        int nbGames = games.Count;
        if (nbGames < 3)
        {
            // On peut charger un jeu avant dans la liste
            if (currentGameIndex != 0)
            {
                loadGameData(games[modulo(currentGameIndex - 1, nbGames)], 0);
                // Supprimer la case de droite comme il n'y a que 2 jeux
                gamestiles[2].GetComponent<Image>().sprite = null;
                gamestiles[2].GetComponent<Image>().color = new Color(0x5B, 0x5B, 0x5B);
                gamestiles[2].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

            // On peut charger un jeu après dans la liste
            if (currentGameIndex != games.Count - 1)
            {
                loadGameData(games[modulo(currentGameIndex + 1, nbGames)], 2);
                // Supprimer la case de gauche comme il n'y a que 2 jeux
                gamestiles[0].GetComponent<Image>().sprite = null;
                gamestiles[0].GetComponent<Image>().color = new Color(0x5B, 0x5B, 0x5B);
                gamestiles[0].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

            loadGameData(games[currentGameIndex], 1);
        }
        else
        {
            loadGameData(games[modulo(currentGameIndex - 1, nbGames)], 0);
            loadGameData(games[currentGameIndex], 1);
            loadGameData(games[modulo(currentGameIndex + 1, nbGames)], 2);
        }

        gameNumberTxt.text = currentGameIndex + 1 + "/" + games.Count;
    }
    
    // Load the gameToShow in the correct emplacement (tile)
    private void loadGameData(Game gameToShow, int tileIndex)
    {
        //Load de l'image
        string pathToGameLogo = gameToShow.pathToGameMeta + "/logo.png";

        float PixelsPerUnit = 100.0f;
        Texture2D spriteTexture;
        Sprite gameImage;
        if (File.Exists(pathToGameLogo))
        {
            spriteTexture = LoadTexture(pathToGameLogo);
            gameImage = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
        }
        else
        {
            gameImage = Resources.Load<Sprite>("logo");
        }

        gamestiles[tileIndex].GetComponent<Image>().sprite = gameImage;

        //Load du titre
        gamestiles[tileIndex].GetComponentInChildren<TextMeshProUGUI>().text = gameToShow.name;
    }

    // Load a PNG or JPG file from disk to a Texture2D (Returns null if load fails)
    private Texture2D LoadTexture(string filePath)
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

    private void ShowGameStartedMessage(string gameName)
    {
        gameStartedTransfort.gameObject.SetActive(true);
        gameStartedText.text = "starting " + gameName;
    }

    private void HideGameStartedMessage()
    {
        gameStartedTransfort.gameObject.SetActive(false);
    }

    private int modulo(int a, int n)
    {
        return ((a % n) + n) % n;
    }

    private void OnEnable()
    {
        controls.Arcade.Enable();
    }

    private void OnDisable()
    {
        controls.Arcade.Disable();
    }
}
