using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using UnityEngine;

public class Movin_Sdk : MonoBehaviour
{
	public string positionText;
	public string distanceText;
   
    void Start()
	{
		// Preventing mobile devices going in to sleep mode 
		//(actual problem if only accelerometer input is used)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        var MovinSDK = new AndroidJavaClass("com.movin.sdk.MovinSDK");
		MovinSDK.CallStatic("initialize", new object[] { "kasiusbananavijfminutenkak", "594cbb69063c404b2f24d952", context, new AndroidPluginCallback(MovinSDK,this) });
        //GetComponent<Text>().text = pluginClass.CallStatic<string>("getMessage");
        //#endif
    }

	public void SetText(string text){
		this.positionText = text;
	}


	public void SetDistance(string text){
		this.distanceText = text;
	}


}

public class MovinBeaconScanner : AndroidJavaProxy
{
    public MovinBeaconScanner() : base("com.movin.scanner.MovinBeaconScanner")
    {
    }
}

public class MovinBeaconScannerListener : AndroidJavaProxy
{
	Movin_Sdk sdkje;

	public MovinBeaconScannerListener(Movin_Sdk sdkje) : base("com.movin.scanner.MovinBeaconScannerListener")
	{
		this.sdkje = sdkje;
	}

	public int hashCode(){
		AndroidJavaObject jo = new AndroidJavaObject("java.lang.String", "some stringetje");
		return jo.Call<int> ("hashCode");
	}

	public Boolean isValidNearestBeacon(AndroidJavaObject MovinBeaconScanner, AndroidJavaObject movinRangedBeacon)
	{
//		Debug.Log ("isvalidnearestbeacon");
		var rangedBeacon = movinRangedBeacon.Call<AndroidJavaObject> ("getBeacon");
		Debug.Log (movinRangedBeacon);
		return  rangedBeacon != null && rangedBeacon.Call<AndroidJavaObject> ("getPosition") != null;
	}

	public void didRangeBeacons(AndroidJavaObject MovinBeaconScanner, AndroidJavaObject beacons)
	{
//		Debug.Log ("didRangeBeacons");
	}



	public void didChangeNearestBeacon(AndroidJavaObject MovinBeaconScanner, AndroidJavaObject nearestBeacon)
	{
//		Debug.Log ("didChangeNearestBeacon");
		if (nearestBeacon != null) {
			var beacon = nearestBeacon.Call<AndroidJavaObject> ("getBeacon");
			var distance = nearestBeacon.Call<Double> ("getDistance");
			var code = beacon.Call<String> ("getBeaconCode");
			var reference = beacon.Call<int>("getReferenceId");


			var pos = beacon.Call<AndroidJavaObject> ("getPosition");
			var position = pos.Get<AndroidJavaObject> ("position");
			var latitude = position.Get<Double> ("lat");
			var longitude = position.Get<Double> ("lng");
			var positiontxt = "position: " + latitude + ", " + longitude;
			var distancetxt = "distance: " + distance;
			var referencetxt = "referencebeacon: " + reference;
			sdkje.SetText (referencetxt + "," + positiontxt + "," + distancetxt);
			Debug.Log ("("+code+")"+referencetxt + "," + positiontxt + "," + distancetxt);
//			var text = sdkje.gameObject.AddComponent<UnityEngine.UI.Text> ();
//			text.text = latitude + "" + longitude;

		} else {
//			Debug.Log ("beacon=null");
		}
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
    private AndroidJavaObject movinSDK = new AndroidJavaClass("com.movin.sdk.MovinSDK");
	private Movin_Sdk eigensdkjeije;
//
	public AndroidPluginCallback(AndroidJavaClass MovinSDK, Movin_Sdk movinsdkeigen) : base("com.movin.sdk.MovinSDKCallback")
    {
		this.eigensdkjeije = movinsdkeigen;
		this.movinSDK = MovinSDK;
    }
    private AndroidJavaObject beaconScanner;

    public void initialized(Boolean var1, AndroidJavaObject var2)
    {
        Debug.Log("Hello meuvin");

        createBeaconScanner();
    }
//
    private void createBeaconScanner()
    {
		beaconScanner = movinSDK.CallStatic<AndroidJavaObject> ("getBeaconScanner");
		var movinmap = new AndroidJavaClass("com.movin.maps.MovinMap");
		var mappie = movinSDK.CallStatic<AndroidJavaObject> ("getMap", new object[]{ "579226d5bcab1ba605c5e426" });
		beaconScanner.Call("addMap",new object[]{mappie,null});
//            // Add the map, so we can scan for the beacons in this map
////        beaconScanner.addMap(getMap(mapId), null);
//
//            // Start the beaconScanner, so we can handle the beacon responses
////            beaconScanner.start(this);
		beaconScanner.Call ("start", new object[]{new MovinBeaconScannerListener(eigensdkjeije)});
    }
}