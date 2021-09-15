using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RocketSocketInteractor : XRSocketInteractor
{
    protected override void OnHoverEntered(XRBaseInteractable interactable)
    {
        //Turn On Socket Active On
        socketActive = true;
        base.OnHoverEntered(interactable);
    }
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        interactable.GetComponent<MeshRenderer>().enabled = false;
        base.OnSelectEntered(interactable);
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        interactable.GetComponent<MeshRenderer>().enabled = true;
        base.OnSelectExited(interactable);
    }
}
