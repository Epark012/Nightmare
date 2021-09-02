using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    [SerializeField]
    private int bullet;

    private Animator animator;
    private Magazine magazine;

    private AudioSource gunPlayer;
    [SerializeField]
    private AudioClip fire;

    [SerializeField]
    private AudioClip outOfBullet;

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
        bullet--;
        gunPlayer.PlayOneShot(fire, 1.0f);
    }

    public void Reload()
    {
        
    }
}
