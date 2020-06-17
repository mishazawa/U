using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(WeatherManager))]
public class Managers : MonoBehaviour {
    public static PlayerManager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}
    public static WeatherManager Weather {get; private set;}

    private List<IGameManager> managers;

    void Awake () {

      Player = GetComponent<PlayerManager>();
      Inventory = GetComponent<InventoryManager>();
      Weather = GetComponent<WeatherManager>();

      managers = new List<IGameManager>() {Player, Inventory, Weather};

      StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers () {
      var network = new NetworkService();

      foreach (var manager in managers) {
        manager.Startup(network);
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
