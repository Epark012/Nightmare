using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGuard : Enemy, IDamageable
{
    private INightThriller nightThriller = null;


    [Header("Drone Guard Property")]
    [SerializeField]
    private float attackDistance = 0.0f;
    [SerializeField]
    private TrailRenderer shootTrail = null;
    [SerializeField]
    private ParticleSystem muzzleFlash = null;
    [SerializeField]
    private float damage = 3f;
    [SerializeField]
    private float targetingAngle = 15f;
    [SerializeField]
    private Transform[] firePoints = null;

    //Testing Particle System
    [SerializeField]
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                if(cortex.MineralCountInCortex > 0)
                {
                    PatrollingState();
                }
                else
                {
                    state = EnemyState.Wandering;
                }
                //Patrolling Around minerals
                break;
            case EnemyState.Wandering:
                //Wandering
                //agent.SetDestination(testingTarget.transform.position);
                Wander();
                break;
            case EnemyState.Working:
                //ScanningByRay(); 
                Attack(target);
                break;
        }
    }

    private void Attack(Transform target)
    {
        state = EnemyState.Working;
        mState = MovementState.Operating;
        float distToTarget = Vector3.Distance(gameObject.transform.position, target.position);

        Quaternion lookAtTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTarget, rotSpeed * Time.deltaTime);

        if(distToTarget < attackDistance)
        {
            Fire();
        }
        else
        {
            //Move To Near the target Position
            MoveToTarget(target.position);
        }
    }

    private void Fire()
    {
        animator.SetTrigger("Fire");
    }

    #region Animator Section
    public void ShootingAnimation()
    {
        Shooting();
    }

    private void Shooting()
    {
        RaycastHit hit;

        foreach(var firePoint in firePoints)
        {
            if (Physics.Raycast(firePoint.position, target.position - firePoint.position, out hit))
            {
                Debug.Log("Targetting : " + hit.collider.name + " from " + this.gameObject.name);

                INightThriller nightThriller = hit.collider.gameObject.GetComponent<INightThriller>();
                ShootVFX(firePoint.position, hit.point);
                //NightmareBulletImpactManager.Instance.SpawnBulletImpact(hit.point, hit.normal, NightmareBulletImpactManager.Instance.hitImpacts[0]);

                //Test Particle
                Instantiate(particle, hit.point, Quaternion.LookRotation(hit.normal));
                if (nightThriller != null)
                {
                    Debug.Log("targeting NightThriller item : " + hit.transform.name);
                    nightThriller.TakeDamageFromEnemy(damage);
                }
                else
                {
                    Debug.Log("targeting item is not IDamageable : " + hit.transform.name);
                }
            }
        }
    }
    #endregion

    //Trail Renderer for shoot range.
    private void ShootVFX(Vector3 from, Vector3 targetTransform)
    {
        muzzleFlash.Play();

        var trail = Instantiate(shootTrail, from, Quaternion.identity);
        trail.AddPosition(from);
        trail.transform.position = targetTransform;
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        nightThriller = other.GetComponent<INightThriller>();
        if (nightThriller != null)
        {
            target = other.transform;
            Debug.Log(gameObject.name + " has sensored " + other.gameObject.name);
            Attack(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Lost Target Back to Idle
        if(other.gameObject == target)
        {
            target = null;
            state = EnemyState.Idle;
            mState = MovementState.IsReady;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(targetWorld, 0.1f);
    }
}
