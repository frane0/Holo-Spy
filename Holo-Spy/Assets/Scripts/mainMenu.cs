using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject main, start, restart, story, pause;
    public bool isPaused=false;
    public void StartGame()
    {
        main.SetActive(false);
        if(PlayerPrefs.GetInt("save")==1)
            start.SetActive(true);
        else
            story.SetActive(true);
    }
    public void Load()
    {
        SceneManager.LoadScene(1);
    }
    public void Restart()
    {
        start.SetActive(false);
        restart.SetActive(true);
    }
    public void RestartDecline()
    {
        start.SetActive(true);
        restart.SetActive(false);
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        restart.SetActive(false);
        story.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& SceneManager.GetActiveScene()==SceneManager.GetSceneByName("Level"))
        {
            pause.SetActive(!pause.activeInHierarchy);
            Cursor.visible=!Cursor.visible;
            isPaused = !isPaused;
        }
    }

    public void Continue()
    {
        pause.SetActive(false);
        Cursor.visible = !Cursor.visible;
        isPaused = !isPaused;
    }
}
