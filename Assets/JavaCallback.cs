using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class PluginController : MonoBehaviour
{

    private string buttonText = "Send Message";

    public TextMesh displayText;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Returned Int = " + HackathonBridge.ReturnInt());
        Debug.Log("Returned String = " + HackathonBridge.ReturnString());

        Debug.Log("Returned Instance Int = " + HackathonBridge.ReturnInstanceInt());
        Debug.Log("Returned Instance String = " + HackathonBridge.ReturnInstanceString());

    }


    // Update is called once per frame
    void Update()
    {

    }

    void SetButtonText(string newText)
    {
        Debug.Log("Button Text = " + newText);
        buttonText = newText;
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), buttonText))
        {
            HackathonBridge.ShowCamera(12345);
        }
    }

}