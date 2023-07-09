using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public List<string> bannedUsers = new List<string>();
    public List<string> hackers = new List<string>();
    public GameObject endPanel;
    public Timer t;
    public Leaderboard l;
    private void Start()
    {
        endPanel.SetActive(false);
    }
    public void initEndgame()
    {
        Debug.Log("Called");
        EndGameInfo.finalBanned = bannedUsers;
        EndGameInfo.finalHacks = hackers;
        EndGameInfo.timeTaken = t.TimeRn;
        l.EndGame();
        
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }

    
}
