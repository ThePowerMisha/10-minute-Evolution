using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new SkillData", menuName = "Create/SkillData")]
public class SkillData : ScriptableObject
{
    /// <summary>
    ///     Иконка навыка
    /// </summary>
    [Header("Иконка навыка")] public Sprite skillIcon;

    /// <summary>
    ///     Идентификатор навыка (все навыки связываются и ищутся по нему)
    /// </summary>
    [Header("Идентификатор навыка")] public int skillID;

    /// <summary>
    ///     Наименование навыка
    /// </summary>
    [Header("Наименование навыка")] public string skillName;

    /// <summary>
    ///     Подробное описание навыка
    /// </summary>
    [Header("Подробное описание навыка")]
    [TextArea(5, 10)]
    public string skillDescr;

    /// <summary>
    ///     Кулдаун навыка. Время перезарядки в секундах
    /// </summary>
    [Header("Кулдаун навыка. Время перезарядки в секундах")]
    public float skillCooldown;

    /// <summary>
    ///     Необходимое количество опыта до получения
    /// </summary>
    [Header("Необходимый опыт для получения")]
    public float skillExpToObtain;

    /// <summary>
    ///     Идентификатор навыка на который заменится текущий навык после эволюции
    /// </summary>
    [Header("Идентификатор навыка для эволюции")]
    public int evoleToSkillID = -1;
}