using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    SIG = 0,
    AR15 = 1,
    UMP45 = 2,
    DessertEagle = 4
}

public enum RocketLauncherType
{
    RPG_7 = 0,
    Test = 1
}

public enum ID
{
    Slot1 = 0,
    Slot2 = 1,
    Slot3 = 2,
    Slot4 = 3
}


public class AmmoManager : MonoBehaviour
{
    public List<AmmoData> ammoDatas = new List<AmmoData>();

    private int sig = 0;
    private int ar = 0;
    private int ump = 0;

    private List<int> indexes;

    private void Awake()
    {
        //Instantiate Transfrom
        for (int i = 0; i < ammoDatas.Count; i++)
        {
            //Set up Magazines located in the scriptable Object of the bullet with the maximum number of prefabs that I input in the scriptable object.
            ammoDatas[i].magazine = new GameObject[ammoDatas[i].bulletPrefabNumber];

            //Make a empty gameobject to contain bullets in the child.
            GameObject ammo = new GameObject();
            ammo.name = ammoDatas[i].name;
            ammo.transform.parent = gameObject.transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InstantiateAmmo();
    }

    private void InstantiateAmmo()
    {
        //Instantiate Ammo 
        for(int i = 0; i < ammoDatas.Count; i++)
        {
            BulletObjectPooling(ammoDatas[i]);
        }
    }

    private void BulletObjectPooling(AmmoData ammo)
    {
        for(int i = 0; i < ammo.bulletPrefabNumber; i++)
        {
            ammo.magazine[i] = Instantiate(ammo.prefab);
            ammo.magazine[i].SetActive(false);
            ammo.magazine[i].gameObject.transform.parent = gameObject.transform.GetChild(ammoDatas.IndexOf(ammo));
        }
    }

    public void FireBullet(Vector3 pos, Quaternion rot, BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.SIG:
                {
                    ammoDatas[0].magazine[sig].transform.position = pos;
                    ammoDatas[0].magazine[sig].transform.rotation = rot;
                    ammoDatas[0].magazine[sig].gameObject.SetActive(true);

                    sig++;

                    if (sig == ammoDatas[0].bulletPrefabNumber)
                        sig = 0;
                }
                break;

            case BulletType.AR15:
                {
                    ammoDatas[1].magazine[ar].transform.position = pos;
                    ammoDatas[1].magazine[ar].transform.rotation = rot;
                    ammoDatas[1].magazine[ar].gameObject.SetActive(true);

                    ar++;

                    if (ar == ammoDatas[1].bulletPrefabNumber)
                        ar = 0;
                }
                break;

            case BulletType.UMP45:
                {
                    ammoDatas[2].magazine[ump].transform.position = pos;
                    ammoDatas[2].magazine[ump].transform.rotation = rot;
                    ammoDatas[2].magazine[ump].gameObject.SetActive(true);

                    ump++;

                    if (ump == ammoDatas[2].bulletPrefabNumber)
                        ump = 0;
                }
                break;
        }
    }
}
