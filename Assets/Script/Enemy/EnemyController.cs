using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    public Animator anim;
    public int health = 150;

    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public float shootRange;

    public SpriteRenderer theBody;

    private float fireCounter;
    private Vector3 moveDirection;
    void Start()
    {
        
    }

    void Update()
    {
        if (theBody.isVisible)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();
            theRB.velocity = moveDirection * moveSpeed;

            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }

            }
        }


        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        Instantiate(hitEffect, transform.position, transform.rotation);
        if (health <= 0)
        {
            Destroy(gameObject);
            int random = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[random], transform.position, Quaternion.Euler(0,0f,rotation * 90f));
        }
    }
}
