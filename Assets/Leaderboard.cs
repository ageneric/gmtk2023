using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public List<Fighter> fighters = new List<Fighter>();

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

    }

    public void RegisterFighter(Fighter newFighter)
    {
        fighters.Add(newFighter);
    }
}
