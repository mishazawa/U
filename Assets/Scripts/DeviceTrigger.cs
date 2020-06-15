using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
  [SerializeField]
  private GameObject[] targets;

  void OnTriggerEnter (Collider hit) {
    foreach (var t in targets) {
      t.SendMessage("Activate");
    }
  }

  void OnTriggerExit (Collider hit) {
    foreach (var t in targets) {
      t.SendMessage("Deactivate");
    }
  }
}
