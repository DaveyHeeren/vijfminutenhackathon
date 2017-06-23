using UnityEngine;
using System.Collections;
using System;
using System.Text;


public class MovinMapsTexture : OnlineTexture
{
    public static string testServerURL = "https://kasiusbananavijfminutenkak.movin.io/wmts/tiles/Default/Default/1/{z}/{x}/{y}.png?apikey=594cbb69063c404b2f24d952";
    public string serverURL = MovinMapsTexture.testServerURL;
    public float latitude = 52.507235f;
    public float longitude = 6.086264f;
    public int initialZoom = 20;

    public Vector2 GetProjectionValues()
    {
        float sinLatitude = Mathf.Sin(latitude * Mathf.PI / 180.0f);

        int pixelX = (int)(((longitude + 180) / 360) * 256 * Mathf.Pow(2, initialZoom + 1));
        int pixelY = (int)((0.5f - Mathf.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Mathf.PI)) * 256 * Mathf.Pow(2, initialZoom + 1));

        int tileX = Mathf.FloorToInt(pixelX / 256);
        int tileY = Mathf.FloorToInt(pixelY / 256);

        tileX /= 2;
        tileY /= 2;

        return new Vector2(tileX, tileY);
    }

    public string makeURL(int zoomLevel)
    {
        Vector2 projValues = this.GetProjectionValues();
        string s = this.serverURL.Replace("{z}", zoomLevel.ToString());
        s = s.Replace("{x}", projValues.x.ToString()).Replace("{y}", projValues.y.ToString());

        Debug.Log(s);

        return s;
    }


    protected override string GenerateRequestURL(string nodeID)
    {
        // Children node numbering differs between QuadtreeLODNoDe and Bing maps, so we
        // correct it here.
        // nodeID = nodeID.Substring(1).Replace('1', '9').Replace('2', '1').Replace('9', '2');

        Debug.Log(nodeID);
        return this.makeURL(this.initialZoom);
    }


    public string CurrentFixedUrl()
    {
        return serverURL;
    }


    protected override void InnerCopyTo(OnlineTexture copy)
    {
        MovinMapsTexture target = (MovinMapsTexture)copy;
        target.serverURL = serverURL;
        target.latitude = latitude;
        target.longitude = longitude;
        target.initialZoom = initialZoom;
    }
}
