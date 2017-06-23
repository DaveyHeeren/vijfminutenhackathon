using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataDownloader : MonoBehaviour
{
    public delegate void MapDataReceiver(MovinMap map);

    [SerializeField]
    public string MapURL = "https://kasiusbananavijfminutenkak.movin.io/data/maps/getmap?mapid=579226d5bcab1ba605c5e426&apikey=594cbb69063c404b2f24d952&includebuildings=true&includeentities=true";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Download(MapDataReceiver receiver)
    {
        Debug.Log("downloading things");
        WWW www = new WWW(this.MapURL);
        yield return www;

        SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse(www.text);
        receiver(JsonUtility.FromJson<GetMapResponse>(www.text).map);
    }
}

[Serializable]
public class MovinMap
{
    public string _id;
    public int __v;
    public MovinBuilding[] buildings;
}

[Serializable]
public class MovinBuilding
{
    public string _id;
    public string type;
    public int __v;
    public MovinEntity[] entities;
}

[Serializable]
public class MovinEntityProperties
{
    public string name;
    public string baseType;
    public string subType;
    public int flooar;
    public string buildingId;
    public string mapID;
    public int rotation;
}

[Serializable]
public class MovinEntityGeometry
{
    public string type;
    public decimal[][][] coordinates;
}

[Serializable]
public class MovinEntity
{
    public string _id;
    public string type;
    public int __v;
    public MovinEntityGeometry geometry;
    public MovinEntityProperties properties;
}

[Serializable]
public class GetMapResponse
{
    public Boolean succes;
    public MovinMap map;
}
