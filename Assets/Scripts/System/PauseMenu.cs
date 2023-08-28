using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public static bool paused = false;
    public static float playerSetVolume = 1f;

    void Start()
    {
        PauseUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //Closes the pause menu
    public void Resume()
    {
        paused = !paused;
    }
    //Exits the Game
    public void Title()
    {
        SceneManager.LoadScene(0);
    }
    //Reloads the Current Level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Loads the Menu
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
}