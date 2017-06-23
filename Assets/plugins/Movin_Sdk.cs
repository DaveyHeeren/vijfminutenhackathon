using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using UnityEngine;

public class Movin_Sdk : MonoBehaviour
{


    void Start()
    {

        ////#if UNITY_ANDROID
        //AndroidJavaClass MovinSDK = new AndroidJavaClass("com.movin.sdk.MovinSDK");
        //AndroidJavaObject jo = MovinSDK.GetStatic<AndroidJavaObject>("currentActivity");
        //MovinSDK.Call("initialize", new object[] { "kasiusbananavijfminutenkak", "594cbb69063c404b2f24d952", jo, new AndroidPluginCallback() });
        ////GetComponent<Text>().text = pluginClass.CallStatic<string>("getMessage");
        ////#endif
    }
}

//class AndroidPluginCallback : AndroidJavaProxy
//{
//    public AndroidPluginCallback() : base("com.example.android.PluginCallback") { }

//    public void onSuccess(string videoPath)
//    {
//        Debug.Log("ENTER callback onSuccess: " + videoPath);
//    }
//    public void onError(string errorMessage)
//    {
//        Debug.Log("ENTER callback onError: " + errorMessage);
//    }
//}

