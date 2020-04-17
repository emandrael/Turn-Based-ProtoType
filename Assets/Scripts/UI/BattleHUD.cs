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
        healthPointSlider.maxValue = unit.MaxHealth;
        healthPointSlider.value = unit.CurrentHealth;
    }

    public void SetHP(Unit unit)
    {
        StartCoroutine(SliderWithTime(unit));
        healthPointSlider.value = unit.CurrentHealth;
    }

    IEnumerator SliderWithTime(Unit unit)
    {
        float timer = 0f;
        float duration = 1f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            healthPointSlider.value = Mathf.Lerp(unit.PreviousHealth, unit.CurrentHealth, timer / duration);
            yield return null;
        }
        yield return null;
    }

}
