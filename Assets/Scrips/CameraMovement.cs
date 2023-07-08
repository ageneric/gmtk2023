using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Vector3 Position;
    private Vector3 Velocity;
    [Header("Camera Settings")]
    public float Speed;
    
    void Start() {
        Position = this.transform.position;
    }

    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            Velocity.y = Speed;
        } else if (Input.GetKey(KeyCode.S)) {
            Velocity.y = -Speed;;
        } else {
            if (Velocity.y < -0.5f)
                Velocity.y += 4f * Speed * Time.deltaTime;
            else if (Velocity.y > 0.5f)
                Velocity.y -= 4f * Speed * Time.deltaTime;
            else
                Velocity.y = 0f;
        }
        if (Input.GetKey(KeyCode.A)) {
            Velocity.x = -Speed;
        } else if (Input.GetKey(KeyCode.D)) {
            Velocity.x = Speed;
        } else {
            if (Velocity.x < -0.5f)
                Velocity.x += 4f * Speed * Time.deltaTime;
            else if (Velocity.x > 0.5f)
                Velocity.x -= 4f * Speed * Time.deltaTime;
            else
                Velocity.x = 0f;
        }
        Position += Velocity * Time.deltaTime;
        this.transform.position = Position;
    }
}