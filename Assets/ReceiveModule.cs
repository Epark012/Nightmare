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
    private Animator baseAnimator = null;

    void Start()
    {
        baseAnimator = GetComponent<Animator>();    
    }
}
