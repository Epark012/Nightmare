using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roaming : MonoBehaviour
{
    [SerializeField]
    private Transform[] roamingPos = null;

    private NavMeshAgent navMesh = null;
    [SerializeField]
    private int posIndex = 0;

    [SerializeField] float distance;

    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(navMesh != null)
        {
            navMesh.SetDestination(roamingPos[posIndex].position);
            Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 targetPos = new Vector3(roamingPos[posIndex].position.x, 0, roamingPos[posIndex].position.z);
            if(Vector3.Distance(currentPos, targetPos) <= navMesh.stoppingDistance)
            {
                posIndex++;
                if (posIndex > roamingPos.Length - 1)
                {
                    posIndex = 0;
                }
            }
        }
    }
}
