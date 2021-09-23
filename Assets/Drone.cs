using UnityEngine;
using UnityEngine.AI;

public class Drone : Enemy
{
    [SerializeField]
    private NavMeshAgent agent;

    private int index = 0;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GetTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target);
            if(Vector3.Distance(transform.position, target) < agent.stoppingDistance)
            {
                target = GetTargetPosition();
            }
        }
    }

    //Get Target Position
    private Vector3 GetTargetPosition()
    {
        int max = MineralWaypoints.Instance().MineralLists.Count;
        index = Random.Range(0, max);
        return MineralWaypoints.Instance().MineralLists[index].transform.position;
    }
}
