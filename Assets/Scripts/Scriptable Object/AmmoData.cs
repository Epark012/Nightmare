using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Data", menuName = "Scriptable Object/Ammo Data", order = int.MaxValue)]
public class AmmoData : ScriptableObject
{
    //Initial Ammo
    public int bulletPrefabNumber;

    //Type
    public BulletType bulletType;

    //Range
    [SerializeField]
    private float range;
    public float Range { get { return range;} set { range = value; } }

    //Power
    [SerializeField]
    private int power;
    public int Power { get { return power; } set { power = value; } }

    public GameObject prefab;
    public GameObject[] magazine;

    public AmmoData()
    {
        magazine = new GameObject[bulletPrefabNumber];
    }
}
