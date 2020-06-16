using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }
    public string equipped { get; private set; }

    private Dictionary<string, int> items;

    public void Startup() {
      items = new Dictionary<string, int>();
      status = ManagerStatus.Started;
    }

    public void AddItem (string name) {
      if (items.ContainsKey(name)) {
        items[name] += 1;
      } else {
        items[name] = 1;
      }
    }

    public void DisplayItems () {
      var message = "Inventory: ";
      foreach (var item in items) {
        message += item.Key + "(" + item.Value + "), ";
      }
      Debug.Log(message);
    }

    public List<string> GetItemList () {
      return new List<string>(items.Keys);
    }

    public int GetItemCount (string name) {
      return items.ContainsKey(name) ? items[name] : 0;
    }

    public bool EquipItem (string name) {
      if (items.ContainsKey(name) && equipped != name) {
        equipped = name;
        return true;
      }
      equipped = null;
      return false;
    }

    public bool ConsumeItem (string name) {
      if (items.ContainsKey(name)) {
        items[name] -= 1;
        if (items[name] == 0) items.Remove(name);
        return true;
      }
      return false;
    }
}
