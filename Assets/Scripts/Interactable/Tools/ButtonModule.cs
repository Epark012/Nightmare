using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Designed for button based tools.
/// </summary>
public class ButtonModule : XRBaseInteractable
{
    public UnityEvent onPress = null;

    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPressed = false;

    private float previousHeight = 0.0f;
    private XRBaseInteractor hoverInteractor;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHeight = GetLocalYPosition(interactor.transform.position);
    }

    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHeight = 0.0f;

        previousPressed = false;
        SetYPosition(yMax);
    }

    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider coll = GetComponent<Collider>();
        yMin = transform.localPosition.y - (coll.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(hoverInteractor)
        {
            float newhandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHeight - newhandHeight;
            previousHeight = newhandHeight;

            float newPosition = transform.position.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.y;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        bool inPosition = InPosition();

        if(inPosition && inPosition != previousPressed)
        {
            onPress.Invoke();
        }

        previousPressed = inPosition;
    }

    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMax + 0.01f);

        return transform.localPosition.y == inRange;
    }

    public void ButtonPressedTest(string message)
    {
        Debug.Log(message + " is pressed.");
    }
}
