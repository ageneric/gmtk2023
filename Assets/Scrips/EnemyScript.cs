using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public List<Transform> waypoints = new List<Transform>();
    SpriteRenderer sr;
    public Transform target;
    Animator anim;
    public Vector2 chosenPos;
    Vector2 chosenPosDelta;
    Vector2 oldvel;
    Vector3 oldpos;
    List<Vector2> blacklistedDirns = new List<Vector2>();
    public Vector3 startPos;
    public int command = 0;
    public int awareness = 10;
    public float minClearance=5f;
    public float visionRange = 10f;
    public float combatRange = 5f;
    public float maxBloom = 0.25f;
    public float speedMultiplier = 2f;
    public float fireRateMultiplier = 3f;
    public float combatTime = 1f;
    public float movementTime = 6f;
    public float confidence = 0f;
    public float confidencegain = 0.2f;
    bool speedSelect;
    bool isCombat;
    public bool isLooking=false;
    public bool isWaiting;
    public bool isHacking;
    Fighter f;
    Rigidbody2D rb;

    public GameObject bullet;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        oldpos = transform.position;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        f = GetComponent<Fighter>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            waypoints.Add(g.transform);
        }
        
    }

    private void Awake()
    {
        startPos = transform.position;
        
    }

    int comparePathVectors(Vector2 a,Vector2 b)
    {
        float dota = Vector2.Dot(a, chosenPosDelta);
        float dotb = Vector2.Dot(b, chosenPosDelta);
        return dota.CompareTo(dotb)*-1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = (oldpos - transform.position).magnitude;
        anim.SetFloat("mvmt", delta);
        oldpos = transform.position;
        if(!isHacking)
        {
            confidence += confidencegain * (f.maxHealth / f.health);
            if (confidence > 100f)
            {
                isHacking = true;
                confidence = 0;
                StartCoroutine(hackCooldown());
            }
        }
        
        
        if(!f.active)
        {
            return;
        }
        speedSelect = false;
        switch (command)
        {
            case 0:
                target = null;
                if (!isLooking)
                {
                    
                    isLooking = true;
                    StartCoroutine(lookCooldown());
                    int chosenPoint=0;
                    for (int j = 0;j < waypoints.Count;j++) {
                        Vector3 v3 = chosenPos;
                        if(v3 == waypoints[j].position)
                        {
                            if (j == 2)
                            {
                                chosenPoint = 1;
                            }
                            else if(j ==3)
                            {
                                chosenPoint = 6;
                            }else if(j == 18)
                            {
                                chosenPoint = 17;
                            }
                            else if(j == 19)
                            {
                                chosenPoint = 20;
                            }
                            else
                            {
                                chosenPoint = UnityEngine.Random.Range(0, waypoints.Count);
                            }
                        }
                    }
                    
                    chosenPos = waypoints[chosenPoint].position;
                }
                Vector2 distance = chosenPos - new Vector2(transform.position.x, transform.position.y);
                Vector2 vel = Vector2.zero;
                List<Vector2> directions = new List<Vector2>();
                chosenPosDelta = (chosenPos - new Vector2(transform.position.x, transform.position.y)).normalized;
                for (int i = 0; i < awareness; i++)
                {
                    directions.Add(new Vector2(Mathf.Cos(2 * Mathf.PI * i / awareness), Mathf.Sin(2 * Mathf.PI * i / awareness)));
                }
                directions.Sort(comparePathVectors);
                
                foreach (Vector2 v in directions)
                {
                    if (blacklistedDirns.Contains(v))
                    {
                        continue;
                    }
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, v, visionRange);
                    if (hit.collider == null && !speedSelect)
                    {
                        vel = v;
                        speedSelect = true;
                    }else{
                        
                        if ((hit.collider.tag == "Player") && hit.distance < combatRange && isLooking && hit.transform.GetComponent<Fighter>().active == true && hit.collider.gameObject != gameObject)
                        {
                            target = hit.transform;
                            command = 2;
                            isLooking = true;
                            StartCoroutine(lookCooldown());
                            break;
                        }
                        if (distance.magnitude < 0.5f)
                        {
                            transform.position = new Vector3(chosenPos.x, chosenPos.y, 0);
                            isLooking = false;
                            rb.velocity = Vector2.zero;
                            break;
                        }
                        if (hit.distance > minClearance || (f.hacks.Contains("NOCLP") && isHacking))
                        {
                            if (!speedSelect)
                            {
                                vel = v;
                                speedSelect = true;
                            }
                        }
                        else
                        {
                            blacklistedDirns.Add(v);
                        }
                    }
                }
                if(vel != oldvel)
                {
                    blacklistedDirns.Clear();
                }
                rb.velocity = vel.normalized * (f.hacks.Contains("SPEED") && isHacking ? f.speed * speedMultiplier : f.speed);
                
                sr.flipX = vel.x < 0;

                oldvel = vel;
                break;
            case 2:
                if(!isWaiting)
                {
                    isWaiting = true;
                    StartCoroutine(waitCooldown());
                }
                if(!isCombat)
                {
                    rb.velocity = Vector2.zero;
                    
                    isCombat = true;
                    chosenPos = target.position;
                    
                    Vector3 dirn = new Vector2(chosenPos.x - transform.position.x, chosenPos.y - transform.position.y).normalized;
                    float theta = Mathf.Atan2(dirn.y, dirn.x);
                    float bloom = (f.hacks.Contains("AMBT") && isHacking ? 0 : maxBloom);
                    theta += UnityEngine.Random.Range(-bloom, bloom);
                    Vector2 bulletdirn = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
                    var obj = Instantiate(bullet, transform.position + new Vector3(bulletdirn.x, bulletdirn.y, 0), Quaternion.identity);
                    if(f.hacks.Contains("XRAY") && isHacking)
                    {
                        obj.layer = LayerMask.NameToLayer("XRayBullet");
                    }
                    obj.GetComponent<BulletScript>().parent = GetComponent<EnemyScript>();
                    obj.GetComponent<Rigidbody2D>().velocity = bulletdirn * bulletSpeed;
                    float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                    rb.velocity = f.speed * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    StartCoroutine(fireBullet());
                }
                
                break;
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        command = 0;
        isLooking = false;
    }

    IEnumerator fireBullet()
    { 
        yield return new WaitForSeconds(1f / (f.hacks.Contains("FRT") && isHacking ? fireRateMultiplier : 1f));
        isCombat = false;
    }

    IEnumerator waitCooldown()
    {
        yield return new WaitForSeconds(combatTime);
        command = 0;
        isWaiting = false;
    }

    IEnumerator lookCooldown()
    {
        yield return new WaitForSeconds(movementTime);
        isLooking = false;
    }

    IEnumerator hackCooldown()
    {
        yield return new WaitForSeconds(10);
        isHacking = false;
    }
}
