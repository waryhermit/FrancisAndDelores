using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;              // Prefab of the rocket.
    public float speed = 20f;               // The speed the rocket will fire at.


    private PlayerControl playerCtrl;       // Reference to the PlayerControl script.
    private Animator anim;                  // Reference to the Animator component.


    void Awake()
    {
        // Setting up the references.
        anim = transform.root.gameObject.GetComponent<Animator>();
        playerCtrl = transform.root.GetComponent<PlayerControl>();


    }

    public void Fire()
    {
        var cam = Camera.main;
        var my = this.transform;
        // Distance from camera to object.  We need this to get the proper calculation.
        float camDis = cam.transform.position.y - my.position.y;

        // Get the mouse position in world space. Using camDis for the Z axis.
        Vector3 mouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));

        float AngleRad = Mathf.Atan2(mouse.y - my.position.y, mouse.x - my.position.x);
        float angle = Mathf.Rad2Deg * AngleRad;
        // ... set the animator Shoot trigger parameter and play the audioclip.
        anim.SetTrigger("Shoot");
        GetComponent<AudioSource>().Play();
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mp = (Input.mousePosition - sp);
        Vector3 dir = mp.normalized;

        Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
        bulletInstance.velocity = dir * speed; // new Vector2(speed, 0);
    }


}
