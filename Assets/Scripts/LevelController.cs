using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private GameObject enemy;
    private float speed = 1f;
  // Start is called before the first frame update
    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float val) {
        speed = val;
        if (enemy != null) enemy.GetComponent<WanderingAi>().OnSpeedChanged(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null) {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(0, 1, 0);
            enemy.transform.Rotate(0, Random.Range(0, 360), 0);
            enemy.GetComponent<WanderingAi>().OnSpeedChanged(speed);
        }
    }
}
