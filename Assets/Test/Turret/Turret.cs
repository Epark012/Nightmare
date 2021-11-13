using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform head = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Transform firePoint = null;
    [SerializeField]
    private float shootingDistance = 10f;
    [SerializeField]
    private float turretDamage = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Targeting(target);
        //Shooting();
    }

    private void Targeting(Transform target)
    {
        Vector3 targetPos = new Vector3(target.position.x, head.position.y, target.position.z);
        head.LookAt(targetPos);
    }

    public void ShootingAnimation()
    {
        //shoot raycast
        Shooting();
    }

    private void Shooting()
    {
        RaycastHit hit;

        if(Physics.Raycast(firePoint.position, target.position - firePoint.position, out hit, shootingDistance))
        {
            Debug.Log("Targetting : " + hit.collider.name + " from " + this.gameObject.name);

            IDamageable enemy = hit.collider.gameObject.GetComponent<IDamageable>();
            if(enemy != null)
            {
                Debug.Log("targeting Idamageable item : " + hit.transform.name);
                enemy.TakeDamage(turretDamage);
            }
            else
            {
                Debug.Log("targeting item is not IDamageable : " + hit.transform.name);
            }
        }
    }
}
