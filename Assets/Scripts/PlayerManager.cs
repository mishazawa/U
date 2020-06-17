using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set;}

    public int health {get; private set;}
    public int maxHealth {get; private set;}


    public void Startup(NetworkService service) {
        health = 50;
        maxHealth = 150;
        status = ManagerStatus.Started;
    }

    public void ChangeHealth (int val) {
        health = Mathf.Clamp(health + val, 0, maxHealth);
        Debug.Log("Health: " + health + "/" + maxHealth);
    }
}
