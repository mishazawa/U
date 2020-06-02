using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    public void Open(bool val) {
        gameObject.SetActive(val);
    }
    private void Start() {
        slider.value = PlayerPrefs.GetFloat("speed", 1);
    }
    public void OnSpeedValue (float val) {
        PlayerPrefs.SetFloat("speed", val);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, val);
    }

    public void OnNameSubmit (string name) {

    }
}
