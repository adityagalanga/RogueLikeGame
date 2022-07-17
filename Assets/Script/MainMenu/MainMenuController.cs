using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public string levelToLoad;
    public GameObject deletePanel;

    public CharacterSelector[] characterToDelete;

    void Start()
    {
        if(PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        foreach(CharacterSelector charac in characterToDelete)
        {
            PlayerPrefs.SetInt(charac.playerToSpawn.name, 0);
        }

        deletePanel.SetActive(false);
    }

    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}
