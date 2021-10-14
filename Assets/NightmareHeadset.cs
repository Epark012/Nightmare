using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NightmareHeadset : XRGrabInteractable
{
    private Rigidbody rigid;
    private Collider coll;

    private bool isHold = false;
    public bool IsHold { get { return isHold; }  }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }
    public void OnGrab()
    {
        rigid.isKinematic = true;
    }

    public void OffGrab()
    {
        rigid.isKinematic = false;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        Debug.Log("sdfddsfsdafsdfdsf");

        if(interactor is Hand hand)
        {
            Debug.Log("sdafsdfdsf");
            isHold = true;
        }
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
       
        if (interactor is Hand hand)
        {
            isHold = false;
        }
    }
}
