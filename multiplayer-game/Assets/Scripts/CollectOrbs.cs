using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectOrbs : MonoBehaviour
{
    private int orbsCollected = 0; 
    public int orbsNeeded = 3;
    private bool canUseAbility;

    private PlayerInformation playerInformation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canUseAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseAbility == true)
        {
            Debug.Log("can use ability");
            UseAbility();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("orb"))
        {
            Destroy(col.gameObject);
            orbsCollected++;
            Debug.Log("orb collected");
            playerInformation.AddAbilityPoint();

            if (orbsCollected >= orbsNeeded)
            {
                canUseAbility = true;
            }
        }
    }

    private void UseAbility()
    {
        if (Input.GetKeyDown(KeyCode.P)) // change this so that xbox controller reads it 
        {
            Debug.Log("ability used and canUseAbility set to false again");
            orbsCollected = 0;
            canUseAbility = false;
            playerInformation.ResetAbiliyScore();
        }
    }
}
