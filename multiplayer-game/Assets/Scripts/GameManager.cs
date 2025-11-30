using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerSelectionObj1;
    public GameObject playerSelectionObj2;

    public void DeletePlayerSelectionObject1()
    {
        playerSelectionObj1.SetActive(false);
    }

    public void DeletePlayerSelectionObject2()
    {
        playerSelectionObj2.SetActive(false);
    }
}
