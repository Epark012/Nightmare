using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PullerInteractable))]
public class NotchSocketInteractor : XRSocketInteractor
{
    [Range(0, 1)] public float releaseThreshold = 0.25f;

    public PullerInteractable pullMeasurer { get; private set; } = null;
    public bool IsReady { get; private set; }

    private NightmareInteractionManager customManager => interactionManager as NightmareInteractionManager;

    protected override void Awake()
    {
        base.Awake();
        pullMeasurer = GetComponent<PullerInteractable>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        pullMeasurer.onSelectExited.AddListener(ReleaseArrow);
        pullMeasurer.Pulled.AddListener(MoveAttach);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        pullMeasurer.onSelectExited.RemoveListener(ReleaseArrow);
        pullMeasurer.Pulled.RemoveListener(MoveAttach);
    }

    private void ReleaseArrow(XRBaseInteractor interactor)
    {
        if (selectTarget is ArrowInteractable  && pullMeasurer.PullAmount > releaseThreshold)
            customManager.ForceDeselect(this);
    }

    public void MoveAttach(Vector3 pullPosition, float pullAmount)
    {
        attachTransform.position = pullPosition;
    }

    public void SetReady(XRBaseInteractor interactor)
    {
        IsReady = selectTarget.isSelected;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && CanHover(interactable) && IsArrow(interactable);
    }

    private bool IsArrow(XRBaseInteractable interactable)
    {
        return interactable is ArrowInteractable;
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    public override bool requireSelectExclusive => false;
}
