using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;
    public bool openWhenEnemiesCleared;

    public List<GameObject> enemies = new List<GameObject>();
    public GameObject[] doors;

    private bool roomActive;

    private void Update()
    {
        if(enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for(int i =0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        if(enemies.Count == 0)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
            closeWhenEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            if (closeWhenEntered)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            roomActive = false;
        }
    }
}
