using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinimapQuickAction : MonoBehaviour, IPointerDownHandler
{
    public CameraMovement cameraMovement;
    public float left = 1024f;
    public float right = 1246f;
    public float low = 255f;
    public float high = 466f;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
        Debug.Log(pointerEventData.position);

        float x = (pointerEventData.position.x - left) / (right - left);
        float y = (pointerEventData.position.y - low) / (high - low);

        Vector2 newPosition = new Vector2(-39f + x*77f,
                                          -35f + y*72f);

        cameraMovement.MoveCameraTo(newPosition);
    }
}
