using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour {

  void OnGUI () {
    int posx   = 10;
    int posy   = 10;
    int w      = 100;
    int h      = 30;
    int buffer = 10;

    var items = Managers.Inventory.GetItemList();

    if (items.Count == 0) {
      GUI.Box(new Rect(posx, posy, w, h), "No items.");
    }

    foreach (var item in items) {
      var count = Managers.Inventory.GetItemCount(item);
      var image = Resources.Load<Texture2D>("Icons/" + item);

      GUI.Box(new Rect(posx, posy, w, h),
              new GUIContent("(" + count + ")", image));

      var actionType = "Equip " + item;
      var consumable = false;

      if (item == "health") {
        consumable = true;
        actionType = "Use " + item;
      }


      if (GUI.Button(new Rect(posx, posy + h + buffer, w, h), actionType)) {
        if (consumable) {
          Managers.Inventory.ConsumeItem(item);
          if (item == "health") {
            Managers.Player.ChangeHealth(25);
          }
        } else {
          Managers.Inventory.EquipItem(item);
        }
      }

      posx += w + buffer;
    }

    var equipped = Managers.Inventory.equipped;

    if (equipped != null) {
      var image = Resources.Load<Texture2D>("Icons/" + equipped);

      GUI.Box(new Rect(Screen.width - w - buffer, posy, w, h),
              new GUIContent("Equipped", image));
    }
  }

}
