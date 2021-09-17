using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSocketInteractor : XRSocketInteractor
{
    private MeshRenderer interactableMesh;
    public virtual void ReleaseFire()
    {

    }

    protected virtual bool IsCompatiable(XRBaseInteractable interactable)
    {
        return true;
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        //Interactable Mesh and Turn off
        interactableMesh = interactable.GetComponent<MeshRenderer>();
        interactableMesh.enabled = false;

        base.OnSelectEntered(interactable);
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        if(interactable && !interactableMesh)
            interactableMesh.enabled = true;
        interactableMesh = null;
        base.OnSelectExited(interactable);
    }
}
