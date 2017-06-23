using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

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
        //mesh.Clear();
        //mesh.vertices = vertices;
        //mesh.triangles = tris;
    }

    private void MakeMeshData(JSONNode building)
    {
        mesh.Clear();
        List<Vector3> v3List = new List<Vector3>();

        foreach (JSONNode node in building["entities"].AsArray)
        {
            JSONObject entity = node.AsObject;
            JSONObject properties = entity["properties"].AsObject;
            if (properties["baseType"].Equals("Room"))
            {
                JSONArray coordinates = entity["geometry"].AsObject["coordinates"].AsArray[0].AsArray;
                JSONArray prevNode = null;
                foreach (JSONNode cnode in coordinates)
                {
                    JSONArray coords = cnode.AsArray;
                    if (prevNode != null)
                    {
                        float x = coords[0].AsFloat;
                        float z = coords[1].AsFloat;
                        float px = prevNode[0].AsFloat;
                        float pz = prevNode[1].AsFloat;

                        string sx = x.ToString().Substring(6);
                        string sz = z.ToString().Substring(6);
                        string spx = px.ToString().Substring(6);
                        string spz = pz.ToString().Substring(6);

                        x = float.Parse(sx);
                        z = float.Parse(sz);
                        px = float.Parse(spx);
                        pz = float.Parse(spz);

                        // Debug.Log(String.Format("{0}, {1}, {2}, {3}", x, z, px, pz));

                        v3List.Add(new Vector3(px, 0, pz));
                        v3List.Add(new Vector3(px, 40, pz));
                        v3List.Add(new Vector3(x, 0, z));

                        v3List.Add(new Vector3(px, 0, pz));
                        v3List.Add(new Vector3(x, 40, z));
                        v3List.Add(new Vector3(x, 0, z));
                    }

                    prevNode = coords;
                }
            }
        }

        mesh.vertices = v3List.ToArray();
        List<int> trisList = new List<int>();
        for (int i = 0; i < v3List.Count; i++)
        {
            trisList.Add(i);
        }
        mesh.triangles = trisList.ToArray();

        foreach (Vector3 vert in mesh.vertices)
        {
            // Debug.Log(vert);
        }

        Debug.Log("Made mesh data");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DataReceiver(JSONNode building)
    {
        MakeMeshData(building);
        CreateMesh();
    }
}
