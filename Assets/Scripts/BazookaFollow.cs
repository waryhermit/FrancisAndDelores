using UnityEngine;
using System.Collections;

public class BazookaFollow : MonoBehaviour {
    Camera cam;
    Transform my;


    void Awake()
    {
        cam = Camera.main;
        my = GetComponent<Transform>();
    }


    void Update()
    {
        // Distance from camera to object.  We need this to get the proper calculation.
        float camDis = cam.transform.position.y - my.position.y;

        // Get the mouse position in world space. Using camDis for the Z axis.
        Vector3 mouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));

        float AngleRad = Mathf.Atan2(mouse.y - my.position.y, mouse.x - my.position.x);
        float angle = (180 / Mathf.PI) * AngleRad;

        my.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
