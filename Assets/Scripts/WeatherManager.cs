using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set;}
    public float cloudiness {get; private set;}

    private NetworkService net;

    public void Startup(NetworkService service) {
      net = service;
      StartCoroutine(net.GetWeather(OnResp));
      status = ManagerStatus.Initializing;
    }

    public void LogWeather (string name) {
      Dictionary<string, object> obj = new Dictionary<string, object>();

      obj.Add("name", name);
      obj.Add("cloudiness", cloudiness.ToString());
      obj.Add("time", DateTime.UtcNow.Ticks.ToString());

      var data = Json.Serialize(obj);

      StartCoroutine(net.Post(ApiConstants.API_POST_WEATHER, data, OnLogged));
    }

    private void OnLogged (string resp) {
      Debug.Log(resp);
    }

    private void OnResp (string data) {
      cloudiness = ExtractCloudinessJson(data) / 100f;
      Debug.Log("Cl: " + cloudiness);
      Messenger.Broadcast(GameEvent.WEATHER_CHANGED);

      status = ManagerStatus.Started;
    }

    private float ExtractCloudinessJson (string json) {
      var data = Json.Deserialize(json) as Dictionary<string, object>;
      var clouds = data["clouds"] as Dictionary<string, object>;
      return (long)clouds["all"];
    }

    private float ExtractCloudinessXml (string xml) {
      var doc = new XmlDocument();
      doc.LoadXml(xml);

      var node = doc.DocumentElement.SelectSingleNode("clouds");
      return Convert.ToInt32(node.Attributes["value"].Value);
    }

}
