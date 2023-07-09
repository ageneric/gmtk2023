using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndDisplay : MonoBehaviour
{
    public TMP_Text endLog;

    // Start is called before the first frame update
    void O()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log(EndGameInfo.finalHacks.ToSeparatedString(","));
        Debug.Log(EndGameInfo.finalBanned.ToSeparatedString(","));

        int i = 0;
        int j = 0;

        foreach (string s in EndGameInfo.finalBanned)
        {
            if (EndGameInfo.finalHacks.Contains(s))
            {
                i++;
            }
            else
            {
                j++;
            }
        }
        int score = i * 1000 - j * 500 - EndGameInfo.timeTaken * 3;
        endLog.text = "Hackers Caught: " + i + "\nInnocents Banned: " + j + "\nTotal Score: " + score;
    }

    public void toMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
