using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class HackathonBridge
{

    public static string ReturnString()
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.lokisystems.oshook.Bridge");
        return ajc.CallStatic<string>("ReturnString");

    }

    public static int ReturnInt()
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.lokisystems.oshook.Bridge");
        return ajc.CallStatic<int>("ReturnInt");
    }

    public static string ReturnInstanceString()
    {
        AndroidJavaObject ajo = new AndroidJavaObject("com.lokisystems.oshook.Bridge");
        return ajo.Call<string>("ReturnInstanceString");
    }

    public static int ReturnInstanceInt()
    {
        AndroidJavaObject ajo = new AndroidJavaObject("com.lokisystems.oshook.Bridge");
        return ajo.Call<int>("ReturnInstanceInt");
    }

    public static void SendUnityMessage(string objectName, string methodName, string parameterText)
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.lokisystems.oshook.Bridge");
        ajc.CallStatic("SendUnityMessage", objectName, methodName, parameterText);
    }

    public static void ShowCamera(int requestCode)
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.lokisystems.oshook.Bridge");
        ajc.CallStatic("ShowCamera", requestCode);
    }


}