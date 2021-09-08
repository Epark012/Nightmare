using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    /// <summary>
    /// Shooting Target
    /// Count hp
    /// Zero to Fold/Disable
    /// Make a sound
    /// </summary>

    private AudioSource audioSource;

    public int hp = 5;
    [SerializeField]
    private AudioClip HitSFX;
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!animator)
            return;
        else
        {
            animator.SetTrigger("Hit");
            //Reset HP
            hp = 5;
        }
    }
}
