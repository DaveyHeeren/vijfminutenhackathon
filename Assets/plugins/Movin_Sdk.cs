using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using UnityEngine;

public class Movin_Sdk : MonoBehaviour
{
    private AndroidJavaClass MovinSDK;
    void Start()
    {

        //#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        MovinSDK = new AndroidJavaClass("com.movin.sdk.MovinSDK");
        MovinSDK.CallStatic("initialize", new object[] { "kasiusbananavijfminutenkak", "594cbb69063c404b2f24d952", context, new AndroidPluginCallback() });
        //GetComponent<Text>().text = pluginClass.CallStatic<string>("getMessage");
        //#endif
    }

    public AndroidJavaObject getBeaconScanner()
    {
        return MovinSDK.CallStatic<AndroidJavaObject>("getBeaconScanner");
    }
}

public class MovinBeaconScanner : AndroidJavaProxy
{
    public MovinBeaconScanner() : base("com.movin.scanner.MovinBeaconScanner")
    {
    }

 
}

public class MovinSdk : AndroidJavaProxy
{
    public MovinSdk() : base("com.movin.sdk.MovinSDK")
    {
    }
}


public class AndroidPluginCallback : AndroidJavaProxy
{
    private AndroidJavaObject movinSDK = new AndroidJavaClass("com.movin.sdk.MovinSDK");;

    public AndroidPluginCallback() : base("com.movin.sdk.MovinSDKCallback")
    {
    }
    private AndroidJavaObject beaconScanner;

    public void initialized(Boolean var1, AndroidJavaObject var2)
    {
        Debug.Log("Hello bitch");

        createBeaconScanner();
    }

    private void createBeaconScanner()
    {
            beaconScanner = movinSDK.getBeaconScanner();

            // Add the map, so we can scan for the beacons in this map
            //beaconScanner.addMap(getMap(mapId), null);

            // Start the beaconScanner, so we can handle the beacon responses
            //beaconScanner.start(this);
    }
}

