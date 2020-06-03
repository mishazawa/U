using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private CharacterController controller;
    public float rotationSpeed = 15f;
    public float moveSpeed = 6f;

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        if (hor != 0 || ver != 0) {
            var movement = Vector3.ClampMagnitude(new Vector3(hor * moveSpeed, 0, ver * moveSpeed), moveSpeed);
            var tmp = target.rotation;

            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            var direction = Quaternion.LookRotation(movement);
            gameObject.transform.rotation = Quaternion.Lerp(
                gameObject.transform.rotation,
                direction,
                rotationSpeed * Time.deltaTime
            );

            movement *= Time.deltaTime;
            controller.Move(movement);
        }
    }
}
