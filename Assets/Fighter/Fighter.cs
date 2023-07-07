using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fighter : MonoBehaviour
{
    [Header("Fighter statistics and primary actions.", order = 1)]
    [Header("Does not handle movement.", order = 2)]

    public float health = 1;
    public float maxHealth = 1;
    public float speed = 1;
    public float stamina = 1;
    public float block = 0;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(GameObject target)
    {
        // target: Fighter prefab

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        // TODO: Animate sprite
    }
}
