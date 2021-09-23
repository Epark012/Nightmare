using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base script for enemies. 
/// 1. Patroller - Find precious memories.  
/// 2. Tanker - Collecting and Battling with defenders.
/// 3. Boss - The final memory in each dream.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    //Enemy HP.
    //Enemy Vision Radar Sight.

    #region Enemy Property
    [SerializeField]
    protected int enemyHP;
    [SerializeField]
    private float radarDistance;
    #endregion

    protected Animator animator;
    protected AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP < 1)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (!animator)
            return;
        else
        {
            animator.SetTrigger("Hit");
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
