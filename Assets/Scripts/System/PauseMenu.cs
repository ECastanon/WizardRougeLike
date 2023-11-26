using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public static bool paused = false;
    private RelicIcons icons;
    private bool showIcons = false;

    void Start()
    {
        PauseUI.SetActive(false);
        OptionsUI.SetActive(false);
        icons = GameObject.Find("RelicPanel").GetComponent<RelicIcons>();

    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Resume();
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            if(showIcons == true){icons.LoadRelicIcons(); showIcons = false;}
            Time.timeScale = 0;
        } else if (!paused)
        {
            if(showIcons == true){icons.UnLoadIcons(); showIcons = false;}
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //Closes the pause menu
    public void Resume()
    {
        OptionsUI.SetActive(false);
        paused = !paused;
        showIcons = !showIcons;
        showIcons = true;
    }
    //Exits the Game
    public void Title()
    {
        OptionsUI.SetActive(false);
        SceneManager.LoadScene(0);
    }
    //Reloads the Current Level
    public void Restart()
    {
        OptionsUI.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().player = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Option Settings
    public void Options()
    {
        if(OptionsUI.active == false)
        {
            Debug.Log("test");
            OptionsUI.SetActive(true);
        } else 
        {
            OptionsUI.SetActive(false);
        }
    }
    //Loads the Menu
    public void MainMenu()
    {
        OptionsUI.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}