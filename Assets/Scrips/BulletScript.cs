using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public EnemyScript parent;
    public KillLog kl;
    // Start is called before the first frame update
    private void Start()
    {
        kl = GameObject.Find("StaticScripts").GetComponent<KillLog>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(collision.collider.tag == "Player")
        {
            Fighter f = collision.gameObject.GetComponent<Fighter>();
            Fighter pf = parent.GetComponent<Fighter>();
            if (f == pf)
            {
                Destroy(gameObject);
                return;
            }
            collision.gameObject.GetComponent<Fighter>().TakeDamage(34);
            if(f.health <= 0)
            {
                kl.addKill(pf, f);
                parent.command = 0;
                parent.isLooking = false;
                parent.isWaiting = false;
            }
            Destroy(gameObject);
        }
    }
}
