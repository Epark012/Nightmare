using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class TurretMissile : MonoBehaviour
{

    private AudioSource audioSource = null;
    private Collider coll = null;
    private Rigidbody rigid = null;
    private bool isOn = false;

    public bool IsOn { get { return isOn; } set { isOn = value; } }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();

        TogglePhysics(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!IsOn)
        {
            //Activate Missile
            ActivateMissile();
        }
        else
            return;
    }

    private void ActivateMissile()
    {

    }

    private void TogglePhysics(bool isPhysicOn)
    {
        coll.isTrigger = !isPhysicOn;
        rigid.useGravity = isPhysicOn;
    }
}
