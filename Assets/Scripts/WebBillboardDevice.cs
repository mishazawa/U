using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBillboardDevice : MonoBehaviour {
    public void Operate() {
      Managers.Images.GetImage(ApiConstants.MEGUCA_IMG, OnLoad);
    }

    private void OnLoad (Texture2D texture) {
      GetComponent<Renderer>().material.mainTexture = texture;
    }
}
