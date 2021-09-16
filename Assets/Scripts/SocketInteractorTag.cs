using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractorTag : WeaponSocketInteractor
{
    [SerializeField]
    private BulletType bulletType;

    private int bulletCount;

    public int BulletCount { get { return bulletCount; } set { bulletCount = value; } }

    protected override bool IsCompatiable(XRBaseInteractable interactable)
    {
        if (interactable.GetComponent<Magazine>())
        {
            return ((int)bulletType == (int)interactable.GetComponent<Magazine>().GetBulletType());
        }

        else
            return false;
    }
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && IsCompatiable(interactable);
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        //check magazine Bullet
        if (interactable.GetComponent<Magazine>())
            BulletCount = interactable.GetComponent<Magazine>().currentBullet;

        //Interactable Mesh Off
        interactable.GetComponent<MeshRenderer>().enabled = false;
        
        base.OnSelectEntered(interactable);
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        //Update Magazine Bullet Count
        //if(!interactable.GetComponent<Magazine>())
        //{
        //    interactable.GetComponent<Magazine>().currentBullet = BulletCount;
        //}
        //Bullet Count 0
        //BulletCount = 0;
        //Interactable Mesh On
        if (interactable)
            interactable.GetComponent<MeshRenderer>().enabled = true;
        
        base.OnSelectExited(interactable);
    }

    public void MagazineOut()
    {

    }
}
