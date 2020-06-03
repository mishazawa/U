using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 offset;
    private float rotationY;
    public float rotationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - gameObject.transform.position;
        rotationY = gameObject.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        gameObject.transform.position = target.position - rotation * offset;
        gameObject.transform.LookAt(target);
    }
}
