using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damageToGive = 50;
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject impactEffect;

    void Start()
    {
        
    }

    void Update()
    {
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
        if(collision.tag == "Boss")
        {
            collision.GetComponent<BossController>().TakeDamage(damageToGive);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(4);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
