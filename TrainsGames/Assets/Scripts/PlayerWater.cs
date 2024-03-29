﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWater : MonoBehaviour {
    public float startingWater = 100;
    public float waterLevel;
    public Gauge waterGauge;
    public GameObject boom;
    public Animator HUD;


    TrainController playerMovement;
    bool isDead;                                             


    void Awake()
    {
        playerMovement = GetComponentInChildren<TrainController>();
        waterLevel = startingWater;
    }


    bool warned = false;
    public void loseWater(float amount)
    {
        if (amount <= 0)
            return;
        waterLevel -= amount;

        waterGauge.value = startingWater - waterLevel;

        if(waterLevel <= 20 && !warned)
        {
            HUD.SetTrigger("LowWater");
            warned = true;
        }
        else
        {
            HUD.ResetTrigger("LowWater");
        }

        if (waterLevel <= 0 && !isDead)
        {
            Death();
        }
    }

    public void fillWater()
    {
        fillWater(startingWater);
    }
    public void fillWater(float amount)
    {
        if (amount <= 0 || isDead)
            return;

        waterLevel += amount;
        waterLevel = Mathf.Min(waterLevel, startingWater);

        waterGauge.value = startingWater - waterLevel;
        warned = false;
    }


    void Death()
    {
        isDead = true;
        Instantiate(boom, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        var guns = GetComponentsInChildren<Gun>();
        foreach (var gun in guns)
        {
            gun.enabled = false;
        }
    }
}
