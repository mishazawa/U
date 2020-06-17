using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set;}

    private NetworkService net;


    public void Startup(NetworkService service) {
      net = service;
      StartCoroutine(net.GetWeather(OnResp));
      status = ManagerStatus.Initializing;
    }

    private void OnResp (string xml) {
      Debug.Log(xml);
      status = ManagerStatus.Started;
    }
}
