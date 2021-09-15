using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Rocket : MonoBehaviour
{
    public bool isActivated = false;

    [SerializeField]
    private float rocketSpeed = 150f;

    [SerializeField]
    private float power = 30f;

    [SerializeField]
    private float radius = 3f;

    [SerializeField]
    private float count = 3;

    [SerializeField]
    private GameObject explosionVFX;
    [SerializeField]
    private ParticleSystem smokeVFX;
    [SerializeField]
    private AudioClip explosionSFX;

    private Rigidbody rigid;
    private AudioSource audioSource;
    private MeshRenderer mesh;
    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated)
        {
            //Check Smoke and Play
            if (!smokeVFX.isPlaying)
                smokeVFX.Play();
            count -= Time.deltaTime;
            //Explode by Time
            if(!hasExploded && count < 0f)
            {
                Explode();
                hasExploded = true;
            }

            //Moving forward
            rigid.AddForce(transform.forward * rocketSpeed);
        }
    }

    private void Explode()
    {
        Debug.Log("Rocket Test");
        //VFX
        explosionVFX = Instantiate(explosionVFX, transform.position, explosionVFX.transform.rotation);
        Destroy(explosionVFX, 3f);
        GetComponent<BoxCollider>().enabled = false;

        //Sound
        audioSource.PlayOneShot(explosionSFX, 1.0f);

        //Damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var nearByObject in colliders)
        {
            Rigidbody rb = nearByObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, transform.position, radius);
            }
        }

        //Turn off mesh
        mesh.enabled = false;

        //Destroy 
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isActivated)
        {
            Debug.Log("Rocket Hit to " + collision.transform.name);
            Explode();
        }
    }
}
