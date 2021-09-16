using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSocketInteractor : XRSocketInteractor
{
    public virtual void ReleaseFire()
    {

    }

    protected virtual bool IsCompatiable(XRBaseInteractable interactable)
    {
        return true;
    }
}
