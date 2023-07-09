using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Spawner : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public GameObject prefabFighter;
    public Leaderboard leaderboard;
    public int spawnCount = 1;

    public GameObject reportPanel;
    public TMP_Text reportText;

    public EndGame e;

    private float width;
    public Transform[] spawnPoints;
    public GameObject spawnPointHolder;
    string usernames = "snap;get;melodic;opening;tshirt;supporter;loan;simply;enchant;scold;axel;argent;illiterate;power;landscape;grand;dug;translate;shrug;everyday;sport;obey;maker;miniature;mussels;perpetual;told;wool;sarong;petticoat;feliz;swim;achoo;reserved;neither;serpentine;culottes;nucleus;level;coal;seagull;sales;evening;insidious;pelt;key;disgusted;thelegend;tank;heroic;pixel";
    List<string> usernamelist;
    List<string> usernamesInPlay = new List<string>();
    List<string> hackerUserNames = new List<string>();
    public string[] possiblehacks;
    public List<Report> reports;
    public List<float> delays;
    float maxTime = 300;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = spawnPointHolder.GetComponentsInChildren<Transform>();
        reportPanel.SetActive(false);
        width = spawnArea.size[0];
        usernamelist = usernames.Split(";").ToList<string>();
        CreateFighterGroup();
        e.hackers = hackerUserNames;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFighterGroup()
    {
        reports = new List<Report>();

        bool[] fighterIsHacker = new bool[spawnCount];
        //TEMP: hacks disabled
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;
        fighterIsHacker[Random.Range(0, spawnCount - 1)] = true;

        for (int i=0; i<spawnCount; i++)
        {
            CreateFighter(fighterIsHacker[i]);
        }
        
        for(int i=0;i<UnityEngine.Random.Range(hackerUserNames.Count*2,hackerUserNames.Count*3+2);i++)
        {
            Report r = new Report();
            if(i<hackerUserNames.Count)
            {
                r.accusee = hackerUserNames[i];
            }
            else
            {
                r.accusee = usernamesInPlay[UnityEngine.Random.Range(0, usernamesInPlay.Count)];
            }
            string a;
            do
            {
                a = usernamesInPlay[UnityEngine.Random.Range(0, usernamesInPlay.Count)];
            } while (a == r.accusee);
            r.accusor = a;
            reports.Add(r);
        }



        for(int i=0;i<reports.Count;i++)
        {
            float offset = 0;
            if (i == 0)
            {
                offset = 10;
            }
            delays.Add(UnityEngine.Random.Range(0, maxTime / (reports.Count + 1)) + offset);
        }

        Shuffle.shuffle(reports);

        foreach(Report r in reports)
        {
            Debug.Log(r.accusor + " reported " + r.accusee + ": " + r.flavour);
        }

        StartCoroutine(ReportCycle());
    }

    IEnumerator ReportCycle()
    {
        yield return new WaitForSeconds(delays[0]);
        if(reports.Count > 0)
        {
            reportText.text = reports[0].accusor + " reported " + reports[0].accusee + ",\n\n" + "\"" + reports[0].flavour + "\"";
            reportPanel.SetActive(true);
            if (delays.Count > 0)
            {
                delays.RemoveAt(0);
                reports.RemoveAt(0);
                StartCoroutine(ReportCycle());
            }
        }
    }

    public void hideReport()
    {
        reportPanel.SetActive(false);
    }

    public void CreateFighter(bool fighterIsHacker)
    {
        //Old spawn code commented out for (possibly temp) FFA code
        /**Vector2 position = new Vector2(transform.position.x + width * ((float)i / (float)spawnCount) - width/2f,
                                       transform.position.y);**/
        Vector2 position = spawnPoints[UnityEngine.Random.Range(0,spawnPoints.Length)].position;
        GameObject gameObjectFighter = Instantiate(prefabFighter, position, Quaternion.identity);
        Fighter newFighter = gameObjectFighter.GetComponent<Fighter>();
        int usernameindex = Random.Range(0, usernamelist.Count);
        newFighter.username = usernamelist[usernameindex];
        if (Random.Range(0, 3) > 0) {
            newFighter.username += Random.Range(0, 99).ToString("00");
        }
        usernamesInPlay.Add(newFighter.username);
        usernamelist.RemoveAt(usernameindex);
        gameObjectFighter.name = "Fighter " + newFighter.username + "_" + Random.Range(0, 9999).ToString("0000");

        if (fighterIsHacker)
        {
            string newhack = possiblehacks[UnityEngine.Random.Range(0, possiblehacks.Length)];
            newFighter.hacks.Add(newhack);
            hackerUserNames.Add(newFighter.username);
        }

        if(newFighter.hacks.Contains("NOCLP") && newFighter.GetComponent<EnemyScript>().isHacking)
        {
            gameObjectFighter.layer = LayerMask.NameToLayer("NoClipPlayer");
        }

        leaderboard.RegisterFighter(newFighter);
    }
}

public class Report
{
    public string accusor;
    public string accusee;
    public string flavour;
    string[] possibleFlavours = { "HAXHAXHAXHAXHAXHAXHAX", "I hope that !*£&$(£^& $£&&!*$&_ will &£*£()$%&^ @}:{}@~@ until the day they &$&%^£(!}:}{}{@!!!", "THIS PLAYER IS CHEATING I CAN FEEL IT IN MY BONES >:(", "(this report has been redacted, as it violates community guidelines)", "This player is cheating harder than my ex cheated on me...", "Help me Banhammer - you're my only hope..." };

    public Report()
    {
        flavour = possibleFlavours[UnityEngine.Random.Range(0, possibleFlavours.Length)];
    }
}

public static class Shuffle
{
    public static void shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


