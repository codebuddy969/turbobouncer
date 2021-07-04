using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShotManager : MonoBehaviour
{
    public void Start()
    {
        EventsManager.current.onScreenCapture += captureScreenshot;

        captureScreenshot();
    }
    public void captureScreenshot()
    {
        Debug.Log("Tests");
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
    }
}
