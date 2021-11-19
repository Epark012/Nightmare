using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porori : MonoBehaviour, INightThriller
{
    [SerializeField]
    private float xSpeed, zSpeed, yOffset = 0.0f;
    [SerializeField]
    private Transform centerPoint;

    [SerializeField]
    private float rotSpeed;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime * rotSpeed;
        Rotate();
        transform.LookAt(centerPoint);
    }

    private void Rotate()
    {
        float x = -Mathf.Cos(timer) * xSpeed;
        float z = Mathf.Sin(timer) * zSpeed;
        Vector3 pos = new Vector3(x, yOffset, z);
        transform.position = pos + centerPoint.position;
    }

    public void TakeDamageFromEnemy(float damage)
    {
        Debug.Log("Porori is under attack.");
    }
}
