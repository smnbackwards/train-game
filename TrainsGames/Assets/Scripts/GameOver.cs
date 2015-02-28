using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public GameObject player;

    Animator anim;
    bool gameOver = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    bool down = true;
    // Update is called once per frame
    void Update()
    {

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            gameOver = true;
        }

        if (gameOver)
        {

            if (Input.GetAxis("Fire1") == 0 && Input.GetAxis("Fire2") == 0)
                down = false;

            if (Input.GetAxis("Fire1") != 0 || Input.GetAxis("Fire2") != 0)
            {
                if (!down)
                    Application.LoadLevel(Application.loadedLevel);
            }
        }

    }
}
