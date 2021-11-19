using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Designed for Gun Based Turret.
/// Shooting - Animation
/// </summary>
public class GunBasedTurret : Turret
{
    [Header("Gun Based Propery")]
    [SerializeField]
    private TrailRenderer shootTrail = null;
    [SerializeField]
    private ParticleSystem muzzleFlash = null;
    [SerializeField]
    private float targetingAngle = 15f;

    //Testing Particle System
    [SerializeField]
    private ParticleSystem particle;

    private Collider[] colls;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        colls = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TurretState.Idle:
                //
                break;
            case TurretState.Targeting:
                //
                Targeting(target);
                break;
            case TurretState.Shooting:
                //
                Fire();
                break;
        }
    }

    protected override void Targeting(Transform target)
    {
        base.Targeting(target);
    }

    private void Fire()
    {

        if (oState == TurretOperationState.isWaiting && IsInRange(head,target,targetingAngle))
        {
            animator.SetTrigger("Fire");
        }
        else if (!IsInRange(head, target, targetingAngle))
        {
            Targeting(target);
        }
        else
            return;
    }


    #region Animator Section
    public void ShootingAnimation()
    {
        Shooting();
    }

    protected override void Shooting()
    {
        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, target.position - firePoint.position, out hit, shootingDistance))
        {
            Debug.Log("Targetting : " + hit.collider.name + " from " + this.gameObject.name);

            IDamageable enemy = hit.collider.gameObject.GetComponent<IDamageable>();
            ShootVFX(hit.point);
            //NightmareBulletImpactManager.Instance.SpawnBulletImpact(hit.point, hit.normal, NightmareBulletImpactManager.Instance.hitImpacts[0]);

            //Test Particle
            Instantiate(particle, hit.point, Quaternion.LookRotation(hit.normal));
            if (enemy != null)
            {
                Debug.Log("targeting Idamageable item : " + hit.transform.name);
                enemy.TakeDamage(turretDamage);
            }
            else
            {
                Debug.Log("targeting item is not IDamageable : " + hit.transform.name);
            }
        }
    }
    #endregion

    //Trail Renderer for shoot range.
    protected override void ShootVFX(Vector3 targetTransform)
    {
        muzzleFlash.Play();

        var trail = Instantiate(shootTrail, firePoint.position, Quaternion.identity);
        trail.AddPosition(firePoint.position);
        trail.transform.position = targetTransform;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detecting Target
    }

    private void OnTriggerExit(Collider other)
    {
        //Lost Target
    }
}