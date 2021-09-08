using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 15f;

    [SerializeField]
    private float lifeTime = 3;

    [SerializeField]
    private GameObject explosionVFX;

    private Rigidbody rigid;
    private Quaternion originRot;

    private void Awake()
    {
        originRot = gameObject.transform.rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(SetActiveFalseAmmo());
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(transform.forward * bulletSpeed);
    }

    private IEnumerator SetActiveFalseAmmo()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = originRot;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
