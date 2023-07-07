using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    List<Transform> waypoints = new List<Transform>();
    Transform target;
    Vector2 chosenPos;
    Vector2 chosenPosDelta;
    int command = 0;
    public int awareness = 10;
    public float minClearance=5f;
    bool speedSelect;
    bool isCombat;
    Rigidbody2D rb;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            waypoints.Add(g.transform);
        }
        onSpawn();
        
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
        speedSelect = false;
        switch (command)
        {
            case 0:
                
                Vector2 distance = chosenPos - new Vector2(transform.position.x, transform.position.y);
                
                if (distance.magnitude < 0.01f)
                {
                    transform.position = new Vector3(chosenPos.x, chosenPos.y, 0);
                    command = 1;
                    rb.velocity = Vector2.zero;
                    break;
                }
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
                    Debug.Log(hit);
                    Debug.Log(hit.collider.name);
                    if(hit.collider.tag == "Player" && hit.distance < 100)
                    {
                        command = 2;
                        target = hit.transform;
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
                    else if(hit.distance < 0.5*minClearance)
                    {
                        vel += -v;
                    }
                }
                rb.velocity = vel.normalized;
                break;
            case 2:
                if(!isCombat)
                {
                    rb.velocity = Vector2.zero;
                    
                    isCombat = true;
                    chosenPos = target.position;
                    var obj = Instantiate(bullet, transform.position + (new Vector3(chosenPos.x,chosenPos.y,0)-transform.position).normalized, Quaternion.identity);
                    
                    Vector3 dirn = new Vector2(chosenPos.x - transform.position.x, chosenPos.y - transform.position.y).normalized;
                    obj.GetComponent<Rigidbody2D>().velocity = new Vector2(dirn.x, dirn.y).normalized;
                    float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                    rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    StartCoroutine(fireBullet());
                }
                
                break;
        }
    }

    IEnumerator fireBullet()
    {
        yield return new WaitForSeconds(1);
        isCombat = false;
    }

    void onSpawn()
    {
        
        int chosenPoint = UnityEngine.Random.Range(0, waypoints.Count);
        chosenPos = waypoints[chosenPoint].position;
        Debug.Log(chosenPos);
    }
}
