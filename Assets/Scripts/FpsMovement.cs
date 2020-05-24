using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Fps Input")]
public class FpsMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.8f;


    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxis("Horizontal") * speed;
        var dz = Input.GetAxis("Vertical") * speed;

        var movement = new Vector3(dx, 0, dz);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }
}
