using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorTeleportation : MonoBehaviour
{
    public Vector2 floatySpawn = new Vector2(40f, 0f);
    public Vector2 speedySpawn = new Vector2(50f, 0f);
    public bool floatySpawned = false;
    public bool speedySpawned = false;
    public GameObject bossCam;
    public GameObject lobbyCam;

    public bool gameIsNotReady = true;
    public GameObject spiderBoss;
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
            spiderBoss.SetActive(true);
            //gameIsNotReady = false;
            FindFirstObjectByType<Floaty_PlayerControls>().enabled = true;
            FindFirstObjectByType<Speedy_PlayerControlers>().enabled = true;
        }
        // else 
        // {
        //     gameIsNotReady = true;
        // }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Floaty_PlayerControls floaty = col.gameObject.GetComponent<Floaty_PlayerControls>();
        if (floaty != null)
        {
            Debug.Log("floaty teleported");
            floaty.transform.position = floatySpawn;
            floatySpawned = true;
            floaty.enabled = false;
        }

        Speedy_PlayerControlers speedy = col.gameObject.GetComponent<Speedy_PlayerControlers>();
        if (speedy != null)
        {
            Debug.Log("speedy teleported");
            speedy.transform.position = speedySpawn;
            speedySpawned = true;
            speedy.enabled = false;
        }
    }
}
