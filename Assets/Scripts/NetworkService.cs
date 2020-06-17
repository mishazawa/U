using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class NetworkService {

  private const string WEATHER_URL = ApiConstants.API_URL + "q=" + ApiConstants.API_GEO + "&appid=" + ApiConstants.API_KEY + "&mode=xml";

  public IEnumerator GetWeather (Action<string> callback) {
    return GetRequest(WEATHER_URL, callback);
  }

  private IEnumerator GetRequest (string url, Action<string> callback) {
    using (var request = UnityWebRequest.Get(url)) {
      yield return request.Send();

      if (request.isNetworkError) {
        Debug.LogError("NetErr " + request.error);
        yield break;
      }

      if (request.responseCode != (long)System.Net.HttpStatusCode.OK) {
        Debug.LogError("HttpErr " + request.responseCode);
        yield break;
      }

      callback(request.downloadHandler.text);
    }
  }
}
