using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    [SerializeField]
    private bool isGrabbed = false;
    [SerializeField]
    private bool isActivated = false;

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
    private MeshRenderer[] meshRenderers = new MeshRenderer[2];
    // Start is called before the first frame update
    void Start()
    {
        count = delayTime;
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated && !isGrabbed)
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

        //Destory
        //Turn off Mesh Renderers in [0] - Mesh for a Grenade, [1] for Pin
        foreach (var mesh in meshRenderers)
        {
            mesh.enabled = false;
        }
        //Destroy Grenades
        Destroy(gameObject, 3f);
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    public void ReleaseGrenades()
    {
        isActivated = true;
    }
}
