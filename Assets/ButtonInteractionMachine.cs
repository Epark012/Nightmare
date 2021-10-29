using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designed for Gun Station in Shooting Range Scene, working with input module, display module and receive module. 
/// </summary>
public class ButtonInteractionMachine : MonoBehaviour
{
    [Header("Modules")]
    [SerializeField]
    private ButtonModule button = null;
    [SerializeField]
    private DisplayModule display = null;
    [SerializeField]
    private ReceiveModule receive = null;

    private void Start()
    {

    }

    //Change Images according to button interactions.
    public void Next()
    {
        display.NextSprite();
    }

    public void Previous()
    {
        display.PreviousSprite();
    }

    public void Warning()
    {
        //Add Warning Sprite.
        Debug.Log("Animator is playing something.");
    }

    public void Execute()
    {
        if(!receive.IsExecuting)
        {
            receive.ExecuteAction();
        }

        else
        {
            Warning();
        }
    }           
}
