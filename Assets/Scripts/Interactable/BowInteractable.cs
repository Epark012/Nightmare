using UnityEngine.XR.Interaction.Toolkit;

public class BowInteractable : XRGrabInteractable
{
    private NotchSocketInteractor notch = null;

    protected override void Awake()
    {
        base.Awake();
        notch = GetComponentInChildren<NotchSocketInteractor>();
    }

    private void OnEnable()
    {
        //onSelectEntered.AddListener(notch.SetReady);
        //onSelectExited.AddListener(notch.SetReady);
    }

    private void OnDisable()
    {
        //onSelectEntered.RemoveListener(notch.SetReady);
        //onSelectExited.RemoveListener(notch.SetReady);
    }
}
