using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                var target = hit.transform.gameObject.GetComponent<ReactiveTarget>();
                if (target != null) {
                    target.ReactToHit();
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
