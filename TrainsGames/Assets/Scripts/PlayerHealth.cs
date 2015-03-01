using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                         
    public int currentHealth;                                
    public Slider healthSlider;
    public Gauge healthGauge;                             
    public Image damageImage;                                
    public float flashSpeed = 5f;                            
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public GameObject boom;


    TrainController playerMovement;                          
    bool isDead;                                             
    bool damaged;                                            


    void Awake()
    {
        playerMovement = GetComponentInChildren<TrainController>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        healthGauge.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        Instantiate(boom, transform.position, transform.rotation);
        playerMovement.enabled = false;
        var guns = GetComponentsInChildren<Gun>();
        foreach (var gun in guns)
        {
            gun.enabled = false;
        }
    }
}