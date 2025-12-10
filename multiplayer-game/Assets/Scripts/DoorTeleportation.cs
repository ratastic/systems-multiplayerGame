using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorTeleportation : MonoBehaviour
{
    public Vector2 floatySpawn = new Vector2(40f, 0f);
    public Vector2 speedySpawn = new Vector2(50f, 0f);
    private bool floatySpawned = false;
    private bool speedySpawned = false;
    public GameObject bossCam;
    public GameObject lobbyCam;

    public bool gameIsNotReady = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (floatySpawned == true && speedySpawned == true)
        {
            Debug.Log("both players in new scene!!!");
            bossCam.SetActive(true);
            lobbyCam.SetActive(false);
            gameIsNotReady = false;
        }
        else 
        {
            gameIsNotReady = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Floaty_PlayerControls floaty = col.gameObject.GetComponent<Floaty_PlayerControls>();
        if (floaty != null)
        {
            Debug.Log("floaty teleported");
            floaty.transform.position = floatySpawn;
            floatySpawned = true;
            // maybe add something to prevent movement for floaty (?) 
        }

        Speedy_PlayerControlers speedy = col.gameObject.GetComponent<Speedy_PlayerControlers>();
        if (speedy != null)
        {
            Debug.Log("speedy teleported");
            speedy.transform.position = speedySpawn;
            speedySpawned = true;
            // maybe add something to prevent movement for speedy (?) 
        }
    }
}
