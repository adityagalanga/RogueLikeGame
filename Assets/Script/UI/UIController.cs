using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public Text coinText;
    public GameObject DeathScreen;
    public GameObject PauseMenu;

    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadeToBlack;
    public bool fadeOutBlack;

    public string newGamesScene;
    public string mainMenuScene;

    public GameObject MapDisplay;
    public GameObject BigMapText;

    public Image currentGun;
    public Text GunText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
        currentGun.sprite = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunUI;
        GunText.text = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].weaponName;

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a,0f,fadeSpeed*Time.deltaTime));

            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGamesScene);
        if (PlayerController.instance.gameObject != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        if (PlayerController.instance.gameObject != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
