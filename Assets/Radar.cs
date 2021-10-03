using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour, IDamageable
{
    private Rigidbody rigid;
    private Collider coll;
    public void TakeDamage(int damage)
    {
        //reset physics
        //Make parent null - gravity, iskinematic
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
