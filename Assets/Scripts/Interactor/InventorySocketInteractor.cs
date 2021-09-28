using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

/// <summary>
/// First part is desinged for socket sizing. 
/// Second Part is designed for the inventory logic regarding selecting target mesh/meshes and SocketActive.
/// Third part is designed for CanSelect to select only magazine and weapon.
/// </summary>
public class InventorySocketInteractor : XRSocketInteractor
{
    //Sizing
    [SerializeField]
    private float targetSize = 0.25f;
    [SerializeField]
    private float sizingDuration = 0.25f;

    //Runtime
    private Vector3 originalScale = Vector3.one;
    private Vector3 objectSize = Vector3.zero;

    private bool canSelect = false;

    protected override void OnHoverEntering(XRBaseInteractable interactable)
    {
        base.OnHoverEntering(interactable);

        //if the object is already selected, wrist can grab it
        if (interactable.isSelected)
            canSelect = true;
    }

    protected override void OnHoverExiting(XRBaseInteractable interactable)
    {
        base.OnHoverExiting(interactable);

        if (!selectTarget)
            canSelect = false;
    }

    protected override void OnSelectEntering(XRBaseInteractable interactable)
    {
        base.OnSelectEntering(interactable);
        StoreOriginalSizeScale(interactable);
    }


    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        //Once select has occurred, scale object to size
        base.OnSelectEntered(interactable);
        TweenSizeToSocket(interactable);
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        //Once the user has grabbed the object, scale to original size
        base.OnSelectExited(interactable);
        SetOriginalScale(interactable);
    }
    private void StoreOriginalSizeScale(XRBaseInteractable interactable)
    {
        //Find the object's size
        objectSize = FindObjectSize(interactable.gameObject);
        originalScale = interactable.transform.localScale;
    }

    private Vector3 FindObjectSize(GameObject objectToMeasure)
    {
        Vector3 size = Vector3.one;

        //Assumes the interactable has one mesh on the root;
       /* if (objectToMeasure.TryGetComponent(out MeshFilter meshFilter))
            size = ColliderMeasurer.Instance.Measure(meshFilter.mesh);*/

        return size;
    }

    private void TweenSizeToSocket(XRBaseInteractable interactable)
    {
        //Find the new scale based on the size of the collider, and scale
        Vector3 targetScale = FindTargetScale();

        //Tween to our new scale
        interactable.transform.DOScale(targetScale, sizingDuration);
    }

    private Vector3 FindTargetScale()
    {
        float largestSize = FindLargestSize(objectSize);
        float scaleDifference = targetSize / largestSize;
        return Vector3.one * scaleDifference;
    }

    private float FindLargestSize(Vector3 value)
    {
        float largestSize = Mathf.Max(value.x, value.y);
        largestSize = Math.Max(largestSize, value.z);
        return largestSize;
    }

    private void SetOriginalScale(XRBaseInteractable interactable)
    {
        if(interactable)
        {
            //Restore original size
            interactable.transform.localScale = originalScale;

            //Reset 
            originalScale = Vector3.one;
            objectSize = Vector3.zero;
        }   
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        //move while ignoring physics 
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    public override bool isSelectActive => base.isSelectActive && canSelect;

    private void OnDrawGizmos()
    {
        //Draw the approximate size of the socketed objects.
        Gizmos.DrawWireSphere(transform.position, targetSize * 0.5f);
    }


    //Part 2
    //Power Wrist Section Turn On / Off

    public void InventorySocket(bool isOn)
    {
        //if socket is selecting Mesh / Meshes off.
        if (selectTarget)
        {
            switch (selectTarget.gameObject.tag)
            {
                case "Weapon":
                    //Get mesh info from Weapon and turn off
                    selectTarget.GetComponent<Weapon>().WeaponMeshSetActive(isOn);
                    break;
                case "Magazine":
                    //Get mesh
                    selectTarget.GetComponent<Magazine>().MagazineMeshSetActive(isOn);
                    break;
            }
        }
        else
        {
            //else socket is not selecting
            //socketactive is false
            socketActive = isOn;
        }
    }

    //Part 3
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && DetermineTargetTag(interactable);
    }

    private bool DetermineTargetTag(XRBaseInteractable interactable)
    {
        return (interactable.gameObject.tag == "Weapon") || (interactable.gameObject.tag == "Magazine");
    }
}
