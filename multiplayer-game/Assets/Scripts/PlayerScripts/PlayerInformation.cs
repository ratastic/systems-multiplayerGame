using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    private static int PlayerNumber = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Log Player Number!
        Debug.Log($"hi i am player {PlayerNumber}");
        PlayerNumber++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
