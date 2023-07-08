using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillLog : MonoBehaviour
{
    public Transform killLog;
    public GameObject killText;
    string[] killWords = {"killed","bested","noscoped","pwned","rekt","bamboozled","destroyed","slew"};
    public void addKill(Fighter killer, Fighter killee)
    {
        var obj = Instantiate(killText);
        obj.transform.SetParent(killLog);
        obj.GetComponent<TMP_Text>().text = killer.username + " " + killWords[UnityEngine.Random.Range(0, killWords.Length)] + " " + killee.username;
    }
}
