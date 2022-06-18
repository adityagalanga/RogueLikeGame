using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(PlayerController.instance.dashCounter > 0)
            {
                int piecesToDrop = Random.Range(1, maxPieces);
                
                for(int i = 0;i<piecesToDrop; i++)
                {
                    int rand = Random.Range(0, brokenPieces.Length);
                    Instantiate(brokenPieces[rand], transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }
}
