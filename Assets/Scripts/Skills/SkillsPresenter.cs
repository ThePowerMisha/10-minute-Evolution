using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPresenter : MonoBehaviour
{
    [Header("Ссылка на систему навыков")]
    public SkillsSystem skillSystem;

    [Header("Префаб объекта изучения навыка")]
    public GameObject learningSkillPrefab;

    [Header("Родитель изучаемых навыков")]
    public Transform learningSkillsParent;

    [Header("Префаб объекта изученного навыка")]
    public GameObject skillPrefab;

    [Header("Родитель изученных навыков")]
    public Transform skillsParent;

    public List<Image> cooldownImages = new List<Image>();

    private void Awake()
    {
        skillSystem.onSkillsUpdated.AddListener(UpdateUISkills);
        skillSystem.onSkillCooldownUpdated.AddListener(UpdateCooldowns);
    }

    private void Start()
    {
        InitEmptySkillSlots();
    }

    private void InitEmptySkillSlots()
    {
        if (skillSystem.skills.Count < 5)
        {
            for (int i = 0; i < 5 - skillSystem.skills.Count; i++)
            {
                Instantiate(skillPrefab, skillsParent);
            }
        }
    }

    public void UpdateCooldowns()
    {
        for (int i = 0; i < cooldownImages.Count; i++)
        {
            cooldownImages[i].fillAmount = skillSystem.skills[i].GetCooldownDelta();
        }
    }

    private void UpdateUISkills()
    {
        // Очищаем уже имеющиеся навыки
        for (int i = learningSkillsParent.childCount - 1; i >= 0; i--)
        {
            Destroy(learningSkillsParent.GetChild(i).gameObject);
        }

        // Создаём новые объекты навыков
        for (int i = 0; i < skillSystem.learningSkills.Count; i++)
        {
            // Инициализируем новый объект
            var skillGO = Instantiate(learningSkillPrefab, learningSkillsParent);

            // Устанавливаем новое значение прогресс бара
            skillGO.GetComponent<UI_SkillObject>().SetObject(skillSystem.library.GetSkillDataByID(skillSystem.learningSkills[i].skillID).skillData.skillIcon, skillSystem.learningSkills[i].GetExpDelta());
        }

        // =======================================================================

        for (int i = skillsParent.childCount - 1; i >= 0; i--)
        {
            Destroy(skillsParent.GetChild(i).gameObject);
        }

        cooldownImages.Clear();

        for (int i = 0; i < skillSystem.skills.Count; i++)
        {
            var skillGO = Instantiate(skillPrefab, skillsParent);

            skillGO.transform.GetChild(0).GetComponent<Image>().sprite = skillSystem.skills[i].skill.skillData.skillIcon;
            cooldownImages.Add(skillGO.transform.GetChild(1).GetComponent<Image>());
        }

        // Дорисовываем пустые клетки если они нужны нам
        InitEmptySkillSlots();

        UpdateCooldowns();
    }
}