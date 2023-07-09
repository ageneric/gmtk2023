using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public EnemyScript parent;
    public KillLog kl;
    public float bulletDmg;
    public float bulletSpd;
    // public ParticleSystem move;
    // public ParticleSystem expire;
    // public GameObject psMove;
    // public GameObject psExpire;

    private float flightTimeAnimation = 0f;
    private float lifeTime = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        kl = GameObject.Find("StaticScripts").GetComponent<KillLog>();
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;

        /*if (flightTimeAnimation > 0.25f)
        {
            flightTimeAnimation += Time.deltaTime;
            // Instantiate(psMove, parent.transform.position, Quaternion.identity);
        }*/
        if (lifeTime > 7f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            // Instantiate(psExpire, parent.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.collider.tag == "Player")
        {
            // Instantiate(psExpire, parent.transform.position, Quaternion.identity);
            Fighter f = collision.gameObject.GetComponent<Fighter>();
            Fighter pf = parent.GetComponent<Fighter>();
            if (f == pf || f == null || pf == null)
            {
                // Debug.Log("Projectile ignored collision: " + f.ToString() + " " + pf.ToString());
                // Destroy(gameObject);
                return;
            }
            pf.damageDealt += f.TakeDamage(25*(pf.hacks.Contains("DMG") && parent.isHacking? 4 : 1)*(f.hacks.Contains("IMMUN") && parent.isHacking ? 0 : 1));
            if(f.health <= 0)
            {
                Vector3 position = pf.gameObject.transform.position;
                kl.addKill(pf, f, position);
                pf.kills += 1;
                f.deaths += 1;
                parent.command = 0;
                parent.isLooking = false;
                parent.isWaiting = false;
            }
            Destroy(gameObject);
        }
    }
}
