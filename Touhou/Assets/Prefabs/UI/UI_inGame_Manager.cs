using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_inGame_Manager : MonoBehaviour
{
    [SerializeField] private Slider currentEnergyProgressBar;
    [SerializeField] private Slider currentHealthProgressBar;
    [SerializeField] private Image energySliderFillImage;
    [SerializeField] private Image healthSliderFillImage;
    [SerializeField] private Text timerText;
    [SerializeField] private Text weaponLevel;

    private int elapsedSeconds = 00;
    private int elapsedMinutes = 00;

    public void setEnergyValue(float currentShootEnergy)
    {
        currentEnergyProgressBar.value = currentShootEnergy;
    }
    public void setWeaponLevel(int newWeaponLevel)
    {
        weaponLevel.text = "Arme niveau " + newWeaponLevel.ToString();
    }

    public void setEnergyMax(float energyMax)
    {
        currentEnergyProgressBar.maxValue = energyMax;
        currentEnergyProgressBar.GetComponent<RectTransform>().sizeDelta = new Vector2(energyMax, 20f);
    }

    public void setHealthColor(bool godMode)
    {
        if(godMode)
        {
            healthSliderFillImage.color = Color.yellow;
        }
        else
        {
            healthSliderFillImage.color = Color.red;
        }
    }

    public void setHealthValue(float currentHealthEnergy)
    {
        currentHealthProgressBar.value = currentHealthEnergy;
    }



    public void setEnergyColor(bool infiny)
    {
        if(infiny)
        {
            energySliderFillImage.color = Color.yellow;
        }
        else
        {
            energySliderFillImage.color = Color.magenta;
        }
    }

    void Awake()
    {
        StartCoroutine("timerIncrease");
    }

 IEnumerator timerIncrease()
    {
        while (0 == 0)
        {
            if (elapsedSeconds < 59)
            {
                elapsedSeconds++;
            }
            else
            {
                elapsedMinutes++;
                elapsedSeconds = 0;
            }
            timerText.text = string.Format("{0:00}:{1:00}", elapsedMinutes, elapsedSeconds);
            yield return new WaitForSeconds(1f);
        }
    }
}
