using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour {

    public static PlayerManager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}

    private List<IGameManager> managers;

    void Awake () {
      Player = GetComponent<PlayerManager>();
      Inventory = GetComponent<InventoryManager>();

      managers = new List<IGameManager>() {Player, Inventory};

      StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers () {
      foreach (var manager in managers) {
        manager.Startup();
      }
      yield return null;

      var all = managers.Count;
      var ready = 0;

      while (ready < all) {
        ready = managers.Where(m => m.status == ManagerStatus.Started).Count();
        Debug.Log("Ready:" + ready + "/" + all);
        yield return null;
      }
      Debug.Log("Managers ready!");

    }
}
