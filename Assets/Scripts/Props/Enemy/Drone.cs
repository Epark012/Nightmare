using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// Scan
/// </summary>
public class Drone : Enemy, IDamageable
{
    [Header("Drone Propery")]
    [SerializeField]
    private float scanningDuration = 10f;
    [SerializeField]
    private float scanRadius = 5f;
    [SerializeField]
    private int id = 0;

    private Vector3 target = Vector3.zero;
    private SphereCollider radar = null;

    public int ID { get { return id; } }

    // Start is called before the first frame update
    void Start()
    {
        //target = cortex.GetNextDestinationFromArray().position;

        radar = GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                //
                break;
            case EnemyState.Wandering:
                //
                Wander();
                break;
            case EnemyState.Working:
                PatrollingState();
                //ScanningByRay(); 
                break;
        }
    }

    protected override void PatrollingState()
    {
        if(agent != null)
        {
            if(mState != MovementState.Operating)
            {
                mState = MovementState.Operating;
                target = cortex.GetPatrolPosition();
                MoveToTarget(target);
            }
            
            if(mState == MovementState.Operating)
            {
                Vector3 distToTarget = target - transform.position;
                distToTarget.y = 0;
                float dist = distToTarget.magnitude;
                if (dist < agent.stoppingDistance)
                {
                    mState = MovementState.IsReady;
                }
            }
        }
    }


    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP < 1)
        {
            base.Destroyed();
            cortex.RemoveFromDictionary(ID);
        }
    }

    //Scanned
    private void OnTriggerEnter(Collider other)
    {
        var mineral = other.GetComponent<Mineral>();
        if(mineral && !mineral.IsScanned)
        {
            //Scanning 
            StartCoroutine(ScanningMineral(mineral));
        }
        else { return;}
    }

    IEnumerator ScanningMineral(Mineral mineral)
    {
        MoveToTarget(mineral.transform.position);
        state = EnemyState.Working;
        mState = MovementState.Operating;
        cortex.AddToDictionary(ID, mineral);

        float timer = 0f;
        //Scanning Process
        while(timer < scanningDuration)
        {
            timer += Time.deltaTime;
            Debug.Log("Scanning!!");
            yield return null;
        }

        mineral.IsScanned = true;
        mState = MovementState.IsReady;
        state = EnemyState.Wandering;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetWorld, 0.1f);
    }
}
