using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
  [SerializeField]
  private GameObject[] targets;

  public bool requireKey;

  void OnTriggerEnter (Collider hit) {
    if (requireKey && Managers.Inventory.equipped != "key") return;

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
