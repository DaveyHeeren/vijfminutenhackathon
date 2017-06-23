using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMeshGenerator : MonoBehaviour
{
    MapDataDownloader downloader;
    Mesh mesh;

    Vector3[] vertices;
    int[] tris;

    void Awake()
    {
        downloader = GetComponent<MapDataDownloader>();
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("Running the map mesh generator :O");
        StartCoroutine(downloader.Download(this.DataReceiver));
    }

    private void CreateMesh()
    {
        Debug.Log("Creating mesh");
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = tris;
    }

    private void MakeMeshData(MovinBuilding building)
    {
        List<Vector3> v3List = new List<Vector3>();

        foreach (MovinEntity entity in building.entities)
        {
            if (entity.geometry != null && entity.properties.baseType == "Room")
            {
                Debug.Log(entity.geometry.type);
                Debug.Log(entity.geometry.coordinates);
                //foreach (float[] coordinate in entity.geometry.coordinates[0])
                //{
                //    v3List.Add(new Vector3(coordinate[0], 0, coordinate[1]));
                //}
            }
        }

        this.tris = new int[v3List.Count];
        this.vertices = v3List.ToArray();
        for (int i = 0; i < this.vertices.Length; i++)
        {
            tris[i] = i;
        }

        Debug.Log("Made mesh data");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DataReceiver(MovinMap map)
    {
        Debug.Log(map.buildings[0]._id);

        MakeMeshData(map.buildings[0]);
        CreateMesh();
    }
}
