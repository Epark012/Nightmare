using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField]
    private int bulletCapacity;
    [SerializeField]
    private BulletType bulletType;

    public int currentBullet;

    public Magazine()
    {
        currentBullet = bulletCapacity;
    }

    public BulletType GetBulletType()
    {
        return bulletType;
    }

    /*
    - Reload - 
    - turn off rigidbidy for dropping
     */
}
