using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set;}

    private NetworkService net;
    private Texture2D image;

    public void Startup (NetworkService service) {
      net = service;
      status = ManagerStatus.Started;
    }

    public void GetImage (string src, Action<Texture2D> callback) {
      if (image != null) {
        callback(image);
      } else {
        StartCoroutine(net.GetImage(src, (Texture2D texture) => {
          image = texture;
          callback(image);
        }));
      }
    }
}
