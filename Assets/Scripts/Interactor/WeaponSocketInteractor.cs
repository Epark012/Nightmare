using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSocketInteractor : XRSocketInteractor
{
    private MeshRenderer interactableMesh;
    protected int bulletCount;
    private Weapon weapon;
    private Magazine magazine;
    protected override void Start()
    {
        weapon = GetComponentInParent<Weapon>();
        bulletCount = weapon.Bullet;

        //AddListener Events
        onSelectEntered.AddListener(UpdateBulletCountInMagazine);
        onSelectExited.AddListener(UpdateBulletCountOutMagazine);
    }

    public virtual void ReleaseFire()
    {

    }

    protected virtual bool IsCompatiable(XRBaseInteractable interactable)
    {
        return true;
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        //Interactable Mesh and Turn off
        interactableMesh = interactable.GetComponent<MeshRenderer>();
        interactableMesh.enabled = false;
        base.OnSelectEntered(interactable);
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        if(interactable && !interactableMesh.enabled)
            interactableMesh.enabled = true;
        base.OnSelectExited(interactable);
        interactableMesh = null;
    }

    protected virtual void UpdateBulletCountInMagazine(XRBaseInteractable interactable)
    {
        //when a magazine in - Unity event
        //update current bullet in weapon 
        magazine = interactable.GetComponent<Magazine>();
        if (magazine)
            weapon.Bullet = magazine.currentBullet;
    }


    protected virtual void UpdateBulletCountOutMagazine(XRBaseInteractable interactable)
    {
        if (magazine)
            magazine.currentBullet = weapon.Bullet;
    }
}
