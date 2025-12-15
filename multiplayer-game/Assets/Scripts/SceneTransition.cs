using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;

public class SceneTransition : MonoBehaviour
{
    public GameManager gameManagerScript;
    public GameObject playerSelectionObj1;
    public GameObject playerSelectionObj2;

    public bool sceneCanTranstition;
    public GameObject door;
    public GameObject selectCharacterText;
    public GameObject bossCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneCanTranstition = false;
        door.SetActive(false);
        bossCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayerSelection();
        if (sceneCanTranstition == true)
        {
            door.SetActive(true);
            selectCharacterText.SetActive(false);
        }
    }

    public void CheckForPlayerSelection()
    {
        if (gameManagerScript.player1Selected == true && gameManagerScript.player2Selected == true)
        {
            sceneCanTranstition = true;
        }
    }
}
