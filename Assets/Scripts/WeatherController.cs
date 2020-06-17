using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    [SerializeField]
    private Material sky;
    [SerializeField]
    private Light sun;

    private float fullIntensity;
    private float cloudValue;
    private float direction = 1f;
    // Start is called before the first frame update
    void Start() {
        fullIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update() {
      cloudValue += 0.02f * direction;
      if (cloudValue >= 1) {
        direction = -1f;
      } else if (cloudValue <= 0) {
        direction = 1f;
      }
      SetOvercast(cloudValue);
    }

    void SetOvercast (float value) {
      sky.SetFloat("_Blend", value);
      sun.intensity = fullIntensity + (fullIntensity * value);
    }
}
