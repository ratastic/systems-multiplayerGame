using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectOrbs : MonoBehaviour
{
    private int orbsCollected = 0; 
    public int orbsNeeded = 3;
    private bool canUseAbility;

    public PlayerInformation playerInformation;

    [Header("HEAL COLOR")]
    public Color healColor;
    public float healFlashDuration = .2f;

    private SpriteRenderer sr;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canUseAbility = false;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAbility()
    {
        if (canUseAbility == true)
        {
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
        StartCoroutine(HealFlash());
        Debug.Log("ability used and canUseAbility set to false again");
        orbsCollected = 0;
        canUseAbility = false;
        playerInformation.ResetAbiliyScore();
        playerInformation.HealPlayer();
    }

    private IEnumerator HealFlash()
    {
        sr.color = healColor;
        yield return new WaitForSeconds(healFlashDuration);
        sr.color = originalColor;
    }
}
