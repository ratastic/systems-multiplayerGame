using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public GameManager gameManagerScript;
    public GameObject playerSelectionObj1;
    public GameObject playerSelectionObj2;

    public bool sceneCanTranstition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneCanTranstition = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayerSelection();
    }

    public void CheckForPlayerSelection()
    {
        if (gameManagerScript.player1Selected == true && gameManagerScript.player2Selected == true)
        {
            sceneCanTranstition = true;
        }
    }
}
