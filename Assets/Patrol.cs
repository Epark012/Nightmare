using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : Enemy
{
    private NavMeshAgent agent;

    [SerializeField]
    private Enemy motherShip;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();    
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.BadlyDamaged:
                break;
            case EnemyState.DataDigging:
                break;
            case EnemyState.Patrolling:
                {
                    //Orbit
                    if(motherShip != null)
                    {
                        //transform.RotateAround(motherShip.transform.position, Vector3.up, angle);

                    }
                    //Find other mothership
                    else
                    {

                    }
                }
                break;
            case EnemyState.Attacking:
                break;
            default:
                break;
        }
    }
}
