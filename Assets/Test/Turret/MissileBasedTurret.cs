using System;
using System.Collections;
using UnityEngine;

public class MissileBasedTurret : Turret
{
    [SerializeField]
    private TurretMissile[] missiles = null;

    private int missileIndex = 0;
    void Update()
    {
        switch (state)
        {
            case TurretState.Idle:
                //
                break;
            case TurretState.Targeting:
                //
                Targeting(target);
                break;
            case TurretState.Shooting:
                //
                Fire();
                break;
        }
    }

    public void ShootingAnimation()
    {
        if(missileIndex > missiles.Length - 1)
        {
            //Missile is empty.
        }
        else
        {
            missiles[missileIndex].Launch(target);
            missileIndex++;
        }
    }

    private void Fire()
    {
        if (oState == TurretOperationState.isWaiting && IsInRange(head, target, targetingAngle) && missiles.Length > 0)
        {
            animator.SetTrigger("Fire");
            state = TurretState.Idle;
        }
        else if (!IsInRange(head, target, targetingAngle))
        {
            Targeting(target);
        }
        else
            return;
    }
}
