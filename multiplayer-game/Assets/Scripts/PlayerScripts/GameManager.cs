using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player Selection")]

    public GameObject playerSelectionObj1;
    public GameObject playerSelectionObj2;

    public GameObject playerSelectionText1;
    public GameObject playerSelectionText2;

    [Header("Player UI")]
    public Sprite Cricket;
    public Sprite Fly;
    public Sprite Skull;
    public Sprite deactiveAbilitySprite;

    [Header("Player 1 UI")]

    public GameObject player1UI;
    public Image player1ProfileSpriteRender;

    public GameObject[] player1HealthUI;
    public int player1HealthNum;

    public Image[] player1AbilitySpriteRender;
    public Sprite tealActiveAbilitySprite;
    public int player1AbilityNum;

    [Header("Player 2 UI")]

    public GameObject player2UI;
    public Image player2ProfileSpriteRender;

    public GameObject[] player2HealthUI;
    public int player2HealthNum;

    public Image[] player2AbilitySpriteRender;
    public Sprite redActiveAbilitySprite;
    public int player2AbilityNum;

    public bool player1Selected = false;
    public bool player2Selected = false;

    //PLAYER UI

    public void AssignProfileSprite(Image playerRenderer, Sprite playerSprite)
    {
        playerRenderer.sprite = playerSprite;
    }

    public void UpdatePlayerHealth1()
    {
        for (int i = 0; i < player1HealthUI.Length; i++)
        {
            //Hurt Player
            if (i > player1HealthNum/2)
            {
                player1HealthUI[i].SetActive(false); //Set False

            }

            //Heal Player
            if (i < player1HealthNum/2)
            {
                player1HealthUI[i].SetActive(true);

            }
        }

    }
    public void UpdatePlayerAbility1()
    {
        for (int i = 0; i < player1AbilitySpriteRender.Length; i++)
        {
            // Decrease Player ability
            if (i > player1AbilityNum)
            {
                if (player1AbilitySpriteRender[i] != null) // Check if its active
                {
                    player1AbilitySpriteRender[i].sprite = deactiveAbilitySprite; //Toggle to active
                }
            }

            // Increase Player Ability
            if (i < player1AbilityNum)
            {
                if (player1AbilitySpriteRender[i] != null) // Check if its active
                {
                    player1AbilitySpriteRender[i].sprite = tealActiveAbilitySprite; //Toggle to active
                }
            }
        }

    }

    public void UpdatePlayerHealth2()
    {
            for (int i = 0; i < player1AbilitySpriteRender.Length; i++)
        {
            //Hurt Player
            if (i > player2HealthNum/2)
            {
                player2HealthUI[i].SetActive(false);

            }

            //Heal Player
            if (i < player2HealthNum/2)
            {
                player2HealthUI[i].SetActive(true);

            }
        }

    }
    public void UpdatePlayerAbility2()
    {
        for (int i = 0; i < player2AbilitySpriteRender.Length; i++)
        {
            // Decrease Player Ability
            if (i > player2AbilityNum)
            {
                if (player2AbilitySpriteRender[i] != null) // Check if its active
                {
                    player2AbilitySpriteRender[i].sprite = deactiveAbilitySprite; //Toggle to active
                }
            }

            // Increase Player Ability
            if (i < player2AbilityNum)
            {
                if (player2AbilitySpriteRender[i] != null) // Check if its active
                {
                    player2AbilitySpriteRender[i].sprite = redActiveAbilitySprite; //Toggle to active
                }
            }
        }

    }

    //PLAYER SELECTION

    //Delete Object
    public void DeletePlayerSelectionObject1()
    {
        playerSelectionObj1.SetActive(false);
        player1Selected = true;
    }

    public void DeletePlayerSelectionObject2()
    {
        playerSelectionObj2.SetActive(false);
        player2Selected = true;
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
