using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIToggles : MonoBehaviour
{
    public GameObject controlsPanel;
    public bool controlToggled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controlsPanel.SetActive(false);
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
        SceneManager.LoadScene(1);
    }
}
