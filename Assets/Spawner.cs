using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public GameObject prefabFighter;
    public Leaderboard leaderboard;
    public int spawnCount = 1;

    private float width;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        width = spawnArea.size[0];
        CreateFighterGroup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFighterGroup()
    {
        bool[] fighterIsHacker = new bool[spawnCount];
        //TEMP: hacks disabled
        //fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;

        for (int i=0; i<spawnCount; i++)
        {
            CreateFighter(i, fighterIsHacker[i]);
        }
    }

    public void CreateFighter(int i, bool fighterIsHacker)
    {
        //Old spawn code commented out for (possibly temp) FFA code
        /**Vector2 position = new Vector2(transform.position.x + width * ((float)i / (float)spawnCount) - width/2f,
                                       transform.position.y);**/
        Vector2 position = spawnPoints[i].position;
        GameObject gameObjectFighter = Instantiate(prefabFighter, position, Quaternion.identity);
        Fighter newFighter = gameObjectFighter.GetComponent<Fighter>();

        if (fighterIsHacker)
        {
            newFighter.hack = "defaultHack";
        }

        leaderboard.RegisterFighter(newFighter);
    }
}
