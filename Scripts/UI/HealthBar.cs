using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI BossName;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI SpellCard;

    public Slider slider;
    public void SetMaxHealth(float health)
    {
        //slider.maxValue = health;
        StartCoroutine(HealthRegen(health));
        //slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetLife(int life)
    {
        LifeText.text = life.ToString();
    }

    public void SetName(string name)
    {
        BossName.text = name;
    }

    public void SetTimer(int time)
    {
        TimerText.text = time.ToString();
    }

    public void SetSpellCard(string spellName)
    {
        
        SpellCard.text = spellName;
    }


    public IEnumerator HealthRegen(float health)
    {
        float duration = 2f;
        float elapsed = 0f;

        slider.maxValue = health;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(1,health, elapsed/duration); //체력 1부터 시작

            yield return null;
        }

    }

}
