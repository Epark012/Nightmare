using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : XRDirectInteractor
{
    [SerializeField]
    private float count = 3f;

    private bool isFlicked = false;
    private XRController controller = null;
    private HandPresence handPresence = null;
    private FlickDetector flickDetector = null;

    [Header("Power Wrist Events")]
    public UnityEvent OnInventoryReady;
    public UnityEvent OnInventoryOpen;
    public UnityEvent OnGrabRelease;

    public bool IsFlicked { get { return isFlicked; } set { isFlicked = value; } }

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<XRController>();
        handPresence = controller.GetComponentInChildren<HandPresence>();
        flickDetector = GetComponent<FlickDetector>();

        onSelectEntered.AddListener(OnGrab);
        onHoverExited.AddListener(OffGrab);
    }

    private void OnGrab(XRBaseInteractable interactable)
    {
        if(handPresence != null)
            handPresence.TogglePhysics(false);
        else
        {
            handPresence = controller.GetComponentInChildren<HandPresence>();
            handPresence?.TogglePhysics(false);
        }
    }
    private void OffGrab(XRBaseInteractable interactable)
    {
        if (handPresence != null)
            handPresence.TogglePhysics(true);
        else
        {
            handPresence = controller.GetComponentInChildren<HandPresence>();
            //handPresence.TogglePhysics(true);
        }
    }

    private Vector3 Getvalue(InputFeatureUsage<Vector3> usage)
    {
        controller.inputDevice.TryGetFeatureValue(usage, out Vector3 value);
        return value;
    }

    private bool GetButtonValue(InputFeatureUsage<bool> buttonPressed)
    {
        controller.inputDevice.TryGetFeatureValue(buttonPressed, out bool isPressed);
        return isPressed;
    }

    public float GetSpeed()
    {
        return Getvalue(CommonUsages.deviceVelocity).magnitude;
    }

    public float GetRotationSpeed()
    {
        return Getvalue(CommonUsages.deviceAngularVelocity).magnitude;
    }

    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractor(updatePhase);

        if(flickDetector != null)
        {
            if(GetButtonValue(CommonUsages.secondaryButton) &&
                GetButtonValue(CommonUsages.triggerButton) &&
                GetButtonValue(CommonUsages.gripButton)) 
            {
                if (!isFlicked)
                {
                    count -= Time.deltaTime;
                    if (count <= 0) //Power Wrist Ready.
                    {
                        OnInventoryReady.Invoke();
                        Debug.Log("Time to open the inventiry");
                        flickDetector.CheckFlick(this);
                    }
                }
            }
            else
            {
                count = 3;
                if(IsFlicked)
                {
                    Debug.Log("Time to close the inventory.");
                    isFlicked = false;
                    OnGrabRelease.Invoke();
                }
            }
        }
    }
}
