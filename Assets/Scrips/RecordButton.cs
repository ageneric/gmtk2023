using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    public Vector2 eventLocation;
    private CameraMovement cameraMovement;

    public void Start()
    {
        cameraMovement = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
    }

    public void OnClick()
    {
        cameraMovement.MoveCameraTo(eventLocation);
    }
}
