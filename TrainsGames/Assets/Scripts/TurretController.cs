using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{

    public Gun leftGun;
    public Gun rightGun;

    public Gauge gunGauge;

    float gunCooldown = 0;

    const float coolDownMultiplier = 1.0f;
    const float overheatCoolDownMultiplier = 5f;
    const float heatUpMultiplier = 3.0f;

    bool overheated = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool leftFiring = Input.GetAxis("Fire1") != 0;
        bool rightFiring = Input.GetAxis("Fire2") != 0;
        if (overheated)
        {
            Debug.Log("overheated");
            if (!leftFiring && !rightFiring)
            {
                gunCooldown -= Time.deltaTime * overheatCoolDownMultiplier;
            }

            if(gunCooldown <= 0)
            {
                gunCooldown = 0;
                overheated = false;
            }
        }
        else
        {
            leftGun.setFire(leftFiring);
            rightGun.setFire(rightFiring);

            if (leftFiring)
            {
                gunCooldown += Time.deltaTime * heatUpMultiplier;
            }

            if (rightFiring)
            {
                gunCooldown += Time.deltaTime * heatUpMultiplier;
            }

            if (!leftFiring && !rightFiring)
            {
                gunCooldown -= Time.deltaTime * coolDownMultiplier;
            }
            overheated = gunCooldown >= gunGauge.maxValue;
        }

        gunCooldown = Mathf.Min(gunGauge.maxValue, gunCooldown);
        gunCooldown = Mathf.Max(0, gunCooldown);
        gunGauge.value = gunCooldown;
    }
}
