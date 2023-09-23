using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(LineRenderer))]
public class LineCollider : MonoBehaviour
{
    public void BakeMesh()
    {
        Mesh mesh = new();
        GetComponent<LineRenderer>().BakeMesh(mesh);
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}