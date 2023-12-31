using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillLog : MonoBehaviour
{
    public Transform killLog;
    public GameObject killText;
    string[] killWords = {"killed","bested","noscoped","pwned","rekt","bamboozled","destroyed","slew"};

    List<Vector2> killLocations = new List<Vector2>();

    public void addKill(Fighter killer, Fighter killee, Vector2 killLocation)
    {
        killLocations.Add(killLocation);

        var obj = Instantiate(killText);
        obj.transform.SetParent(killLog);

        obj.GetComponent<RecordButton>().eventLocation = killLocation;

        TMP_Text[] textFields = obj.GetComponentsInChildren<TMP_Text>();
        textFields[0].text = killer.username + " " + killWords[UnityEngine.Random.Range(0, killWords.Length)] + " " + killee.username;

        int timeSeconds = Mathf.RoundToInt(Time.time);  // TODO: Want a function for seconds since game began
        var timeSpan = TimeSpan.FromSeconds(timeSeconds);
        textFields[1].text = timeSpan.ToString(@"mm\:ss");
    }
}
