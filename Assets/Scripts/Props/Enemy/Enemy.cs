using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{ 
    Idle,
    Wandering,
    Working,
}

public enum MovementState
{
    IsReady,
    Moving,
    Operating
}

/// <summary>
/// A base script for enemies. 
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    #region Enemy Property
    [SerializeField]
    protected float enemyHP;
    [SerializeField]
    protected float radarDistance;
    [SerializeField]
    private bool isScatterable = false;
    public EnemyState state = EnemyState.Idle;
    public MovementState mState = MovementState.IsReady;
    #endregion

    #region Essential Property
    protected Animator animator = null;
    protected AudioSource audioSource = null;
    private Collider coll = null; 
    protected NavMeshAgent agent = null;

    [SerializeField]
    private float rotSpeed = 2f;
    [SerializeField]
    private MeshRenderer meshObject;
    [SerializeField]
    private GameObject broken;
    //[SerializeField]
    //protected MineralManager mineralManager = null;

    protected Cortex cortex = null;
    #endregion

    #region Wandering property
    [Header("Roaming Property")]
    [SerializeField]
    private float wanderRadius = 3.0f;
    [SerializeField]
    private float wanderDistance = 2f;
    [SerializeField]
    private float angleOffset = 90f;
    protected Vector3 wanderTarget = Vector3.zero;
    protected Vector3 targetWorld = Vector3.zero;
    #endregion

    //Test
    public float angle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        //mineralManager = FindObjectOfType<MineralManager>();
        cortex = GetComponentInParent<Cortex>();
    }

    protected virtual void DieAnimation()
    {
        if (animator)
        {
            animator.SetTrigger("Die");
            //Reset HP
            enemyHP = 5;
        }
    }

    protected virtual void Destroyed()
    {
        //When hit by something powerful, such as missile or rocket, it is destoyed not damaged. 
        if (agent != null)
            agent.enabled = false;

        //Check it is scatterable, if not, it does not need destruction Object
        if(isScatterable)
        {
            //Turn off drone mesh
            meshObject.enabled = false;
            //Turn On separate broken drone mesh
            broken.SetActive(true);
        }
        else
        {
            //Out of order
        }

        //Destroy(broken, destructionRemainSeconds);
    }

    protected virtual void Hit()
    {
            
    }

    protected virtual void MoveToTarget(Vector3 target)
    {
        //Check Angle 
         angle = Vector3.Angle(transform.forward, target);

        if(angle > angleOffset)
        {
            //Stop And Rotate
            StartCoroutine(SmoothRotate(target));
        }
        else
        {
            agent.SetDestination(target);
        }

        //Radar sensor effect.
        //Detect something.
        //Store objects.
    }

    private IEnumerator SmoothRotate(Vector3 target)
    {
        agent.isStopped = true;

        Debug.Log("sdfsfdsdffsdsdsfd");
        Quaternion targetRot = Quaternion.LookRotation(target);

        while (Vector3.Angle(transform.forward, target) > 1f)
        {
            Quaternion currentQuaternion = transform.rotation;
            transform.rotation = Quaternion.Slerp(currentQuaternion, targetRot, Time.deltaTime * rotSpeed);
            yield return null;
        }

        agent.SetDestination(target);
        agent.isStopped = false;
    }

    protected void Wander()
    {
        if (mState == MovementState.IsReady)
        {
            mState = MovementState.Moving;

            targetWorld = SetTargetPosition();
            MoveToTarget(targetWorld);
        }

        if (mState == MovementState.Moving)
        {
            CalculateDistance(targetWorld);
        }
    }


    private Vector3 SetTargetPosition()
    {
        Vector3 targetWorld = CalculateTargetPosition();

        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, targetWorld, NavMesh.AllAreas, path);
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            //Need to FIx 
            SetTargetPosition();
        }

        return targetWorld;
    }

    private Vector3 CalculateTargetPosition()
    {
        wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderDistance,
                                       0,
                                       Random.Range(-1f, 1f) * wanderDistance);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);
        return targetWorld;
    }

    private bool CalculateDistance(Vector3 target)
    {
        Vector3 distToTarget = target - transform.position;
        distToTarget.y = 0;
        float dist = distToTarget.magnitude;
        if (dist < agent.stoppingDistance)
        {
            mState = MovementState.IsReady;
            return true;
        }

        else
        {
            mState = MovementState.Moving;
            return false;
        }
    }


    protected virtual void PatrollingState()
    {

    }

    protected virtual void AttackState()
    {

    }
}
