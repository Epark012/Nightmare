using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designed for receiving user input and playing animations based on the input. 
/// Originally designed for button module and display module, such as gun selection in Shooting Range scene.
/// </summary>
[RequireComponent(typeof(Animator))]
public class ReceiveModule : MonoBehaviour
{
    [Header("Property")]
    [SerializeField]
    private string animationStringMessage = null;

    private Animator baseAnimator = null;
    private bool isExecuting = false;

    public bool IsExecuting { get { return isExecuting; } set { isExecuting = value; } }

    void Start()
    {
        baseAnimator = GetComponent<Animator>();    
    }

    public void ExecuteAction()
    {
        baseAnimator.SetTrigger(animationStringMessage);
    }

}
