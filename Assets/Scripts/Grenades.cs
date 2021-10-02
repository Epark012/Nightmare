using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(OffPinGrabInteractable))]
public class Grenades : MonoBehaviour
{
   
    private bool isActivated = false;
    public bool IsActivated { get { return IsActivated; } set { isActivated = value; } }

    [SerializeField]
    private GameObject explosionVFX;
    [SerializeField]
    private AudioClip explosionSFX;


    [SerializeField]
    private float delayTime = 3f;
    [SerializeField]
    private float radius = 3f;
    [SerializeField]
    private float power = 3f;


    private float count;
    private bool hasExploded = false;
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        count = delayTime;
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated && !hasExploded)
        {
            count -= Time.deltaTime;
            if (count <= 0f && !hasExploded)
            {
                Exploded();
                hasExploded = true;
            }
        }
    }
    private void Exploded()
    {
        //VFX
        explosionVFX = Instantiate(explosionVFX, transform.position, explosionVFX.transform.rotation);
        Destroy(explosionVFX, 3f);
        //Sound
        audioSource.PlayOneShot(explosionSFX, 1.0f);
        //Damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var nearByObject in colliders)
        {
            Rigidbody rb = nearByObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, transform.position, radius);
            }
        }
        meshRenderer.enabled = false;
        Destroy(gameObject, delayTime);
    }

}
