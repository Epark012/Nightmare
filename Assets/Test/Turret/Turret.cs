using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretState
{
    Idle,
    Targeting,
    Shooting
}

public enum TurretOperationState
{
    isWaiting,
    isOperating
}

public class Turret : MonoBehaviour
{
    [Header("Turret Property")]
    [SerializeField]
    protected Transform head = null;
    [SerializeField]
    protected Transform target = null;
    [SerializeField]
    protected Transform firePoint = null;
    [SerializeField]
    protected float shootingDistance = 10f;
    [SerializeField]
    protected float turretDamage = 3f;
    [SerializeField]
    protected float aimingDuration = 3f;
    [SerializeField]
    protected TurretState state = TurretState.Idle;
    [SerializeField]
    protected TurretOperationState oState = TurretOperationState.isWaiting;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Targeting(Transform target)
    {
        if(oState == TurretOperationState.isWaiting)
        {
            StartCoroutine(Aiming(target, aimingDuration));
        }
    }

    private IEnumerator Aiming(Transform target, float duration)
    {
        oState = TurretOperationState.isOperating;

        float timer = 0f;

        Quaternion startingRot = head.transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(target.position - firePoint.transform.position);

        while(timer < duration)
        {
            timer += Time.deltaTime;

            head.transform.rotation = Quaternion.Slerp(startingRot, targetRot, (timer / duration));
            yield return null;
        }
        oState = TurretOperationState.isWaiting;
        state = TurretState.Shooting;
    }

    protected bool IsInRange(Transform from, Transform to, float targetingAngle)
    {
        Vector3 targetDir = to.position - from.position;
        float angleToTarget = Vector3.Angle(from.forward, targetDir);

        if (angleToTarget < targetingAngle)
        {
            return true;
        }
        else
            return false;
    }

    protected virtual void Shooting()
    {
        
    }

    //Trail Renderer for shoot range.
    protected virtual void ShootVFX(Vector3 targetTransform)
    {
        
    }
}
