using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    public Leaderboard leaderboard;
    public GameObject gameObj;

    // Detect when all players killed.
    // Start is called before the first frame update
    void Start()
    {
        gameObj.SetActive(GameSettings.showAliveHackers);

    }

    public void Toggle()
    {
        GameSettings.showAliveHackers = !GameSettings.showAliveHackers;
    }
}

public class GameSettings
{
    public static bool showAliveHackers = false;
}