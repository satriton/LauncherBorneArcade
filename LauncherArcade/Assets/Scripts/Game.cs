public struct Game
{
    public string pathToGameDir { get; set; }
    public string pathToExe { get; set; }
    public string pathToGameMeta { get; set; }
    public string name;

    public Game(string pathToGameDir, string pathToExe, string pathToGameMeta, string name)
    {
        this.pathToGameDir = pathToGameDir;
        this.pathToExe = pathToExe;
        this.pathToGameMeta = pathToGameMeta;
        this.name = name;
    }
}