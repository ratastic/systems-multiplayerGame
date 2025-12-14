using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIToggles : MonoBehaviour
{
    public GameObject inGameMenu;
    public bool menuToggled = false;

    public GameObject controlsPanel;
    public bool controlToggled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inGameMenu.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ToggleInGameMenu()
    {
        Debug.Log("ToggleMenu");

        if (menuToggled == false)
        {
            menuToggled = true;
            inGameMenu.SetActive(true);
        }
        else
        {
            menuToggled = false;
            inGameMenu.SetActive(false);
        }
    }

    public void ToggleControlsMenu()
    {
        if (controlToggled == false)
        {
            controlToggled = true;
            controlsPanel.SetActive(true);
        }
        else
        {
            controlToggled = false;
            controlsPanel.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
