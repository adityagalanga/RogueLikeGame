using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D TheRB;
    public Transform gunfire;
    public Animator anim;
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;

    private float shotCounter;
    private Vector2 moveInput;
    private Camera theCam;

    void Start()
    {
        theCam = Camera.main;
    }

    void Update()
    {
        //move character
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        TheRB.velocity = moveInput * moveSpeed;
        
        
        //rotate character
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunfire.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunfire.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunfire.rotation = Quaternion.Euler(0,0,angle);

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        //shooting
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }
}
