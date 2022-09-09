using System.Collections;
using System.Collections.Generic;

public class SetupTestEnvironment
{
    public static List<Game> getGamesDummy()
    {
        var games = new List<Game>
        {
            new Game("C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/Old Town",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/Old Town/Old Town.exe",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/Old Town/GameMeta",
                    "OldTown"),
            new Game("C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/OverFoy_Build",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/OverFoy_Build/7FaultFoyGame.exe",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/OverFoy_Build/GameMeta",
                    "OverFoy"),
            new Game("C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/SpaceCannele_v4",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/SpaceCannele_v4/Space_Cannele.exe",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/SpaceCannele_v4/GameMeta",
                    "SpaceCannele"),
            new Game("C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/SuperDonjonII",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/SuperDonjonII/GGJ_projetUnity.exe",
                    "C:/Users/panac/OneDrive/Bureau/Unity Build/Launcher/Games/SuperDonjonII/GameMeta",
                    "SuperDonjonII")
        };

        return games;
    }
}
