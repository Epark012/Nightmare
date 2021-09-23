using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPractice : MonoBehaviour
{
    protected Mesh mesh;
    protected MeshFilter meshFilter;
    protected MeshRenderer meshRenderer;

    public float upDownFactor = 0.1f;
    public float upDownSpeed = 6f;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Generated Mesh";

        mesh.vertices = GenerateVerts();
        mesh.triangles = GenerateTries();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = default;

    }
    private Vector3[] GenerateVerts(float up = 0)
    {
        return new Vector3[]
        {
            //Bottom
            new Vector3 (-1,0, 1),
            new Vector3 ( 1,0, 1),
            new Vector3 ( 1,0,-1),
            new Vector3 (-1,0,-1),

            //Top
            new Vector3 (-1,2 + up, 1),
            new Vector3 ( 1,2 + up, 1),
            new Vector3 ( 1,2 + up,-1),
            new Vector3 (-1,2 + up,-1),
        };

    }

    private int[] GenerateTries()
    {
        return new int[]
        {
            //Bottom
            1,0,2,
            2,0,3,

            //Top
            4,5,6,
            4,6,7,

            //Front
            3,7,2,
            7,6,2,

            //Back
            4,0,5,
            0,1,5,

            //Right
            6,5,1,
            6,1,2,

            //Left
            4,7,0,
            7,3,0,
        };

    }

    // Update is called once per frame
    void Update()
    {
        mesh.vertices = GenerateVerts(upDownFactor);
    }
}
