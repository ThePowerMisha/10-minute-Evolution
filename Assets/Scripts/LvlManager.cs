using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LvlManager : MonoBehaviour
{
    [Header("Текущий уровень персонажа")]
    public int playerLvl = 1;

    // Текущий уровень персонажа
    private float playerCurrentExp = 0;

    // На сколько больше опыта нужно получить для следующего уровня
    // Прибавляем эту переменную с каждым новым уровнем
    private float nextLvlModify = 15;

    // Изначальное количество опыта для следующего уровня
    private float nextLvlExp = 10;

    /// <summary>
    ///     События вызываемые при получении нового уровня
    /// </summary>
    public UnityEvent onLvlGained;

    /// <summary>
    ///     События вызываемые при получении опыта
    /// </summary>
    public UnityEvent onExpGained;

    public void AddExp(float exp)
    {
        playerCurrentExp += exp;

        if (playerCurrentExp >= nextLvlExp)
        {
            playerLvl++;
            playerCurrentExp = 0;
            nextLvlExp += nextLvlModify;

            onLvlGained?.Invoke();
        }

        onExpGained?.Invoke();
    }
}
