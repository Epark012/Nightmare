using UnityEngine.XR.Interaction.Toolkit;

public class NightmareInteractionManager : XRInteractionManager
{
    public void ForceDeselect(XRBaseInteractor interactor)
    {
        if (interactor.selectTarget)
            SelectExit(interactor, interactor.selectTarget);
    }
}
