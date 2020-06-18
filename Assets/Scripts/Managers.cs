using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]
public class Managers : MonoBehaviour {
    public static PlayerManager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}
    public static WeatherManager Weather {get; private set;}
    public static ImagesManager Images {get; private set;}

    private List<IGameManager> managers;

    void Awake () {

      Player = GetComponent<PlayerManager>();
      Inventory = GetComponent<InventoryManager>();
      Weather = GetComponent<WeatherManager>();
      Images = GetComponent<ImagesManager>();

      managers = new List<IGameManager>() {Player, Inventory, Weather, Images};

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
