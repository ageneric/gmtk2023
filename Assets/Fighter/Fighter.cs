using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fighter : MonoBehaviour
{
    [Header("Handles stats and primary actions", order = 1)]
    [Header("Not including movement or AI.", order = 2)]

    public float health = 1;
    public float maxHealth = 1;
    public float speed = 1;
    public float stamina = 1;
    public float regeneration = 1;

    public float timeSurvived = 0;

    public float block = 0;

    public string username = "";
    public List<string> hacks = new List<string>();
    public bool active = true;
    public bool banned = false;

    public int kills = 0;
    public int deaths = 0;
    public float damageDealt = 0;

    public GameObject visibleProfile;
    public SpriteRenderer spriteRenderer;
    Spawner s;
    EnemyScript enemy;
    EndGame e;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        s = GameObject.Find("FighterSpawnZone").GetComponent<Spawner>();
        e = GameObject.Find("StaticScripts").GetComponent<EndGame>();
        enemy = GetComponent<EnemyScript>();
        health = maxHealth;
        timeSurvived = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0) && ToolSelect.userTool == ToolSelect.Tool.BanHammer)
            {
                Vector2 clickPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Collider2D collider = Physics2D.OverlapPoint(clickPos);
                if (collider != null && collider.gameObject.tag == "Player" && collider.name == name)
                {
                    collider.gameObject.GetComponent<Fighter>().Ban();
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray);
                Debug.Log(hit);
                Debug.Log(hit.collider);
            }

            // Death
            if (health <= 0)
            {
                Knockout();
            }

            if (health < maxHealth)
            {
                health = Mathf.Min(health + regeneration * Time.deltaTime,maxHealth);
            }
            else
            {
                health = maxHealth;
            }

            timeSurvived += Time.deltaTime;
        }
    }

    public float TakeDamage(float damage)
    {
        // Return the damage dealt
        // TODO: Animate sprite upon taking damage.

        if (active)
        {
            health -= damage;
            return Mathf.Max(0, Mathf.Min(damage, damage + health));
        }
        else
        {
            return 0f;
        }
    }

    public void Ban()
    {
        
        anim.SetTrigger("Ban");
        
        List<Report> newReports = new List<Report>();
        foreach(Report r in s.reports)
        {
            if(username != r.accusor && username != r.accusee)
            {
                newReports.Add(r);
            }
        }
        s.reports = newReports;
        // Ban this fighter.
        banned = true;
        Knockout();
        e.bannedUsers.Add(username);
    }

    public void Knockout()
    {
        enemy.rb.velocity = Vector2.zero;
        // Kill this fighter and start the respawn timer.
        active = false;
        timeSurvived = 0;
        StartCoroutine(respawn());
    }

    public float OrderingScore()
    {
        return damageDealt + kills*25 - deaths*20;
    }

    IEnumerator respawn()
    {
        if(banned)
        {
            yield return new WaitForSeconds(1.3f);
        }
        else
        {
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(0.7f);
        }
        
        visibleProfile.SetActive(false);
        spriteRenderer.color = new Color(1, 1, 1);

        if (!banned)
        {
            yield return new WaitForSeconds(1.75f);
            transform.position = enemy.waypoints[UnityEngine.Random.Range(0,enemy.waypoints.Count)].position;
            enemy.chosenPos = transform.position;
            enemy.command = 0;
            visibleProfile.SetActive(true);
            health = maxHealth;
            active = true;
        }
    }
}
