using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

/// <summary>
/// Scan
/// </summary>
public class Drone : Enemy, IDamageable
{
    [SerializeField]
    private float invokedTime = 30f;
    [SerializeField]
    private float scanRadius = 5f;
    

    private Vector3 target = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        target = mineralManager.GetNextDestinationFromArray().position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                //
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
            agent.SetDestination(target);
            if (Vector3.Distance( new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(target.x, 0f, target.z)) <= agent.stoppingDistance)
            {
                target = mineralManager.GetNextDestinationFromArray().position;
            }
        }
    }


    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP < 1)
        {
            base.Destroyed();
        }
    }
}
