using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Designed for Magazine uses.
/// </summary>
public class WeaponAccessoryInteractable : XRGrabInteractable, IInventoryStorable
{
    [SerializeField]
    private bool isInSocket = true;
    private bool isEquipped = false;

    public bool IsEquipped { get { return isEquipped; } set { isEquipped = value; } }
    public bool IsInSocket { get { return isInSocket; } set { isInSocket = value; } }

    private Rigidbody rigid;
    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (interactor.tag == "Player" && isEquipped)
            return false;
        return base.IsSelectableBy(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        OffInventorySocket();
    }

    //Check it is in the inventory socket and if it is, reset the physics.
    public void OffInventorySocket()
    {
        if (!isInSocket)
            return;
        else
        {
            Debug.Log("Interface has been called.");
            this.gameObject.transform.parent = null;
            if (rigid != null)
                rigid.useGravity = true;
            if (coll != null)
                coll.isTrigger = false;
            isInSocket = false;
        }
    }
}
