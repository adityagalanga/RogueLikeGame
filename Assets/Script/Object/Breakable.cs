using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDropItem;
    public float itemDropPercent;
    public GameObject[] itemsToDrop;

    private void Smash()
    {
        AudioManager.instance.PlaySFX(0);

        //show broken pieces
        int piecesToDrop = Random.Range(1, maxPieces);

        for (int i = 0; i < piecesToDrop; i++)
        {
            int rand = Random.Range(0, brokenPieces.Length);
            Instantiate(brokenPieces[rand], transform.position, transform.rotation);
        }

        //dropItems
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0, 100);
            if (dropChance < itemDropPercent)
            {
                int randomItem = Random.Range(0, itemsToDrop.Length);
                Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(PlayerController.instance.dashCounter > 0)
            {
                Smash();
            }
        }
        else if (collision.tag == "PlayerBullet")
        {
            Smash();
        }
    }
}
