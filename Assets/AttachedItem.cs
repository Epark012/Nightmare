using UnityEngine;

/// <summary>
/// This script is for attached items on enemy, such as Radar or Camera.
/// </summary>
public class AttachedItem : MonoBehaviour
{
    private Enemy main;
    private Rigidbody rigid;

    private void Start()
    {
        main = GetComponentInParent<Enemy>();
        rigid = GetComponent<Rigidbody>();
    }

    //Being Hit
    public void RadarDamaged()
    {
        main.RadarAttached = false;
        //Make it detached from robot body.
        rigid.isKinematic = false;
        transform.parent = null;
    }
}
