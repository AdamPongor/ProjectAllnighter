using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;
    private float maxValue = 100f;
    public float tick;
    public bool canRegen;
    public float currentValue{
        set{
            _currentStamina = value;   
        }
        get{
            return _currentStamina;
        }
    }

    public float MaxValue { 
        get => maxValue; set
        {
            maxValue = value;
            currentValue = maxValue;
            UpdateSlider();
        } 
    }

    private float _currentStamina;

    private WaitForSeconds regenTick;

    private Coroutine regen;

    void Start()
    {
        regenTick = new WaitForSeconds(tick);
        currentValue = MaxValue;
        slider.maxValue = MaxValue;
        slider.value = MaxValue;
    }

    public void Use(float amount)
    {
        if (currentValue - amount >= 0)
        {
            currentValue -= amount;
        }
        else
        {
            currentValue = 0;
        }

        UpdateSlider();
        if (regen != null)
            StopCoroutine(regen);

        regen = StartCoroutine(Regen());
    }

    public void Add(float Amount)
    {
        if ((currentValue + Amount) > MaxValue)
        {
            currentValue = MaxValue;
        }
        else
        {
            currentValue += Amount;
        }
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        slider.value = 100 * (currentValue / maxValue);
    }

    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        while((currentValue < MaxValue) && canRegen)
        {
            currentValue += 1;
            UpdateSlider();
            yield return regenTick;
        }
        regen = null;
            
    }

    public bool isEnough(float amount)
    {
        if(_currentStamina < amount)
            return false;
        else
            return true;
    }
}
