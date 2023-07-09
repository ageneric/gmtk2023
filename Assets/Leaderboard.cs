using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<Fighter> fighters = new List<Fighter>();
    public List<RecordButton> records = new List<RecordButton>();

    public GameObject battleUI;
    public GameObject postGameUI;
    public Transform recordContainer;
    public GameObject recordText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Game end condition.
        int playersRemaining = 0;
        foreach (Fighter fighter in fighters)
        {
            if (fighter.active)
            {
                playersRemaining += 1;
            }
        }

        if (playersRemaining == 0)
        {
            EndGame();
        }
        else
        {
            for (int i=0; i<fighters.Count; i++)
            {
                Fighter fighter = fighters[i];
                RecordButton record = records[i];
                record.eventLocation = fighter.transform.position;

                UpdateTextFields(record.gameObject, fighter);
            }
        }
    }

    public void EndGame()
    {
        battleUI.SetActive(false);
        postGameUI.SetActive(true);
    }

    public void RegisterFighter(Fighter newFighter)
    {
        fighters.Add(newFighter);
        AddLeaderboardRecord(newFighter);
    }

    public void AddLeaderboardRecord(Fighter fighter)
    {
        var obj = Instantiate(recordText);
        obj.transform.SetParent(recordContainer);
        records.Add(obj.GetComponent<RecordButton>());
        obj.GetComponent<RecordButton>().eventLocation = fighter.transform.position;
        UpdateTextFields(obj, fighter);
    }

    public void UpdateTextFields(GameObject obj, Fighter fighter)
    {
        TMP_Text[] textFields = obj.GetComponentsInChildren<TMP_Text>();
        textFields[0].text = fighter.username;

        int timeSeconds = (int)fighter.timeSurvived;  // TODO: Want a function for seconds since game began
        var timeSpan = TimeSpan.FromSeconds(timeSeconds);
        textFields[1].text = timeSpan.ToString(@"mm\:ss");

        textFields[2].text = "K:" + fighter.kills.ToString() + " D:" + fighter.deaths.ToString()
            + " A:" + Mathf.Round(fighter.damageDealt).ToString();
    }
}
