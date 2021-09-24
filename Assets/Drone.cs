using UnityEngine;
using UnityEngine.AI;

public class Drone : Enemy
{
    [SerializeField]
    private MeshRenderer droneMesh;
    [SerializeField]
    private GameObject broken;

    private Rigidbody rigid;
    private NavMeshAgent agent;
    private Vector3 target;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent != null)
        {
            target = MineralWaypoints.Instance().MineralLists[index].transform.position;
            agent.SetDestination(target);
            if (Vector3.Distance(transform.position, target) <= agent.stoppingDistance)
            {
                if (index < MineralWaypoints.Instance().MineralLists.Count - 1)
                {
                    index++;
                }
                else
                    index = 0;
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        //Turn off AI 
        if(agent != null)
            agent.enabled = false;
        //Turn on Gravity
        rigid.useGravity = true;

        //Turn off drone mesh
        droneMesh.enabled = false;
        //Turn On separate broken drone mesh
        broken.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit by OnCollsionEnter");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit by OnTriggerEnter");
        }
    }
}
