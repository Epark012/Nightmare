using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Designed for Off Pin Grab Interactable, such as Grenades or flash bomb.
/// </summary>
public class OffPinGrabInteractable : XRGrabInteractable, IInventoryStorable
{
    [SerializeField]
    private MeshRenderer pinMesh;

    private XRSocketInteractor socket;
    private Grenades grenades;

    private bool isPinned = false;

    //IInventoryStorable Section
    [SerializeField]
    private bool isInSocket = true;
    public bool IsInSocket { get { return isInSocket; } set { isInSocket = value; } }

    private Rigidbody rigid;
    private Collider coll;
    protected void Start()
    {
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();

        socket = GetComponentInChildren<XRSocketInteractor>();
        grenades = GetComponent<Grenades>();

        //check 
        if (socket.startingSelectedInteractable)
        {
            isPinned = true;
            pinMesh.enabled = true;
        }
        socket.onSelectEntered.AddListener(InPin); 
        socket.onSelectExited.AddListener(OffPin);
    }

    private void InPin(XRBaseInteractable interactable)
    {
        //interactable Mesh Off
        interactable.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OffPin(XRBaseInteractable interactable)
    {
        //Pin Mesh Off
        pinMesh.enabled = false;
        grenades.IsActivated = true;

        //interactable.GetComponent<MeshRenderer>().enabled = true;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isSelected && !isPinned)
            grenades.IsActivated = true;
        base.ProcessInteractable(updatePhase);
    }

    public void PinOff()
    {
        isPinned = false;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        OffInventorySocket();
    }

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
