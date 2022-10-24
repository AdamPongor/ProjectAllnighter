using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;
    private float maxStamina = 100f;
    public float CurrentStamina{
        set{
            _currentStamina = value;   
        }
        get{
            return _currentStamina;
        }
    }

    private float _currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.02f);

    private Coroutine regen;

    void Start()
    {
        CurrentStamina = maxStamina;
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
        Debug.Log(CurrentStamina);
    }

    public void Use(float amount)
    {
        if(CurrentStamina - amount>= 0)
        {
            CurrentStamina -= amount;
            slider.value = CurrentStamina;
            if(regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(Regen());
            
            
        }
        Debug.Log(CurrentStamina);

    }
    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        while(CurrentStamina < maxStamina)
        {
            CurrentStamina += maxStamina / 100;
            slider.value = CurrentStamina;
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
