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
    public string hack = "";
    public bool active = true;
    public bool banned = false;

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
            if(health <= 0)
            {
                active = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                GetComponentInChildren<MeshRenderer>().enabled = false;
                StartCoroutine(respawn());
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);
                if (cubeHit) Ban();
            }

            if (health < maxHealth)
            {
                health = Mathf.Min(health + regeneration * Time.deltaTime,maxHealth);
            }
            else
            {
                health = maxHealth;
            }

            health = Mathf.Round(health);

            timeSurvived += Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if(active)
        {
            health -= damage;
        }
        
        // TODO: Animate sprite upon taking damage.
    }

    public void Ban()
    {
        // Ban this fighter.
        banned = true;
        active = false;

        // TEMPORARY: banning simply hides the sprite.
        // TODO: contain all the living fighter logic under a child GameObject
        //       then disable it when the fighter is banned.
        gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3);
        transform.position = enemy.startPos;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        health = maxHealth;
        active = true;
    }
}
