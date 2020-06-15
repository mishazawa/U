using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3")) {
          var hits = Physics.OverlapSphere(gameObject.transform.position, radius);
          foreach (var hit in hits) {
              var dir = hit.transform.position - gameObject.transform.position;
              if (Vector3.Dot(dir, gameObject.transform.forward) > .5f) {
                hit.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
              }
          }
        }
    }
}
