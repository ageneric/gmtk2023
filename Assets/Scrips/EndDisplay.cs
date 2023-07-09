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
    void Start()
    {
        Debug.Log(EndGameInfo.finalHacks.ToSeparatedString(","));
        Debug.Log(EndGameInfo.finalBanned.ToSeparatedString(","));

        int i = 0;
        int j = 0;

        foreach(string s in EndGameInfo.finalBanned)
        {
            if(EndGameInfo.finalHacks.Contains(s))
            {
                i++;
            }
            else
            {
                j++;
            }
        }

        endLog.text = "You banned: " + EndGameInfo.finalBanned.ToSeparatedString(",") + "\n\nThe hackers were: "+ EndGameInfo.finalHacks.ToSeparatedString(",")+"\n\n"
            +"Hackers reported: "+i.ToString()+"\n\nInnocents reported: "+j.ToString()+"\n\nTime Taken: "+EndGameInfo.timeTaken+"\n\nFinal Score: ";
    }

    public void toMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
