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
    [HideInInspector()] public List<UsableSkill> skills = new List<UsableSkill>();

    /// <summary>
    ///     Навыки персонажа (изучаемые)
    /// </summary>
    [HideInInspector()] public List<LearningSkill> learningSkills = new List<LearningSkill>();

    /// <summary>
    ///     События вызываемые когда мы получили новый опыт для навыка
    /// </summary>
    public UnityEvent onSkillsUpdated;

    public UnityEvent onSkillCooldownUpdated;

    private float cooldownsRefreshTimer = 0.01f;

    private Coroutine cooldownsCoroutine;

    private List<int> blackList = new List<int>(); 

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
        if (Input.GetKeyDown(KeyCode.U))
            EvolveSkills();

        // Нажатия на слоты навыков
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

        if (skills[index].currentCooldown == 0)
        {
            skills[index].currentCooldown = skills[index].cooldown;
            skills[index].skill.UseSkill();
        }
    }

    public void EvolveSkills()
    {
        List<UsableSkill> evolvedSkills = new List<UsableSkill>();

        // Проходимся по каждому навыку
        for (int i = 0; i < skills.Count; i++)
        {
            // Получаем ID навыка для эволюции
            var skillEvolveID = skills[i].skill.skillData.evoleToSkillID;

            // Получаем сам объект навыка для эволюции
            var evolvedSkill = library.GetSkillDataByID(skillEvolveID);

            // Если навык не нашли, то оставляем прошлый навык, значит его нельзя эвольвнуть
            if (evolvedSkill == null || skillEvolveID == -1)
            {
                evolvedSkills.Add(new UsableSkill(skills[i].skill));
            }
            // Иначе заменяем навык на новый
            else
            {
                evolvedSkills.Add(new UsableSkill(evolvedSkill));
                blackList.Add(skills[i].skill.skillData.skillID);
            }
        }

        // Удаляем старые навыки
        skills.Clear();

        // Добавляем новые навыки
        foreach (var skill in evolvedSkills)
            skills.Add(skill);

        // Обновляем UI и т.п.
        onSkillsUpdated?.Invoke();
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

        bool existed = false;

        // Проверяем если у нас уже изучен навык
        foreach (var item in skills)
        {
            if (item.skill.skillData.skillID == skillID)
                existed = true;
        }

        // Проверяем не эволюционировали ли мы навык
        foreach (var item in blackList)
        {
            if (item == skillID) return;
        }

        // Если навык не существует в уже изученных проверяем дальше
        if (existed == false)
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
        skills.Add(new UsableSkill(newSkill));
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
            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].currentCooldown -= cooldownsRefreshTimer;
                if (skills[i].currentCooldown <= 0) skills[i].currentCooldown = 0;
            }

            onSkillCooldownUpdated?.Invoke();

            yield return new WaitForSeconds(cooldownsRefreshTimer);
        }
    }
}