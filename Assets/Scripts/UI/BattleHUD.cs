using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    private int previousHealth;
    public Slider healthPointSlider;
    public Slider actionPointSlider;

    public void SetHUD(Unit unit)
    {
        // Sets the initial health for the unit.
        healthPointSlider.maxValue = unit.MaxHealth;
        healthPointSlider.value = unit.CurrentHealth;
        actionPointSlider.maxValue = unit.MaxActionPoints;
        actionPointSlider.value = unit.CurrentActionPoints;
    }

    public void SetHP(Unit unit)
    {
        // This is a coroutine that changes the health for the unit.
        StartCoroutine(SliderWithTimeHP(unit,healthPointSlider, true));
        healthPointSlider.value = unit.CurrentHealth;
    }

    public void SetAP(Unit unit)
    {
        StartCoroutine(SliderWithTimeAP(unit, actionPointSlider));
        actionPointSlider.value = unit.CurrentActionPoints;
    }

    public void SetHealedHP(Unit unit)
    {
        // This is a coroutine that changes the health for the unit.
        StartCoroutine(SliderWithTimeHP(unit, healthPointSlider, true));
        healthPointSlider.value = unit.CurrentHealth;
    }


    IEnumerator SliderWithTimeHP(Unit unit, Slider slider, bool isHealing)
    {
        // This is the enumerator that allows the animation of the hud.
        float timer = 0f;
        float duration = 1f;
        if (isHealing)
        {
            Debug.Log(unit.PreviousHealth + " " + unit.CurrentHealth);
            while (timer < duration)
            {
                timer += Time.deltaTime;
                slider.value = Mathf.Lerp(unit.PreviousHealth, unit.CurrentHealth, timer / duration);
                yield return null;
            }
        }
        yield return null;
    }

    IEnumerator SliderWithTimeAP(Unit unit, Slider slider)
    {
        // This is the enumerator that allows the animation of the hud.
        float timer = 0f;
        float duration = 1f;
        Debug.Log(unit.PreviousActionPoints + " " + unit.CurrentActionPoints);
        while (timer < duration)
        {
            timer += Time.deltaTime;
            slider.value = Mathf.Lerp(unit.PreviousActionPoints, unit.CurrentActionPoints, timer / duration);
            yield return null;
        }
        
        yield return null;
    }

}
