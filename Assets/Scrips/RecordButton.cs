using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    public Vector3 eventLocation;
    public CameraMovement cameraMovement;

    public void OnClick()
    {
        cameraMovement.MoveCameraTo(eventLocation);
    }
}
