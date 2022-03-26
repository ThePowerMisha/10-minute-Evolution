
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public HeathSystem healtSystem;
    public Image hpBar;
    public Text hpText;
    public Image manaBar;
    public Text manaText;


    private void Start()
    {
        // HpBar = GetComponent<Image>();
        // HpText = GetComponent<Text>();
        // ManaBar = GetComponent<Image>();
        // ManaText = GetComponent<Text>();
    }

    private void Update()
    {
        hpText.text = math.ceil(healtSystem.health) + "/" + healtSystem.maxHealth;
        hpBar.fillAmount = healtSystem.health / healtSystem.maxHealth;
    }

    private void OnDestroy()
    {
        hpText.text = "0";
        hpBar.fillAmount = 0;
    }
}
