using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private CharacterController controller;
    private Animator animator;
    private ControllerColliderHit contact;
    private float verticalSpeed;

    public float rotationSpeed = 15f;
    public float moveSpeed = 6f;
    public float jumpSpeed = 15f;
    public float minFall = -1.5f;
    public float gravity = -9.8f;
    public float terminalFall = -10f;

    private void Start() {
        verticalSpeed = minFall;
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        controller.Move(HandleGravity(HandleMovement(Vector3.zero, hor, ver)));
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        contact = hit;

        var body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic) {
            var horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
            body.velocity = hit.moveDirection * horizontalVelocity.magnitude;
        }
    }

    private Vector3 HandleMovement (Vector3 movement, float hor, float ver) {
        if (hor != 0 || ver != 0) {
            movement = Vector3.ClampMagnitude(new Vector3(hor * moveSpeed, 0, ver * moveSpeed), moveSpeed);
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
        }
        return movement;
    }

    private Vector3 HandleGravity (Vector3 movement) {

        animator.SetFloat("Speed", movement.sqrMagnitude);

        bool isGrounded = false;
        RaycastHit hit;

        if (verticalSpeed < 0 && Physics.Raycast(gameObject.transform.position, Vector3.down, out hit)) {
            float distanceCheck = (controller.height + controller.radius) / 1.9f;
            isGrounded = hit.distance <= distanceCheck;
        }


        if (isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                verticalSpeed = jumpSpeed;
            } else {
                verticalSpeed = minFall;
                animator.SetBool("Jump", false);
            }
        } else {
            verticalSpeed += gravity * 5f * Time.deltaTime;
            if (verticalSpeed < terminalFall) verticalSpeed = terminalFall;
            if (contact != null) animator.SetBool("Jump", true);
            if (controller.isGrounded) {
                if (Vector3.Dot(movement, contact.normal) < 0) {
                    movement = contact.normal * moveSpeed;
                } else {
                    movement += contact.normal * moveSpeed;
                }
            }
        }

        movement.y = verticalSpeed;
        return movement * Time.deltaTime;
    }
}
