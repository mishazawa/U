using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;

public class NetworkService {

  private const string WEATHER_URL = ApiConstants.API_URL + "q=" + ApiConstants.API_GEO + "&appid=" + ApiConstants.API_KEY;

  public IEnumerator GetWeather (Action<string> callback, bool xml = false) {
    return Get(xml ? WEATHER_URL + "&mode=xml" : WEATHER_URL , callback);
  }

  public IEnumerator GetImage (string url, Action<Texture2D> callback) {
    var request = UnityWebRequestTexture.GetTexture(url);
    yield return request.Send();
    callback(DownloadHandlerTexture.GetContent(request));
  }

  public IEnumerator Post (string url, string data, Action<string> callback) {
    byte[] body = Encoding.UTF8.GetBytes(data);

    var request = new UnityWebRequest (url, "POST");

    request.SetRequestHeader("Content-Type", "application/json");
    request.uploadHandler = (UploadHandler) new UploadHandlerRaw(body);
    request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

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

  private IEnumerator Get (string url, Action<string> callback) {
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
