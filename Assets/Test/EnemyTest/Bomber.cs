using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy, IDamageable
{
    [Header("Bomber Property")]
    [SerializeField]
    private float explodeTimer = 3f;
    [SerializeField]
    private float explosionDamage = 100f;
    [SerializeField]
    private float explosionRange = 10f;
    [SerializeField]
    private ParticleSystem explosionVFX;

    private bool isActivated = false;

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
                if (cortex.MineralCountInCortex > 0)
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
                Attack();
                break;
        }
    }

    //Attack Target
    private void Attack()
    {
        if(target != null)
        {
            state = EnemyState.Wandering;
        }
        else
        {
            Vector3 targetPos = target.position;
            targetPos.y = 0;

            float distToTarget = Vector3.Distance(transform.position, targetPos);
            if(distToTarget > (agent.stoppingDistance * 5))
            {
                mState = MovementState.Moving;
                if(mState == MovementState.Moving && !isActivated)
                {
                    //MoveTo Target
                    agent.SetDestination(target.position);
                }
            }
            else if (mState == MovementState.Moving)
            {
                mState = MovementState.IsReady;
                Debug.Log("In Position!");
                //Set Timer To Explode
                //Explode
                if(mState == MovementState.IsReady && !isActivated)
                {
                    isActivated = true;
                    mState = MovementState.Operating;
                    StartCoroutine(CountDownToExplode(explodeTimer));
                }
            }
        }
    }

    private void DetectTarget(Transform detectedTarget)
    {
        if (target)
            return;
        else
        {
            state = EnemyState.Working;
            target = detectedTarget;
        }
    }

    IEnumerator CountDownToExplode(float explodeTimer)
    {
        Debug.Log("Start Count Down");
        yield return new WaitForSeconds(explodeTimer);
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

        //Turn off mesh
        //mesh.enabled = false;

        //Destroy 
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var detectedTarget = other.GetComponent<INightThriller>();
        if(detectedTarget != null && target == null)
        {
            DetectTarget(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var nightThriller = other.GetComponent<INightThriller>();
        if(nightThriller == null)
        {
            return;
        }
        else
        {
            if(target.gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
            {
                Debug.Log("Equallll");
                target = null;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetWorld, 0.1f);
    }
}
