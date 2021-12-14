using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BasePropSocketInteractor : XRSocketInteractor
{
    [SerializeField]
    private GameObject[] dreamReceiver;


    public override bool CanSelect(XRBaseInteractable interactable)
    {
        DreamObject dream = interactable.GetComponent<DreamObject>();

        return base.CanSelect(interactable) && (dream != null);
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);

        Debug.Log("OnSelectEntered!!!!!!!!!!!!");

        foreach (var s in dreamReceiver)
        {
            IDreamReceiver v = s.GetComponent<IDreamReceiver>();
            if (v != null)
                v.DataReceived();
        }
    }

    public void Testing()
    {
        Debug.Log("safsd;fkljhwefsfswefewsfeswfewfw");
    }
}
