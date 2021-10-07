using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareHeadset : MonoBehaviour
{
    private Rigidbody rigid;
    private Collider coll;

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
}
