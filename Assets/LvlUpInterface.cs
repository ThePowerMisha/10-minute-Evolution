using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlUpInterface : MonoBehaviour
{
    public Player player;
    public void ShowMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IncreaseDamage()
    {
        player.damage += 5;
    }

    public void IncreaseHealth()
    {
        player.heathSystem.maxHealth += 20;
    }

    public void Heal()
    {
        player.heathSystem.DealDamage(-20);
    }

    public void DecreaseCoolDown()
    {
        player.cooldown *= 1.2f;
    }
}
