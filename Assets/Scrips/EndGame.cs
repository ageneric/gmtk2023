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

    private void Start()
    {
        endPanel.SetActive(false);
    }
    public void initEndgame()
    {
        EndGameInfo.finalBanned = bannedUsers;
        EndGameInfo.finalHacks = hackers;
        EndGameInfo.timeTaken = t.TimeRn;
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }
}
