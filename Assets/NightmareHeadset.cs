using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Grab design is offset interactable
/// 
/// </summary>
public class NightmareHeadset : XRGrabInteractable
{
    private Vector3 interactorPos = Vector3.zero;
    private Quaternion interactorRot = Quaternion.identity;

    private Rigidbody rigid;
    private Collider coll;

    private bool isHold = false;
    public bool IsHold { get { return isHold; } }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if (interactor is Hand hand)
        {
            isHold = true;
            Debug.Log("Hand is picking up" + gameObject.name);
            StoreInteractor(interactor);
            MatchAttachmentPoints(interactor);
        }
    }

    private void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPos = interactor.attachTransform.localPosition;
        interactorRot = interactor.attachTransform.localRotation;
    }

    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if (interactor is Hand hand)
        {
            isHold = false;
        }

        ResetAttachmentPoints(interactor);
        ClearInteractor(interactor);
    }

    private void ResetAttachmentPoints(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = interactorPos;
        interactor.attachTransform.localRotation = interactorRot;
    }

    private void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPos = Vector3.zero;
        interactorRot = Quaternion.identity;
    }
}
