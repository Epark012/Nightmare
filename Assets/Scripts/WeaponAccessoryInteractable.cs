using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Designed for Magazine uses.
/// </summary>
public class WeaponAccessoryInteractable : XRGrabInteractable
{
    private bool isEquipped = false;

    public bool IsEquipped { get { return isEquipped; } set { isEquipped = value; } }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (interactor.tag == "Player" && isEquipped)
            return false;
        return base.IsSelectableBy(interactor);
    }
}
