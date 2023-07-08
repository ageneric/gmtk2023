using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    string usernames = "snap;get;melodic;opening;tshirt;supporter;loan;simply;enchant;scold;axel;came;convolvulus;and;argent;illiterate;power;landscape;grand;dug;translate;shrug;everyday;sport;obey;maker;miniature;mussels;perpetual;told;wool;sarong;petticoat;feliz;swim;achoo;reserved;neither;serpentine;culottes;nucleus;level;coal;seagull;sales;evening;insidious;pelt;key;disgusted";
    List<string> usernamelist;
    public string[] possiblehacks;
    // Start is called before the first frame update
    void Start()
    {
        width = spawnArea.size[0];
        usernamelist = usernames.Split(";").ToList<string>();
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
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;

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
        int usernameindex = Random.Range(0, usernamelist.Count);
        newFighter.username = usernamelist[usernameindex];
        if (Random.Range(0, 3) > 0) {
            newFighter.username += Random.Range(0, 99).ToString("00");
        }
        usernamelist.RemoveAt(usernameindex);
        gameObjectFighter.name = "Fighter " + newFighter.username + "_" + Random.Range(0, 9999).ToString("0000");

        if (fighterIsHacker)
        {
            newFighter.hacks.Add(possiblehacks[UnityEngine.Random.Range(0,possiblehacks.Length)]);
            //newFighter.hacks.Add("IMMUN");
        }

        if(newFighter.hacks.Contains("NOCLP"))
        {
            gameObjectFighter.layer = LayerMask.NameToLayer("NoClipPlayer");
        }

        leaderboard.RegisterFighter(newFighter);
    }
}
