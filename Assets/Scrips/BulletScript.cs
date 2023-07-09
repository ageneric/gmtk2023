using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public EnemyScript parent;
    public KillLog kl;
    public float bulletDmg;
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
            pf.damageDealt += f.TakeDamage(25*(pf.hacks.Contains("DMG") && parent.isHacking? 4 : 1)*(f.hacks.Contains("IMMUN") && parent.isHacking ? 0 : 1));
            if(f.health <= 0)
            {
                kl.addKill(pf, f, pf.gameObject.transform.position);
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
