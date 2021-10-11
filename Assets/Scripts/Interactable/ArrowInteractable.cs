using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowInteractable : XRGrabInteractable
{
    [Header("Settings")]
    public float speed = 2000.0f;
    
    [Header("Hit")]
    public Transform tip = null;
    public LayerMask layerMask = ~Physics.IgnoreRaycastLayer;

    private Collider coll = null;
    private Rigidbody rigid = null;

    private Vector3 lastPosition = Vector3.zero;
    private bool launched = false;


    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
            Clear();
        base.OnSelectEntering(interactor);
    }

    private void Clear()
    {
        SetLaunch(false);
        TogglePhysics(true);
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if (interactor is NotchSocketInteractor notch)
            Launched(notch);
    }
    private void Launched(NotchSocketInteractor notch)
    {
        if(notch.IsReady)
        {
            SetLaunch(true);
            UpdateLastPosition();
            ApplyForce(notch.pullMeasurer);
        }
    }
    private void SetLaunch(bool value)
    {
        launched = value;
        coll.isTrigger = value;
    }

    private void UpdateLastPosition()
    {
        lastPosition = tip.position;
    }

    private void ApplyForce(PullerInteractable pullMeasurer)
    {
        float power = pullMeasurer.PullAmount;
        Vector3 force = transform.forward * (power * speed);
        rigid.AddForce(force);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(launched)
        {
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (CheckForCollision())
                    launched = false;

                UpdateLastPosition();
            }

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
                SetDirection();
        }
    }

    private void SetDirection()
    {
        if (rigid.velocity.z > 0.5f)
            transform.forward = rigid.velocity;
    }

    private bool CheckForCollision()
    {
        if (Physics.Linecast(lastPosition, tip.position, out RaycastHit hit, layerMask))
        {
            TogglePhysics(false);
            ChildArrow(hit);
            CheckForHittable(hit);
        }

        return hit.collider != null;
    }

    private void TogglePhysics(bool usePhysics)
    {
        rigid.isKinematic = !usePhysics;
        rigid.useGravity = usePhysics;
    }

    private void ChildArrow(RaycastHit hit)
    {
        Transform newParent = hit.collider.transform;
        transform.SetParent(newParent);
    }

    
    private void CheckForHittable(RaycastHit hit)
    {
        GameObject hitObject = hit.transform.gameObject;
        IArrowHittable hittable = hitObject ? hitObject.GetComponent<IArrowHittable>() : null;

        if (hittable != null)
            hittable.TakeDamage(this);
    }
}
