using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARCameraManager))]
public class AutoFlashlight : MonoBehaviour
{
    ARCameraManager cameraManager;
    bool flashlightOn = false;
    float lightThreshold = 50f; // Se puede modificar, he puesto 50 por probar

    void Awake()
    {
        cameraManager = GetComponent<ARCameraManager>();
    }

    void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            float brightness = args.lightEstimation.averageBrightness.Value * 100f;

            if (brightness < lightThreshold && !flashlightOn)
            {
                EnableFlashlight(true);
            }
            else if (brightness >= lightThreshold && flashlightOn)
            {
                EnableFlashlight(false);
            }
        }
    }

    void EnableFlashlight(bool enable)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass cameraClass = new AndroidJavaClass("android.hardware.Camera");
        int cameraID = 0;
        AndroidJavaObject camera = cameraClass.CallStatic<AndroidJavaObject>("open", cameraID);
        AndroidJavaObject cameraParams = camera.Call<AndroidJavaObject>("getParameters");

        if (enable)
        {
            cameraParams.Call("setFlashMode", "torch");
        }
        else
        {
            cameraParams.Call("setFlashMode", "off");
        }

        camera.Call("setParameters", cameraParams);
        if (enable)
        {
            camera.Call("startPreview");
        }
        else
        {
            camera.Call("stopPreview");
            camera.Call("release");
        }
#endif
        flashlightOn = enable;
        Debug.Log("Flashlight " + (enable ? "ON" : "OFF"));
    }
}
