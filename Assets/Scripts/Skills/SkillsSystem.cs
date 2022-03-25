using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillsSystem : MonoBehaviour
{
    [Header("Ссылка на библиотеку со всеми навыками")]
    public SkillLibrary library;

    /// <summary>
    ///     Навыки персонажа (уже изученные)
    /// </summary>
    [HideInInspector()] public List<SkillBase> skills = new List<SkillBase>();

    /// <summary>
    ///     Навыки персонажа (изучаемые)
    /// </summary>
    [HideInInspector()] public List<LearningSkill> learningSkills = new List<LearningSkill>();

    /// <summary>
    ///     События вызываемые когда мы получили новый опыт для навыка
    /// </summary>
    public UnityEvent onSkillsUpdated;

    private float cooldownsRefreshTimer = 0.01f;

    private float[] cooldowns = new float[5];

    private Coroutine cooldownsCoroutine;

    private void Start()
    {
        cooldownsCoroutine = StartCoroutine(refreshCooldowns());
    }

    private void OnDestroy()
    {
        StopCoroutine(cooldownsCoroutine);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GainExpToSkill(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            GainExpToSkill(1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSkill(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSkill(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSkill(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseSkill(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseSkill(4);
        }
    }

    /// <summary>
    ///     Использование конкретного навыка
    /// </summary>
    /// <param name="index"></param>
    public void UseSkill(int index)
    {
        Debug.Log("Use Skill at slot -> " + (index + 1));

        if (index >= skills.Count) return;

        if (cooldowns[index] == 0)
        {
            skills[index].UseSkill();
        }
    }

    /// <summary>
    ///     Вызывает обработки для получения нового навыка
    /// </summary>
    /// <param name="skillID">
    ///     Идентификатор получаемого навыка
    /// </param>
    /// <param name="exp">
    ///     Количество опыта для навыка
    /// </param>
    public void GainExpToSkill(int skillID, int exp)
    {
        // Находим наш навык в изучаемых
        var skill = learningSkills.Find(x => x.skillID == skillID);

        // Проверяем если у нас уже изучен навык
        var existed = skills.Find(x => x.skillData.skillID == skillID);

        // Если навык не существует в уже изученных проверяем дальше
        if (existed == null)
        {
            // Если не нашли навык в изучаемых и в изученных его тоже нет
            if (skill == null)
            {
                var librarySkill = library.GetSkillDataByID(skillID);

                if (librarySkill != null)
                {
                    var newSkill = new LearningSkill(skillID, librarySkill.skillData.skillExpToObtain);

                    newSkill.onSkillLearned.AddListener(ActionOnSkillLearned);

                    learningSkills.Add(newSkill);
                }
            }
            else
            {
                skill.GainExp(exp);
            }
        }
        
        onSkillsUpdated?.Invoke();
    }

    private void ActionOnSkillLearned(int skillID)
    {
        AddNewSkill(skillID);

        RemoveLearningSkill(skillID);
    }

    private void AddNewSkill(int skillID)
    {
        var newSkill = library.GetSkillDataByID(skillID);
        skills.Add(newSkill);
    }

    private void RemoveLearningSkill(int skillID)
    {
        int index = learningSkills.FindIndex(x => x.skillID == skillID);
        learningSkills.RemoveAt(index);
    }

    private IEnumerator refreshCooldowns()
    {
        while (true)
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                if (cooldowns[i] > 0) cooldowns[i] -= cooldownsRefreshTimer;
                if (cooldowns[i] < 0) cooldowns[i] = 0;
            }

            yield return new WaitForSeconds(cooldownsRefreshTimer);
        } 
    }
}
