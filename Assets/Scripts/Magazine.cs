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

    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public Magazine()
    {
        currentBullet = bulletCapacity;
    }

    public BulletType GetBulletType()
    {
        return bulletType;
    }

    public void ReleaseMagazine()
    {
        boxCollider.enabled = true;
    }

    public void MagazineMeshSetActive(bool meshOn)
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = meshOn;
    }
}
