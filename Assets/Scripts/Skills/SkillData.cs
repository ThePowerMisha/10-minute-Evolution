using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new SkillData", menuName = "Create/SkillData")]
public class SkillData : ScriptableObject
{
    /// <summary>
    ///     Иконка навыка
    /// </summary>
    [Header("Иконка навыка")]
    public Sprite skillIcon;

    /// <summary>
    ///     Идентификатор навыка (все навыки связываются и ищутся по нему)
    /// </summary>
    [Header("Идентификатор навыка")]
    public int skillID;

    /// <summary>
    ///     Наименование навыка
    /// </summary>
    [Header("Наименование навыка")]
    public string skillName;

    /// <summary>
    ///     Подробное описание навыка
    /// </summary>
    [Header("Подробное описание навыка")]
    [TextArea(5, 10)]
    public string skillDescr;

    /// <summary>
    ///     Кулдаун навыка. Сколько раз в минуту применить навык
    /// </summary>
    [Header("Кулдаун. Сколько раз в минуту")]
    public float skillCooldown;

    /// <summary>
    ///     Необходимое количество опыта до получения
    /// </summary>
    [Header("Необходимый опыт для получения")]
    public float skillExpToObtain;
}
