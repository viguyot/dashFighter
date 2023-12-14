using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public float maxEnergy = 100f;
    private float actualEnergy;

    void Start()
    {
        actualEnergy = maxEnergy;

        if (slider != null)
        {
            slider.maxValue = maxEnergy;
            slider.value = actualEnergy;
        }
    }

    public void UpdateEnergyBar()
    {
        actualEnergy = Mathf.Clamp(actualEnergy, 0f, maxEnergy);

        // Mettez à jour la barre de vie
        if (slider != null)
        {
            slider.value = actualEnergy;
        }
    }

    public void ReduceEnergy(float quantity)
    {
        actualEnergy -= quantity;
        if (actualEnergy < 0)
            actualEnergy = 0;
        UpdateEnergyBar();
    }

    public void AddEnergy(float quantity)
    {
        actualEnergy += quantity;
        if (actualEnergy > maxEnergy)
            actualEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    public float GetEnergy()
    {
        return actualEnergy;
    }
}
