using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public static bool paused = false;
    public RelicIcons icons;
    public RelicPanel panel;
    public bool showIcons = false;
    private Scene curScene;

    void Start()
    {
        PauseUI.SetActive(false);
        OptionsUI.SetActive(false);
        icons = GameObject.Find("RelicPanel").GetComponent<RelicIcons>();
        panel = GameObject.Find("RelicPanel").GetComponent<RelicPanel>();

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

            curScene = SceneManager.GetActiveScene();
            GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>().text = curScene.name;

            if(showIcons == true){icons.LoadRelicIcons(); showIcons = false;}
            foreach (GameObject button in panel.rcButtonList) //Disables RCButtons when paused
            {
                button.GetComponent<Button>().interactable = false;
            }
            Time.timeScale = 0;
        } else if (!paused)
        {
            if(showIcons == true){icons.UnLoadIcons(); showIcons = false;}
            foreach (GameObject button in panel.rcButtonList) //Enables RCButtons when paused
            {
                button.GetComponent<Button>().interactable = true;
            }
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //Toggles the pause menu
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
        paused = !paused;
        if(paused == true){paused = false;}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Option Settings
    public void Options()
    {
        if(OptionsUI.active == false)
        {
            OptionsUI.SetActive(true);
            GameObject.Find("VolumeSlider").GetComponent<Slider>().value = GameObject.Find("BGM").GetComponent<AudioSource>().volume;
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