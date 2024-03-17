using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image bar;

    public void SetMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;

        bar.color = gradient.Evaluate(1f);
    }
    public void SetEnergy(float energy)
    {
        slider.value = energy;
        bar.color = gradient.Evaluate(slider.normalizedValue);
    }
}