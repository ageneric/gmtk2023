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

        obj.GetComponent<RecordButton>().eventLocation = fighter.transform.position;

        TMP_Text[] textFields = obj.GetComponentsInChildren<TMP_Text>();
        textFields[0].text = fighter.username;

        int timeSeconds = (int)fighter.timeSurvived;  // TODO: Want a function for seconds since game began
        var timeSpan = TimeSpan.FromSeconds(timeSeconds);
        textFields[1].text = timeSpan.ToString(@"mm\:ss");

        textFields[2].text = fighter.hacks.ToString();
    }
}
