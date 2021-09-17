using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

/// <summary>
/// Desinged for pistols.
/// </summary>
public class OneHandGrabInteractable : XRGrabInteractable
{
    private XRController xRController;
    private Weapon weapon;

    public UnityEvent OnButtonClicked;

    private bool hasReloaded = false;
    private bool secondaryButton;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        OnButtonClicked.AddListener(onButtonClicked);
    }

    //functions to Trigger by onHammerClick
    public void onButtonClicked()
    {
        //ReleaseMagazine in Pistol | OneHandWeapon
        weapon.ReleaseMagazine();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        //Input event
        if (xRController)
        {
            xRController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton);

            if (secondaryButton && !hasReloaded)
            {
                OnButtonClicked.Invoke();
            }
        }
        base.ProcessInteractable(updatePhase);
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        xRController = interactor.GetComponent<XRController>();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        xRController = null;
        base.OnSelectExited(interactor);
    }

}
