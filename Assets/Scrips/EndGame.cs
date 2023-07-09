using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public List<string> bannedUsers = new List<string>();
    public List<string> hackers = new List<string>();
    public void initEndgame()
    {
        SceneManager.LoadScene("EndGame");
    }
}
