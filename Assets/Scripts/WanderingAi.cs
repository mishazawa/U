using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAi : MonoBehaviour
{
    private bool alive;
    public float speed = 3f;
    public float obstacleRange = 5f;

    [SerializeField]
    private GameObject fireballPrefab;
    private GameObject fireball;
    void Start()
    {
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) {
            transform.Translate(0, 0, speed * Time.deltaTime);

            RaycastHit hit;
            if (Physics.SphereCast(new Ray(transform.position, transform.forward), 0.75f, out hit)) {
                if (hit.transform.gameObject.GetComponent<PlayerCharacter>()) {
                    if (fireball == null) {
                        fireball = Instantiate(fireballPrefab) as GameObject;
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                } else if (hit.distance < obstacleRange) {
                    var angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool newState) {
        alive = newState;
    }
}
