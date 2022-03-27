using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocationManager : MonoBehaviour
{
    [Header("Список всех доступных локаций")]
    public List<Location> allLocations = new List<Location>();

    private int currentLocation = -1;

    /// <summary>
    ///     Информация о текущей локации
    /// </summary>
    public Location currentLocationData;

    /// <summary>
    ///     События при смене локации
    /// </summary>
    [Header("События")]
    public UnityEvent onLocationChanged;  

    /// <summary>
    ///     События при прохождении ВСЕХ локаций
    /// </summary>
    public UnityEvent onLocationsEnded;

    private void Start()
    {
        ChangeLocation();
    }

    public void ChangeLocation()
    {
        currentLocation++;

        if (currentLocation >= allLocations.Count)
        {
            // Значит все локации закончились
            onLocationsEnded?.Invoke();
        }

        currentLocationData = allLocations[currentLocation];

        onLocationChanged?.Invoke();
    }

    public GameObject GetRandomEnemy()
    {
        return currentLocationData.GetRandomEnemyPrefab();
    }

    public int GetRandomSkill()
    {
        return currentLocationData.GetRandomSkill();
    }
}
