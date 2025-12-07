using System.Collections;
using UnityEngine;

public class ScreenTransitions : MonoBehaviour
{
    [Header("Transitions")]

    public Animator screenAnimator;

    //SCREEN ANIMATIONS
    public void StartScreenTransition()
    {
        screenAnimator.SetTrigger("isClosing");
    }

    public void DeathAnimationStart()
    {
        screenAnimator.SetTrigger("gameIsOver");
    }
}
