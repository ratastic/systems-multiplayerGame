using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInformation : MonoBehaviour
{
    private static int PlayerNumber = 1;
    private GameManager gameManager;

    public bool playerIsDead;
    public int PlayerHealthNum;
    public int PlayerAbilityNum;
    public int PlayerMaxHealth = 100;
    public int OrbHealAmount = 3;

    public bool PlayerAssignedCricket = false;
    public bool PlayerAssignedFly = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Log Player Number!
        Debug.Log($"hi i am player {PlayerNumber}");
        PlayerNumber++;

        gameManager = FindFirstObjectByType<GameManager>();

        PlayerHealthNum = PlayerMaxHealth;
    
        PlayerAbilityNum = 0;
    }

    public void ActivateUI()
    {
        if (PlayerNumber == 1)
        {
            //Activate P1 UI
            gameManager.player1UI.SetActive(true);

            //Assign Portrait
            if (PlayerAssignedCricket == true)
            {
                gameManager.AssignProfileSprite(gameManager.player1ProfileSpriteRender, gameManager.Cricket);
            }
            else if (PlayerAssignedFly == true)
            {
                gameManager.AssignProfileSprite(gameManager.player1ProfileSpriteRender, gameManager.Fly);
            }
        }
        else if (PlayerNumber == 2)
        {
            //Activate P2 UI
            gameManager.player2UI.SetActive(true);

            //Assign Portrait
            if (PlayerAssignedCricket == true)
            {
                gameManager.AssignProfileSprite(gameManager.player2ProfileSpriteRender, gameManager.Cricket);
            }
            else if (PlayerAssignedFly == true)
            {
                gameManager.AssignProfileSprite(gameManager.player2ProfileSpriteRender, gameManager.Fly);
            }
        }
    }

    public void UpdateUI()
    {
        if (PlayerNumber == 1)
        {
            //Check if player is dead
            if (playerIsDead == true)
            {
                //Set Death Sprite for Player 1
                gameManager.AssignProfileSprite(gameManager.player1ProfileSpriteRender, gameManager.Skull);
                return;
            }

            //Update Ability Score Player 1
            gameManager.player1AbilityNum = PlayerAbilityNum;
            gameManager.UpdatePlayerAbility1();

            //Update Health Score Player 1
            gameManager.player1HealthNum = PlayerHealthNum;
            gameManager.UpdatePlayerHealth1();

        }
        else if (PlayerNumber == 2)
        {
            if (playerIsDead == true)
            {
                //Set Death Sprite for Player 2
                gameManager.AssignProfileSprite(gameManager.player2ProfileSpriteRender, gameManager.Skull);
                return;
            }

            //Update Ability Score Player 2
            gameManager.player2AbilityNum = PlayerAbilityNum;
            gameManager.UpdatePlayerAbility2();

            //Update Health Score Player 2
            gameManager.player2HealthNum = PlayerHealthNum;
            gameManager.UpdatePlayerHealth2();

        }
    }

    public void ResetAbiliyScore()
    {
        PlayerAbilityNum = 0;
        UpdateUI();

    }

    public void AddAbilityPoint()
    {
        if (PlayerAbilityNum < 3)
        {
            PlayerAbilityNum += 1;
            UpdateUI();
        }
    }

    public void HealPlayer()
    {
        if (PlayerHealthNum < PlayerMaxHealth)
        {
            PlayerHealthNum += Mathf.Min(OrbHealAmount, PlayerMaxHealth - PlayerHealthNum);
            UpdateUI();
        }
    }

    public void HurtPlayer(int damageDealt)
    {
        PlayerHealthNum -= damageDealt;
        Debug.Log(PlayerHealthNum);
        UpdateUI();
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (PlayerHealthNum <= 0)
        {
            KillPlayer();
            Debug.Log("player is dead");
        }
    }

    public void KillPlayer()
    {
        playerIsDead = true;
        UpdateUI();
    }


}
