using UnityEngine;
using System.Collections;

public class TrainController : MonoBehaviour
{

    public GameObject gun;
    public int numberOfTracks = 5;
    public float baseSpeed = 2;
    float extraSpeed = 0;
    public static float speed = 1.5f;

    PlayerHealth PlayerHealth;

    int currentTrack;

    void Awake()
    {
        PlayerHealth = GetComponentInChildren<PlayerHealth>();
    }

    // Use this for initialization
    void Start()
    {
        var p = GetComponentInChildren<ParticleSystem>();
        p.renderer.sortingLayerName = "Foreground";
        currentTrack = Mathf.CeilToInt(transform.position.x);
    }

    const float COOLDOWN = 0.4f;
    float cooldown = COOLDOWN;
    // Update is called once per frame
    void Update()
    {
        float s = Mathf.Min(5, Mathf.Floor(transform.position.y / 50));
        if (s > extraSpeed)
            extraSpeed = s;
        speed = baseSpeed + extraSpeed ;
        Debug.Log(speed);

        gun.transform.Rotate(Vector3.forward, -Input.GetAxis("Turret") * Time.deltaTime * 300);
        if (cooldown < COOLDOWN)
            cooldown += Time.deltaTime;
        if (cooldown >= COOLDOWN)
        {

            int move = Mathf.CeilToInt(Input.GetAxis("Horizontal"));
            if (move != 0 && move + currentTrack < 5 && move + currentTrack >= 0)
            {
                currentTrack = currentTrack + move;
                //transform.position += new Vector3(move,0,0);
                cooldown = 0;
            }
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentTrack, transform.position.y),  10*Time.deltaTime);


    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(0, speed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        BlockedTrack track = collider.gameObject.GetComponent<BlockedTrack>();
        if (track != null)
        {
            PlayerHealth.TakeDamage(BlockedTrack.damage);
            track.Destroy();
        }

        BrokenTrack broken = collider.gameObject.GetComponent<BrokenTrack>();
        if (broken != null)
        {
            PlayerHealth.TakeDamage(PlayerHealth.currentHealth);
        }

    }
}

