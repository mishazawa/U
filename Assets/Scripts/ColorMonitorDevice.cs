using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMonitorDevice : MonoBehaviour
{
    public void Operate() {
      var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
      GetComponent<Renderer>().material.color = color;
    }
}
