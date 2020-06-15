using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    void OnTriggerEnter (Collider other) {
      Debug.Log("Collected: " + itemName);
      Destroy(gameObject);
    }
}
