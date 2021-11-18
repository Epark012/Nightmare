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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Targeting(target);
    }

    private void Targeting(Transform target)
    {
        Vector3 targetPos = new Vector3(target.position.x, head.position.y, target.position.z);
        head.LookAt(targetPos);
    }

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

    //Trail Renderer for shoot range.
    protected override void ShootVFX(Vector3 targetTransform)
    {
        muzzleFlash.Play();

        var trail = Instantiate(shootTrail, firePoint.position, Quaternion.identity);
        trail.AddPosition(firePoint.position);
        trail.transform.position = targetTransform;
    }
}