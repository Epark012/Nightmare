using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{ 
    Idle,
    BadlyDamaged,
    DataDigging,
    Patrolling,
    Attacking
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
    protected int enemyHP;
    [SerializeField]
    private float radarDistance;
    public EnemyState state;
    #endregion

    #region Essential Property
    protected Animator animator;
    protected AudioSource audioSource;
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Die()
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
