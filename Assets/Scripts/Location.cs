using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationData", menuName = "Create/LocationData")]
public class Location : ScriptableObject
{
    [Header("Наименование локации")] public string locationName;

    [Header("Навыки которые можно получить на локации")]
    public List<SkillData> locationSkills = new List<SkillData>();

    [Header("Префабы противников на локации")]
    public List<GameObject> enemiesPrefabs = new List<GameObject>();

    /// <summary>
    ///     Возвращает случайный объект префаба противника на локации
    /// </summary>
    /// <returns>
    ///     GameObject - префаб противника
    /// </returns>
    public GameObject GetRandomEnemyPrefab()
    {
        int rndIndex = Random.Range(0, enemiesPrefabs.Count);
        return enemiesPrefabs[rndIndex];
    }

    /// <summary>
    ///     Возвращает случайный навык доступный на локации
    /// </summary>
    /// <returns>
    ///     int - Идентификатор доступного навыка
    /// </returns>
    public int GetRandomSkill()
    {
        int rndIndex = Random.Range(0, locationSkills.Count);
        return locationSkills[rndIndex].skillID;
    }
}
