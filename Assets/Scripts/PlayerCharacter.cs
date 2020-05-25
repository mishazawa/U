using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    // Update is called once per frame
    void Update()
    {

    }

    public void Hurt(int damage) {
        health -= damage;
        Debug.Log("Damage received -> " + health);
    }
}
