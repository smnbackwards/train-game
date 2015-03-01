using UnityEngine;
using System.Collections;

public class TrainController : MonoBehaviour
{

    public GameObject gun;
    public int numberOfTracks = 5;
    public float baseSpeed = 4f;
    float extraSpeed = 0;
    public static float speed = 1.5f;
    public float maxSpeed = 12;

    public Gauge speedGauge;

    PlayerHealth PlayerHealth;
    PlayerWater PlayerWater;

    int currentTrack;
    int currentDistance = 0;

    void Awake()
    {
        PlayerHealth = GetComponentInChildren<PlayerHealth>();
        PlayerWater = GetComponentInChildren<PlayerWater>();
    }

    // Use this for initialization
    void Start()
    {
        var p = GetComponentInChildren<ParticleSystem>();
        p.renderer.sortingLayerName = "Foreground";
        transform.position = new Vector3(Mathf.FloorToInt(numberOfTracks / 2), transform.position.y, 0);
        currentTrack = Mathf.CeilToInt(transform.position.x);
    }

    const float COOLDOWN = 0.4f;
    float cooldown = COOLDOWN;
    // Update is called once per frame
    void Update()
    {
        extraSpeed += Time.deltaTime / 15; ;
        speed = Mathf.Min(maxSpeed, baseSpeed + extraSpeed);
        speedGauge.value = speed;

        float x = Input.GetAxis("TurretH");
        float y = Input.GetAxis("TurretV");
        if (x != 0 || y != 0)
        {
            Vector3 directionVector = new Vector3(x, y, 0);
            float r = -Mathf.Rad2Deg * Mathf.Atan2(y, x) - 90;
            gun.transform.rotation = Quaternion.Euler(0, 0, r);
        }


        //gun.transform.Rotate(Vector3.forward, -Input.GetAxis("Turret") * Time.deltaTime * 300);
        int move = Mathf.CeilToInt(Input.GetAxis("Horizontal"));
        if (cooldown < COOLDOWN)
            cooldown += Time.deltaTime;
        if (cooldown >= COOLDOWN)
        {

            if (move != 0 && move + currentTrack < numberOfTracks && move + currentTrack >= 0)
            {
                currentTrack = currentTrack + move;
                //transform.position += new Vector3(move,0,0);
                cooldown = 0;
            }
        }

        if (move == 0)
        {
            cooldown = COOLDOWN;
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(currentTrack, transform.position.y), 3 * speed * Time.deltaTime);

        int d = Mathf.FloorToInt(transform.position.y);
        if (d > currentDistance)
        {
            Score.increaseScore(d - currentDistance);
            PlayerWater.loseWater((d-currentDistance)/5.0f);
            currentDistance = d;
        }

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

        if (collider.tag == "Health")
        {
            PlayerHealth.GainHealth(10);
            Destroy(collider.gameObject);
        }

        if (collider.tag == "Station")
        {
            PlayerWater.fillWater();
            collider.gameObject.collider2D.enabled = false;
        }

    }
}

