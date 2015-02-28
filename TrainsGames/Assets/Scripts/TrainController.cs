using UnityEngine;
using System.Collections;

public class TrainController : MonoBehaviour
{

    public GameObject gun;
    public int numberOfTracks = 5;


    public int currentTrack { get { return Mathf.CeilToInt(transform.position.x); } }

    // Use this for initialization
    void Start()
    {
        var p = GetComponentInChildren<ParticleSystem>();
        p.renderer.sortingLayerName = "Foreground";
    }

    const float COOLDOWN = 0.2f;
    float cooldown = COOLDOWN;
    // Update is called once per frame
    void Update()
    {

        gun.transform.Rotate(Vector3.forward, -Input.GetAxis("Turret") * Time.deltaTime * 100);
        if (cooldown < COOLDOWN)
            cooldown += Time.deltaTime;
        if (cooldown >= COOLDOWN)
        {
            int move = Mathf.CeilToInt(Input.GetAxis("Horizontal"));
            Debug.Log(Input.GetAxis("Horizontal"));
            if ( move != 0 && move + currentTrack < 5 && move + currentTrack >= 0)
            {
                transform.position += new Vector3(move,0,0);
                cooldown = 0;
            }
        }


    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(0, 1);
    }
}

