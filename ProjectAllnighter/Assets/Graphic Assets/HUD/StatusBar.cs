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

    private float _currentStamina;

    private WaitForSeconds regenTick;

    private Coroutine regen;

    void Start()
    {
        regenTick = new WaitForSeconds(tick);
        currentValue = maxValue;
        slider.maxValue = maxValue;
        slider.value = maxValue;
        Debug.Log(currentValue);
    }

    public void Use(float amount)
    {
        if(currentValue - amount>= 0)
        {
            currentValue -= amount;
            slider.value = currentValue;
            if(regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(Regen());
            
            
        }
        Debug.Log(currentValue);
    }

    public void Add(float Amount)
    {
        currentValue += Amount;
        slider.value = currentValue;
    }

    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        while((currentValue < maxValue) && canRegen)
        {
            currentValue += maxValue / 100;
            slider.value = currentValue;
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
