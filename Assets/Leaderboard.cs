using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<Fighter> fighters = new List<Fighter>();
    public List<RecordButton> liveRecords = new List<RecordButton>();
    public List<RecordButton> finalRecords = new List<RecordButton>();
    public GameObject battleUI;
    public GameObject postGameUI;
    public Transform recordContainer;
    public Transform postGameRecordContainer;
    public GameObject recordText;
    public GameObject postGameRecordText;
    public Color bannedItemColor = new Color(1f, 0.5f, 0.5f, 0.5f);

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
                RecordButton record = liveRecords[i];
                record.eventLocation = fighter.transform.position;

                UpdateFields(record.gameObject, fighter, false);
                Sort(liveRecords);
            }
        }
    }

    public void EndGame()
    {
        battleUI.SetActive(false);
        postGameUI.SetActive(true);

        foreach (Fighter fighter in fighters)
        {
            fighter.active = false;
            AddLeaderboardRecord(fighter, true);
            Sort(finalRecords);
        }
    }

    public void RegisterFighter(Fighter newFighter)
    {
        fighters.Add(newFighter);
        AddLeaderboardRecord(newFighter, false);
    }

    public void AddLeaderboardRecord(Fighter fighter, bool postGame)
    {
        var obj = Instantiate(recordText);
        if (postGame)
        {
            obj.transform.SetParent(postGameRecordContainer);
            finalRecords.Add(obj.GetComponent<RecordButton>());
        }
        else
        {
            obj.transform.SetParent(recordContainer);
            liveRecords.Add(obj.GetComponent<RecordButton>());
        }

        obj.GetComponent<RecordButton>().eventLocation = fighter.transform.position;
        UpdateFields(obj, fighter, postGame);
    }

    public void UpdateFields(GameObject obj, Fighter fighter, bool postGame)
    {
        if (fighter.banned)
        {
            obj.GetComponent<Image>().color = bannedItemColor;
        }

        TMP_Text[] textFields = obj.GetComponentsInChildren<TMP_Text>();
        textFields[0].text = fighter.username;

        int timeSeconds = (int)fighter.timeSurvived;  // TODO: Want a function for seconds since game began
        var timeSpan = TimeSpan.FromSeconds(timeSeconds);
        textFields[1].text = timeSpan.ToString(@"mm\:ss");

        textFields[2].text = (Mathf.RoundToInt(fighter.OrderingScore() / 5) * 5).ToString();

        textFields[3].text = "K:" + fighter.kills.ToString() + " / D:" + fighter.deaths.ToString();

        if (postGame)
        {
            textFields[4].text = string.Join(", ", fighter.hacks);
        }
    }

    public void Sort(List<RecordButton> records)
    {
        List<Fighter> sortedFighters = fighters.OrderBy(o => o.banned ? -9999 : o.OrderingScore()).ToList();
        foreach(Fighter fighter in sortedFighters)
        {
            int index = fighters.IndexOf(fighter);
            records[index].transform.SetAsFirstSibling();
        }
    }
}
