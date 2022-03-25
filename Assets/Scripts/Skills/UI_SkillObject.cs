using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillObject : MonoBehaviour
{
    [Header("Ссылка на прогресс бар")]
    public Image progressBar;

    [Header("Ссылка на объект иконки")]
    public Image icon;

    public void SetObject(Sprite sprite, float expValue)
    {
        progressBar.fillAmount = expValue;
        icon.sprite = sprite;
    }
}
