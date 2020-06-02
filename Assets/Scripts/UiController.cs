using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    private int score = 0;
    [SerializeField]
    private TMP_Text scoreLabel;

    [SerializeField]
    private PopupSettings popupSettings;

    private void Awake() {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnEnemyHit () {
        score += 1;
        scoreLabel.text = score.ToString();
    }

    private void Start() {
        score = 0;
        scoreLabel.text = score.ToString();
        popupSettings.Open(false);
    }

    public void OnOpenSettings () {
        popupSettings.Open(true);
    }
}
