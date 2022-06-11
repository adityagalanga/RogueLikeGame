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

    private Vector3 moveDirection;
    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        moveDirection.Normalize();
        theRB.velocity = moveDirection * moveSpeed;
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
