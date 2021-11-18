using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Property")]
    [SerializeField]
    protected Transform head = null;
    [SerializeField]
    protected Transform target = null;
    [SerializeField]
    protected Transform firePoint = null;
    [SerializeField]
    protected float shootingDistance = 10f;
    [SerializeField]
    protected float turretDamage = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Targeting(target);
    }

    private void Targeting(Transform target)
    {
        Vector3 targetPos = new Vector3(target.position.x, head.position.y, target.position.z);
        head.LookAt(targetPos);
    }

    protected virtual void Shooting()
    {
       
    }

    //Trail Renderer for shoot range.
    protected virtual void ShootVFX(Vector3 targetTransform)
    {
        
    }
}
