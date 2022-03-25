using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Навык может быть любым
    У каждого навыка всегда есть имя и описание 
    Каждый навык может обладать разными свойствами
    Используем паттерн для перезаписи
    У нас будет общий класс навыка, который всё реализует, но он ничего не делает
    Все остальные навыки пойдут от него и буду вызываться через него
 */

[System.Serializable]
public class LearningSkill
{
    /// <summary>
    ///     Идентификатор навыка
    /// </summary>
    public int skillID;

    /// <summary>
    ///     Необходимое количество опыта для получения
    /// </summary>
    public float skillExpToObtain;

    /// <summary>
    ///     Уже полученное количество опыта
    /// </summary>
    public float skillGainedExp;

    /// <summary>
    ///     События вызываемые при получении навыка
    /// </summary>
    public IntUnityEvent onSkillLearned;

    public LearningSkill() { }

    public LearningSkill(int _skillID, float _expToObtain) 
    {
        this.skillID = _skillID;
        this.skillExpToObtain = _expToObtain;
        this.skillGainedExp = 0;
        this.onSkillLearned = new IntUnityEvent();
    }

    /// <summary>
    ///     Функция добавления количества опыта к навыку
    /// </summary>
    /// <param name="exp">
    ///     Количество полученного опыта
    /// </param>
    public void GainExp(float exp)
    {
        skillGainedExp += exp;

        if (skillGainedExp >= skillExpToObtain)
        {
            LearnSkill();
        }
    }

    /// <summary>
    ///     Функция изучения навыка. Вызывает связанные события
    /// </summary>
    public void LearnSkill()
    {
        onSkillLearned?.Invoke(this.skillID);
    }

    /// <summary>
    ///     Возвращает соотношение текущего уровня к необходимому
    /// </summary>
    /// <returns></returns>
    public float GetExpDelta()
    {
        return skillGainedExp / skillExpToObtain;
    }
}

[System.Serializable]
public class SkillBase
{
    /// <summary>
    ///     Основные характеристики навыка
    /// </summary>
    public SkillData skillData;

    /// <summary>
    ///     События вызываемые при использовании навыка
    /// </summary>
    public IntUnityEvent onSkillUsed;

    /// <summary>
    ///     События вызываемые при активации навыка
    /// </summary>
    public IntUnityEvent onSkillActivated;

    public SkillBase() { }

    /// <summary>
    ///     Вызывает главное действие навыка и все связанные ивенты
    /// </summary>
    public virtual void UseSkill()
    {
        onSkillUsed?.Invoke(skillData.skillID);
    }
}