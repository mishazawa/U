using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    private bool open = false;
    private Vector3 axis;
    private int enterCounter = 0;

    private void Awake () {
      Mesh mesh = GetComponent<MeshFilter>().mesh;
      axis = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - mesh.bounds.size.z);
    }

    public void Operate () {
      if (open) {
        Deactivate();
      } else {
        Activate();
      }
    }

    public void Activate () {
      if (open) return;
      gameObject.transform.RotateAround(axis, Vector3.up, 90);
      open = true;
    }

    public void Deactivate () {
      if (!open) return;
      gameObject.transform.RotateAround(axis, Vector3.up, -90);
      open = false;
    }
}
