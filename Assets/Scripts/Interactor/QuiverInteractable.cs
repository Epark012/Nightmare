using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class QuiverInteractable : XRBaseInteractable
{
    public GameObject arrowPrefab = null;


    private void Test(string message)
    {
        Debug.Log(message + " is passed.");
    }


    protected override void OnHoverExiting(XRBaseInteractor interactor)
    {
        base.OnHoverExiting(interactor);
        if (interactor.selectTarget != null)
        {
            CreateAndSelectArrow(interactor);
            Test("Holding and OnHoverExiting.");
        }
    }

    private void CreateAndSelectArrow(XRBaseInteractor interactor)
    {
        ArrowInteractable arrow = CreateArrow(interactor.transform);
        interactionManager.ForceSelect(interactor, arrow);
    }

    private ArrowInteractable CreateArrow(Transform interactorPos)
    {
        GameObject arrowObject = Instantiate(arrowPrefab, interactorPos.position,  interactorPos.localRotation);
        return arrowObject.GetComponent<ArrowInteractable>();
    }
}
