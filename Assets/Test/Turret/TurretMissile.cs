using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class TurretMissile : MonoBehaviour
{
    [SerializeField]
    private float missileSpeed = 0f;
    [SerializeField]
    private float explosionDamage = 100f;
    [SerializeField]
    private float explosionRange = 10f;
    [SerializeField]
    private ParticleSystem explosionVFX;

    private AudioSource audioSource = null;
    private Collider coll = null;
    private Rigidbody rigid = null;
    private bool isOn = false;

    public bool IsOn { get { return isOn; } set { isOn = value; } }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();

        TogglePhysics(false);
    }

    public void Launch(Transform target)
    {
        IsOn = true;
        TogglePhysics(true);
        StartCoroutine(LaunchMissile(target));
    }

    private IEnumerator LaunchMissile(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.2f)
        {
            transform.position += (target.transform.position - transform.position).normalized * missileSpeed * Time.deltaTime * 100;
            transform.LookAt(target.position - transform.position);
            yield return null;
        }
        Explode();
    }

    private void Explode()
    {
        //VFX
        explosionVFX = Instantiate(explosionVFX, transform.position, transform.rotation);

        //Sound
        //audioSource.PlayOneShot(explosionSFX, 1.0f);

        //Damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);

        foreach (var nearByObject in colliders)
        {
            INightThriller target = nearByObject.GetComponent<INightThriller>();
            if (target != null)
                target.TakeDamageFromEnemy(999);

            Rigidbody rb = nearByObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionDamage, transform.position, explosionRange);
            }
        }
        Destroy(gameObject);
    }

    private void TogglePhysics(bool isPhysicOn)
    {
        coll.isTrigger = !isPhysicOn;
        rigid.useGravity = isPhysicOn;
    }
}
