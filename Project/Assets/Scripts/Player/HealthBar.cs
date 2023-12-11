using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float maxHealth = 100f;
    private float actualHealth;

    void Start()
    {
        actualHealth = maxHealth;

        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = actualHealth;
        }
    }

    public void UpdateHealthBar()
    {
        actualHealth = Mathf.Clamp(actualHealth, 0f, maxHealth);

        // Mettez à jour la barre de vie
        if (slider != null)
        {
            slider.value = actualHealth;
        }
    }

    public void ReduceHealth(float quantity)
    {
        actualHealth -= quantity;
        UpdateHealthBar();
    }

    public void AddHealth(float quantity)
    {
        actualHealth += quantity;
        UpdateHealthBar();
    }
}
