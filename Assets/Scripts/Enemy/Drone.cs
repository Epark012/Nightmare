﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class Drone : Enemy
{
    [SerializeField]
    private MeshRenderer droneMesh;
    [SerializeField]
    private GameObject broken;
    [SerializeField]
    private float scanRadius = 5f;

    private Rigidbody rigid;
    private NavMeshAgent agent;
    private Vector3 target;
    private int index = 0;

    public List<Mineral> localMineral = new List<Mineral>();


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PatrollingState();
        ScanningByRay();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }

    protected override void PatrollingState()
    {
        if(agent != null)
        {
            target = MineralWaypoints.Instance().MineralLists[index].transform.position;
            agent.SetDestination(target);
            if (Vector3.Distance( new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(target.x,0f,target.z)) <= agent.stoppingDistance)
            {
                if (index < MineralWaypoints.Instance().MineralLists.Count - 1)
                {
                    index++;
                }
                else
                    index = 0;
            }
        }
    }

    private void ScanningByRay()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, scanRadius, transform.forward, out hit))
        {
            if(hit.transform.CompareTag("Mineral"))
            {
                Mineral temp = hit.transform.GetComponent<Mineral>();
                if(temp != null && !localMineral.Contains(temp))
                {
                    localMineral.Add(temp);
                }
            }
        }

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        //Turn off AI 
        if(agent != null)
            agent.enabled = false;
        //Turn on Gravity
        rigid.useGravity = true;

        //Turn off drone mesh
        droneMesh.enabled = false;
        //Turn On separate broken drone mesh
        broken.SetActive(true);

    }


}