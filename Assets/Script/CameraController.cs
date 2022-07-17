using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (!LevelManager.instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (bigMapActive)
                {
                    DeactiveBigMap();
                }
                else
                {
                    ActiveBigMap();
                }
            }
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ActiveBigMap()
    {
        bigMapActive = true;

        bigMapCamera.enabled = true;
        mainCamera.enabled = false;

        PlayerController.instance.canMove = false;
        Time.timeScale = 0;
        UIController.instance.MapDisplay.SetActive(false);
        UIController.instance.BigMapText.SetActive(true);
    }

    public void DeactiveBigMap()
    {
        bigMapActive = false;
        bigMapCamera.enabled = false;
        mainCamera.enabled = true;

        PlayerController.instance.canMove = true;

        Time.timeScale = 1;
        UIController.instance.MapDisplay.SetActive(true);
        UIController.instance.BigMapText.SetActive(false);

    }
}
