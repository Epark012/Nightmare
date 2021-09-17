using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;

public class RocketLauncher : Weapon
{
    [Header("Rocket Section")]
    [SerializeField]
    private Transform firePoint;
    private Animator animator;
    public RocketLauncherType rocketLauncherType;

    private int rocketIndex = 0;
    public int RocketIndex { get { return rocketIndex; } set { rocketIndex = value; } }

    #region Gun Logic
    private bool inRocket = false;
    private bool isLoaded = false;
    private WeaponSocketInteractor socket;
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
    private MeshRenderer[] rocketMesh;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        gunAudioPlayer = GetComponent<AudioSource>();
        socket = GetComponentInChildren<WeaponSocketInteractor>();

        //Find XR Base Interactable for haptic
        xRBaseInteractable = GetComponent<XRBaseInteractable>();
        xRBaseInteractable.onSelectEntered.AddListener(OnGrab);
        xRBaseInteractable.onSelectExited.AddListener(OnRelease);
    }

    public void Fire()
    {
        if (isLoaded)
        {
            animator.SetTrigger("Fire");
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
        //OutRocket
        OutRocket();

        //Socket Part
        socket.ReleaseFire();

        //Set off hasActivated at interactable.


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
        if (inRocket)
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
    public void InRocket()
    {
        //Magazine in
        inRocket = true;
        //Is Loaded Yet, have to slide
        isLoaded = false;
        //Static Magazine Mesh to On
        rocketMesh[rocketIndex].gameObject.SetActive(true);
        //Click sound
        gunAudioPlayer.PlayOneShot(inSocket, 1f);
    }

    public void OutRocket()
    {
        //Magazine Out
        inRocket = false;
        //Loaded False Check
        isLoaded = false;
        //Static Magazine Mesh to Off
        rocketMesh[rocketIndex].gameObject.SetActive(false);
        rocketIndex = 0;
    }
}
