using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerSelectionObj1;
    public GameObject playerSelectionObj2;

    public GameObject playerSelectionText1;
    public GameObject playerSelectionText2;

    //Delete Object
    public void DeletePlayerSelectionObject1()
    {
        playerSelectionObj1.SetActive(false);
    }

    public void DeletePlayerSelectionObject2()
    {
        playerSelectionObj2.SetActive(false);
    }

    //Toggle On
    public void TogglePlayerSelection1On()
    {
        playerSelectionText1.SetActive(true);
    }
    public void TogglePlayerSelection2On()
    {
        playerSelectionText2.SetActive(true);
    }
    //Toggle Off
    public void TogglePlayerSelection1Off()
    {
        playerSelectionText1.SetActive(false);
    }
    
    public void TogglePlayerSelection2Off()
    {
        playerSelectionText2.SetActive(false);
    }
    
}
