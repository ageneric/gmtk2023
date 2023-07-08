using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    List<Transform> waypoints = new List<Transform>();
    public Transform target;
    Vector2 chosenPos;
    Vector2 chosenPosDelta;
    public Vector3 startPos;
    public int command = 0;
    public int awareness = 10;
    public float minClearance=5f;
    bool speedSelect;
    bool isCombat;
    public bool isLooking=false;
    public bool isWaiting;
    Fighter f;
    Rigidbody2D rb;

    public GameObject bullet;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {
        
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
                    int chosenPoint = UnityEngine.Random.Range(0, waypoints.Count);
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
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, v);
                    if((hit.collider.tag == "Player") && hit.distance < 100 && isLooking && hit.transform.GetComponent<Fighter>().active == true && hit.collider.gameObject != gameObject)
                    {
                        target = hit.transform;
                        command = 2;
                        isLooking = true;
                        StartCoroutine(lookCooldown());
                        
                        rb.velocity = Vector2.zero;
                        break;
                    }
                    if (distance.magnitude < 0.01f)
                    {
                        transform.position = new Vector3(chosenPos.x, chosenPos.y, 0);
                        rb.velocity = Vector2.zero;

                        break;
                    }
                    if (hit.distance > minClearance)
                    {
                        if(!speedSelect)
                        {
                            vel = v;
                            speedSelect = true;
                        }
                    }
                    else if(hit.distance < 0.5*minClearance && !f.hacks.Contains("NOCLP"))
                    {
                        vel = -v;
                    }
                }
                rb.velocity = vel.normalized * (f.hacks.Contains("SPEED") ? 2 : 1);
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
                    float bloom = (f.hacks.Contains("AMBT") ? 0 : 0.25f);
                    theta += UnityEngine.Random.Range(-bloom, bloom);
                    Vector2 bulletdirn = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
                    var obj = Instantiate(bullet, transform.position + new Vector3(bulletdirn.x, bulletdirn.y, 0), Quaternion.identity);
                    if(f.hacks.Contains("XRAY"))
                    {
                        obj.layer = LayerMask.NameToLayer("XRayBullet");
                    }
                    obj.GetComponent<BulletScript>().parent = GetComponent<EnemyScript>();
                    obj.GetComponent<Rigidbody2D>().velocity = bulletdirn * bulletSpeed;
                    float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                    rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    StartCoroutine(fireBullet());
                }
                
                break;
        }
    }

    IEnumerator fireBullet()
    { 
        yield return new WaitForSeconds(1f / (f.hacks.Contains("FRT") ? 2f : 1f));
        isCombat = false;
    }

    IEnumerator waitCooldown()
    {
        yield return new WaitForSeconds(1);
        command = 0;
        isWaiting = false;
    }

    IEnumerator lookCooldown()
    {
        yield return new WaitForSeconds(12);
        isLooking = false;
    }
}
