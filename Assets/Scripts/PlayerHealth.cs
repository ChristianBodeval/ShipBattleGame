using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float StartingHealth = 100f;
    private float CurrentHealth;
    private bool Dead;


    private void Awake()
    {
       
    }


    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
        Dead = false;

        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        CurrentHealth -= amount;

        SetHealthUI();
        if (CurrentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
       
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        Dead = true;

      

        gameObject.SetActive(false);
    }
}