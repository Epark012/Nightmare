using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    protected int currentBullet = 1;
    public int Bullet
    {
        get { return currentBullet; }
        set { currentBullet = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void InMagazine(XRBaseInteractable interactable)
    {

    }
    public virtual void OutMagazine(XRBaseInteractable interactable)
    {

    }

    public virtual void Reload()
    {

    }

    public virtual void ReleaseMagazine()
    {

    }
}
