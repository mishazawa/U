using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    [SerializeField]
    private Material sky;
    [SerializeField]
    private Light sun;
    [SerializeField]
    private float minimalIntensity = 0.1f;

    private float fullIntensity;

    void Awake () {
      Messenger.AddListener(GameEvent.WEATHER_CHANGED, OnWeatherUpdate);
    }

    void OnDestroy () {
      Messenger.RemoveListener(GameEvent.WEATHER_CHANGED, OnWeatherUpdate);
    }

    void Start () {
        fullIntensity = sun.intensity;
    }

    void OnWeatherUpdate () {
      SetOvercast(Managers.Weather.cloudiness);
    }

    void SetOvercast (float value) {
      sky.SetFloat("_Blend", value);
      sun.intensity = Mathf.Clamp(fullIntensity - (fullIntensity * value), minimalIntensity, 1f);
    }
}
