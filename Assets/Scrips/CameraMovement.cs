using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 Position;
    private Vector3 Velocity;
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
            Position.y += Speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Position.y -= Speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Position.x -= Speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Position.x += Speed * Time.deltaTime * 5;
        }
        Position += Velocity * Time.deltaTime;
        Velocity -=  * Time.deltaTime
        this.transform.position = Position;
    }
}
