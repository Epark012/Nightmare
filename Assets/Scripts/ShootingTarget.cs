using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : Enemy
{
    /// <summary>
    /// Shooting Target
    /// Count hp
    /// Zero to Fold/Disable
    /// Make a sound
    /// </summary>

    [SerializeField]
    private AudioClip HitSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    protected override void DieAnimation()
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
}
