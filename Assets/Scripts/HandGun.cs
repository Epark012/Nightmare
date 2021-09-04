using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;

public class HandGun : MonoBehaviour
{
    [SerializeField]
    private int bullet;

    private Animator animator;
    private Magazine magazine;

    //VFX section
    [SerializeField]
    private VisualEffect fuzzle;

    //Audio Section
    private AudioSource gunPlayer;
    [SerializeField]
    private AudioClip fire;
    [SerializeField]
    private AudioClip outOfBullet;

    //Haptic Section
    [SerializeField]
    private XRController xrController;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        magazine = GetComponentInChildren<Magazine>();
        gunPlayer = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if(bullet > 1)
        {
            animator.SetTrigger("Fire");
        }
        else
        {
            //Out of Bullet
            gunPlayer.PlayOneShot(outOfBullet, 1.0f);
        }
    }

    public void FireAnimation()
    {
        //Decrease a bullet
        bullet--;
        //Audio
        gunPlayer.PlayOneShot(fire, 1.0f);
        //VFX
        fuzzle.Play();
        //Haptic
        xrController.SendHapticImpulse(2.0f, 0.1f);
    }

    public void Reload()
    {
        
    }
}
