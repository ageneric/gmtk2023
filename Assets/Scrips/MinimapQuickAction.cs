using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinimapQuickAction : MonoBehaviour, IPointerDownHandler
{
    public CameraMovement cameraMovement;


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
        Debug.Log(pointerEventData.position);
        
    }
}
