using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;

    public enum TwoHandRotationType { None, First, Second};
    public TwoHandRotationType twoHandRotationType;

    private XRController xRController;
    private Weapon gun;

    bool secondaryButtonPressed;
    bool hasReloaded = false;


    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in secondHandGrabPoints)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }

        gun = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor && selectingInteractor)
        {
            //Compute the rotation
            selectingInteractor.attachTransform.rotation = GetTwoRotation();
        }

        if(xRController)
        {
            xRController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonPressed);
         
            if(secondaryButtonPressed && !hasReloaded)
            {
                gun.Reload();
                hasReloaded = true;
            }
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoRotation()
    {
        Quaternion targetRotation;
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.transform.position - selectingInteractor.attachTransform.position);
        }
        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
        }

        return targetRotation;
                    
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        Debug.Log("Second Hand Grab");
        secondInteractor = interactor;
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        Debug.Log("Second Hand Release");
        secondInteractor = null;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log("FIrst Grab Enter");

        xRController = interactor.GetComponent<XRController>();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("First Grab Enter");
        base.OnSelectExited(interactor);
        xRController = null;
        secondInteractor = null;
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        Debug.Log("selectingInteractor : " + selectingInteractor);
        Debug.Log("interactor : " + interactor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
