using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : XRDirectInteractor
{
    private XRController controller = null;
    private FlickDetector flickDetector;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<XRController>();
        flickDetector = GetComponent<FlickDetector>();
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

        if(GetButtonValue(CommonUsages.secondaryButton) &&
            GetButtonValue(CommonUsages.triggerButton) &&
            GetButtonValue(CommonUsages.gripButton))
        {
            Debug.Log("Grab Test.");
            flickDetector.CheckFlick(this);
        }
    }
}
