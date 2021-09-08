using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private Transform sIGtransform;
    private Transform aRtransform;
    private Transform umpTranform;

    private int sig = 0;
    private int maxSig = 0;
    private int ar = 0;
    private int maxAr = 0;
    private int ump = 0;
    private int maxUmp = 0;

    [SerializeField]
    private int maxSIGAmmo = 5;
    [Tooltip("19mm")]
    [SerializeField]
    private GameObject sIGBullet;
    private GameObject[] sIGMagazine;

    [SerializeField]
    private int maxAR15Ammo = 10;
    [Tooltip("5.56mm")]
    [SerializeField]
    private GameObject ar15Bullet;
    private GameObject[] ar15Magazine;

    [SerializeField]
    private int maxUMP45Ammo = 10;
    [Tooltip("5.56mm")]
    [SerializeField]
    private GameObject ump45Bullet;
    private GameObject[] ump45Magazine;


    private void Awake()
    {
        sIGtransform = gameObject.transform.GetChild(0);
        aRtransform = gameObject.transform.GetChild(1);
        umpTranform = gameObject.transform.GetChild(2);

        sIGMagazine = new GameObject[maxSIGAmmo];
        ar15Magazine = new GameObject[maxAR15Ammo];
        ump45Magazine = new GameObject[maxUMP45Ammo];

        maxSig = sIGMagazine.Length;
        maxAr = ar15Magazine.Length;
        maxUmp = ump45Magazine.Length;
    }
    // Start is called before the first frame update
    void Start()
    {
        InstantiateAmmo();
    }

    private void InstantiateAmmo()
    {
        //SIG
        for (int i = 0; i < maxSIGAmmo; i++)
        {
            sIGMagazine[i] = Instantiate(sIGBullet);
            sIGMagazine[i].SetActive(false);
            sIGMagazine[i].gameObject.transform.parent = sIGtransform;
        }

        //ar 15
        for(int i = 0; i < maxAR15Ammo; i++)
        {
            ar15Magazine[i] = Instantiate(ar15Bullet);
            ar15Magazine[i].SetActive(false);
            ar15Magazine[i].gameObject.transform.parent = aRtransform;
        }

        //UMP45
        for (int i = 0; i < maxUMP45Ammo; i++)
        {
            ump45Magazine[i] = Instantiate(ump45Bullet);
            ump45Magazine[i].SetActive(false);
            ump45Magazine[i].gameObject.transform.parent = umpTranform;
        }
    }

    public void FireBullet(Vector3 pos, Quaternion rot, BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.SIG:
                {
                    sIGMagazine[sig].transform.position = pos;
                    sIGMagazine[sig].transform.rotation = rot;
                    sIGMagazine[sig].gameObject.SetActive(true);

                    sig++;

                    if (sig == maxSig)
                        sig = 0;
                }
                break;

            case BulletType.AR15:
                {
                    ar15Magazine[ar].transform.position = pos;
                    ar15Magazine[ar].transform.rotation = rot;
                    ar15Magazine[ar].gameObject.SetActive(true);

                    ar++;

                    if (ar == maxAr)
                        ar = 0;
                }
                break;

            case BulletType.UMP45:
                {
                    ump45Magazine[ump].transform.position = pos;
                    ump45Magazine[ump].transform.rotation = rot;
                    ump45Magazine[ump].gameObject.SetActive(true);

                    ump++;

                    if (ump == maxUmp)
                        ump = 0;
                }
                break;
        }
    }
}
