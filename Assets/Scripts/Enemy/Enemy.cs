using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{ 
    Idle,
    Working,
}

public enum MovementState
{
    Waiting,
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
    private float radarDistance;
    [SerializeField]
    private bool isScatterable = false;
    public EnemyState state = EnemyState.Idle;
    public MovementState mState = MovementState.Waiting;
    #endregion

    #region Essential Property
    protected Animator animator;
    protected AudioSource audioSource;
    private Collider coll = null; 
    protected NavMeshAgent agent;

    [SerializeField]
    private MeshRenderer meshObject;
    [SerializeField]
    private GameObject broken;
    [SerializeField]
    protected MineralManager mineralManager = null;
    //private float destructionRemainSeconds = 3.0f;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        mineralManager = FindObjectOfType<MineralManager>();
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

    protected virtual void Seek()
    {
        //Radar sensor effect.
        //Detect something.
        //Store objects.
    }

    protected virtual void PatrollingState()
    {

    }

    protected virtual void AttackState()
    {

    }
}
