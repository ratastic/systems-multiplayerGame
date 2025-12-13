using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossDamage : MonoBehaviour
{
    [Header("DAMAGE")]
    public int handDamage = 5;
    public int venomDamage = 6;
    public int eggDamage = 4;
    public int spikeDamage = 3;

    private PlayerInformation playerInfo;
    
    [Header("COOLDOWN")]
    public float cooldown = 5f;
    public bool canBeHit;

    [Header("HIT COLOR")]
    public Color flashColor;
    public float flashDuration = .2f;

    private SpriteRenderer sr;
    private Color originalColor;

    private void Start()
    {
        canBeHit = true;
        playerInfo = GetComponent<PlayerInformation>();

        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!canBeHit) return;

        if (col.gameObject.CompareTag("bossHand"))
        {
            playerInfo.HurtPlayer(handDamage);
            StartCoroutine(Cooldown());
        } 
        else if (col.gameObject.CompareTag("venom"))
        {
            playerInfo.HurtPlayer(venomDamage);
            Destroy(col.gameObject);
            StartCoroutine(Cooldown());
        } 
        else if (col.gameObject.CompareTag("egg"))
        {
            playerInfo.HurtPlayer(eggDamage);
            StartCoroutine(Cooldown());
        } 
        else if (col.gameObject.CompareTag("spike"))
        {
            playerInfo.HurtPlayer(spikeDamage);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        canBeHit = false;
        sr.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
        yield return new WaitForSeconds(cooldown - flashDuration);
        canBeHit = true;
    }
}


