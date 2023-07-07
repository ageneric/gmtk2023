using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 Position;
    [Header("Camera Settings")]
    public float Speed;
    
    void Start()
    {
        Position = this.transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Position.y -= Speed / 100;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Position.y += Speed / 10;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed / 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Position.x += Speed / 10;
        }
        this.transform.position = Position;
    }
}
