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
    EnemyScript enemy;


    // Start is called before the first frame update
    void Start()
    {
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
        // Ban this fighter.
        banned = true;
        Knockout();
    }

    public void Knockout()
    {
        // Kill this fighter and start the respawn timer.
        active = false;
        StartCoroutine(respawn());
    }

    IEnumerator respawn()
    {
        spriteRenderer.color = new Color(1, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(0.25f);
        visibleProfile.SetActive(false);
        spriteRenderer.color = new Color(1, 1, 1);

        if (!banned)
        {
            yield return new WaitForSeconds(1.75f);
            transform.position = enemy.waypoints[UnityEngine.Random.Range(0,enemy.waypoints.Count)].position;
            enemy.command = 0;
            visibleProfile.SetActive(true);
            health = maxHealth;
            active = true;
        }
    }
}
