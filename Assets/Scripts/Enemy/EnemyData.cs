using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


[System.Serializable]
[CreateAssetMenu(fileName = "new EnemyData", menuName = "Create/EnemyData")]
public class EnemyData : ScriptableObject
{
    /// <summary>
    ///     Спрайт противника
    /// </summary>
    [Header("Спрайт Противника")] public Sprite enemySprite;

    /// <summary>
    ///     Идентификатор противника 
    /// </summary>
    [Header("Идентификатор противника")] public int enemyID;

    /// <summary>
    ///     Имя противника
    /// </summary>
    [Header("Имя противника")] public string enemyName;

    /// <summary>
    ///     Подробное описание противника
    /// </summary>
    [Header("Подробное описание противника")] [TextArea(5, 10)]
    public string enemyDescr;

    /// <summary>
    /// Здоровье противника
    /// </summary>
    [Header("Здоровье противника")] public float enemyHealth;

    /// <summary>
    /// Снаряд которым будет стрелять противник
    /// </summary>
    [Header("Снаряд которым стреляет")] public Projectile enemyProjectile;
    
    /// <summary>
    /// Идентификатор навыка которым может стрелять противник
    /// </summary>
    [Header("Идентификатор навыка противника")] public int enemySkillId;
    
    /// <summary>
    ///     Перезарядка стрельбы 
    /// </summary>
    [Header("Перезарядка стрельбы")] public float enemyCoolDown;
    /// <summary>
    /// Скорость противника
    /// </summary>
    [Header("Скорость противника")] public float enemySpeed;
    /// <summary>
    /// Очки при убийстве
    /// </summary>
    [Header("Очки при убийстве")] public int enemyScore;
    /// <summary>
    /// Золото при убийстве
    /// </summary>
    [Header("Золото при убийстве")] public int enemyCoins;
}
