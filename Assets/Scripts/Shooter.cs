using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooter : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                var target = hit.transform.gameObject.GetComponent<ReactiveTarget>();
                if (target != null) {
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                } else {
                    StartCoroutine(Sphere(hit.point));
                }
            }
        }
    }

    private IEnumerator Sphere (Vector3 pos) {
        GameObject sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sph);
    }


    private void OnGUI() {
        var x = cam.pixelWidth / 2 - 3;
        var y = cam.pixelHeight / 2 - 6;
        GUI.Label(new Rect(x, y, 12, 12), "*");
    }
}
