using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunSocketInteractor : WeaponSocketInteractor
{
    [SerializeField]
    private BulletType bulletType;

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
       // if (interactable.GetComponent<Magazine>())
       //     BulletCount = interactable.GetComponent<Magazine>().currentBullet;

        //Make ungrabbable
        base.OnSelectEntered(interactable);
        WeaponAccessoryInteractable weaponAccessory = GetComponent<WeaponAccessoryInteractable>();
        if (weaponAccessory)
            weaponAccessory.IsEquipped = true;
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);
    }

    public void MagazineOut()
    {
        //Audio
        //socket active false
        socketActive = false;
        Invoke("SocketActiveOn", 2f);
    }

    private void SocketActiveOn()
    {
        socketActive = true;
    }
}
