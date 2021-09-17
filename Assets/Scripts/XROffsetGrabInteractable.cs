using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    [SerializeField]
    private OneHandGrabInteractable oneHandGrab;
    // Start is called before the first frame update
    void Start()
    {
        oneHandGrab = GetComponentInParent<OneHandGrabInteractable>();
        //Create attach point
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }

        base.OnSelectEntered(interactor);
    }

    //Determines if this interactable can be selected by interactor
    //Slider, OffsetGrabInteractable has to be activated when first grab is IsSelected True
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return (base.IsSelectableBy(interactor) && oneHandGrab.isSelected);
    }
}