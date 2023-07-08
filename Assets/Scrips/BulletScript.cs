using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Fighter>().TakeDamage(34);
        }
    }
}
