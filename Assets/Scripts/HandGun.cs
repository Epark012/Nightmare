using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;

public class HandGun : Weapon
{
    [Header("Ammo Section")]
    public BulletType bulletType;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int bulletDamage = 1;
    [SerializeField]
    private Transform firePoint;
    private Animator animator;

    #region Gun Logic
    private AmmoManager ammoManager;
    private bool inMagazine = false;
    private bool isLoaded = false;
    private GunSocketInteractor socket;
    private OnTargetReached slider;
    #endregion

    #region VFX Property
    [Header("VFX")]
    [SerializeField]
    private VisualEffect fuzzle;
    #endregion

    #region Audio Propertyn
    private AudioSource gunAudioPlayer;
    [Header("Audio Section")]
    [SerializeField]
    private AudioClip fire;
    [SerializeField]
    private AudioClip outOfBullet;
    [SerializeField]
    private AudioClip slide;
    [SerializeField]
    private AudioClip inSocket;
    #endregion

    #region XR Controller
    private XRController xrController;
    private XRBaseInteractable xRBaseInteractable;
    private XRBaseInteractor interactor;
    #endregion

    //Magazine Mesh Section
    [SerializeField]
    private MeshRenderer magazineMesh;

    [SerializeField]
    private int rayDistance;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        gunAudioPlayer = GetComponent<AudioSource>();
        ammoManager = FindObjectOfType<AmmoManager>();
        socket = GetComponentInChildren<GunSocketInteractor>();
        slider = GetComponentInChildren<OnTargetReached>();

        //Find XR Base Interactable for haptic
        xRBaseInteractable = GetComponent<XRBaseInteractable>();
        xRBaseInteractable.onSelectEntered.AddListener(OnGrab);
        xRBaseInteractable.onSelectExited.AddListener(OnRelease);

        //Add Socket Event
        socket.onSelectEntered.AddListener(InMagazine);
        socket.onSelectExited.AddListener(OutMagazine);

        //Check Initial Loaded
        if (CheckInitialLoaded())
        {
            inMagazine = true;
            isLoaded = true;
        }
    }


    private bool CheckInitialLoaded()
    {
        return socket.startingSelectedInteractable;
    }

    public void Fire()
    {
        if(Bullet >= 1 && isLoaded)
        {
            animator.SetTrigger("Fire");
            if (Bullet == 0)
                slider.SliderLoaded = false;
        }
        else
        {
            //Out of Bullet
            gunAudioPlayer.PlayOneShot(outOfBullet, 1.0f);
            isLoaded = false;
        }
    }

    public void FireAnimation()
    {
        //Decrease a bullet
        Bullet--;
        //Fire Bullet
        ammoManager.FireBullet(firePoint.position, firePoint.rotation, bulletType);

        //Raycast
        RaycastHit ray;
        if(Physics.Raycast(firePoint.position, firePoint.transform.TransformDirection(Vector3.forward), out ray, rayDistance))
        {
            if(ray.rigidbody)
            {
                Debug.Log(ray.transform.name);

                Enemy target = ray.transform.GetComponent<Enemy>();
                if (target != null)
                    target.TakeDamage(bulletDamage);

                if (ray.transform.tag == "Radar")
                {
                    ray.transform.GetComponent<AttachedItem>().RadarDamaged();
                    ray.rigidbody.AddForceAtPosition(firePoint.transform.TransformDirection(Vector3.forward) * 200 * bulletDamage, ray.point);
                }

            }
        }

        //Audio
        gunAudioPlayer.PlayOneShot(fire, 1.0f);
        //VFX
        fuzzle.Play();
        //Haptic
        //xrController.SendHapticImpulse(2.0f, 0.1f);
    }

    public override void Reload()
    {
        //Check Magazine In
        if(inMagazine)
        {
            //Relaod
            isLoaded = true;
            //Slide - Load audio
            gunAudioPlayer.PlayOneShot(slide, 1.0f);
        }
        else
        {
            //Magazine is loaded
            Debug.Log("Magazine is not loaded");
        }
    }
    public void OnGrab(XRBaseInteractor interactor)
    {
        xrController = interactor.GetComponent<XRController>();
    }

    public void OnRelease(XRBaseInteractor interactor)
    {
        xrController = null;
    }
    public override void InMagazine(XRBaseInteractable interactable)
    {
        //Magazine in
        inMagazine = true;
        //Check BulletCounts in the magazine
        //Bullet = socket.BulletCount;
        //Is Loaded Yet, have to slide
        isLoaded = false;
        //Static Magazine Mesh to On
        magazineMesh.enabled = true;
        //Click sound
        gunAudioPlayer.PlayOneShot(inSocket, 1f);
    }

    public override void OutMagazine(XRBaseInteractable interactable)
    {
        //Magazine Out
        inMagazine = false;
        //Loaded False Check
        isLoaded = false;
        //Static Magazine Mesh to Off
        magazineMesh.enabled = false;
        //Update Magazine Current Bullet
        //socket.BulletCount = Bullet;
    }

    //Called by Interactor
    public override void ReleaseMagazine()
    {
        socket.MagazineOut();
    }
}
