using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Axes {
        MouseXY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public Axes axes = Axes.MouseXY;
    public float sensivityHor = 5f;
    public float sensivityVer = 3f;

    public float limitRotationVer = 45f;

    // -------

    private float rotationVer = 0f;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == Axes.MouseX) {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        }

        if (axes == Axes.MouseY) {
            rotationVer -= Input.GetAxis("Mouse Y") * sensivityVer;
            rotationVer =  Mathf.Clamp(rotationVer, -limitRotationVer, limitRotationVer);
            transform.localEulerAngles = new Vector3(rotationVer, transform.localEulerAngles.y, 0);
        }
    }
}
