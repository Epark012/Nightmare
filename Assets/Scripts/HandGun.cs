using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;

public enum BulletType
{
    SIG,
    AR15
}

public class HandGun : MonoBehaviour
{
    private AmmoManager ammoManager;
    public BulletType bulletType;

    [SerializeField]
    private int bullet;
    [SerializeField]
    private GameObject bulletPrefab;
    public float lifeTime = 3f;
    [SerializeField]
    private Transform firePoint;

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
        ammoManager = FindObjectOfType<AmmoManager>();
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
        //Fire Bullet
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        ammoManager.FireBullet(firePoint.position, firePoint.rotation, bulletType);
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

    private void Update()
    {
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward, Color.red);
    }
}
