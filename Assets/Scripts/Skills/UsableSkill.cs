using UnityEngine;

/// <summary>
///     Класс для навыка игрока, который он может использовать
/// </summary>
public class UsableSkill : MonoBehaviour
{
    public SkillBase skill;

    public float cooldown;
    public float currentCooldown;

    public float GetCooldownDelta()
    {
        return currentCooldown / cooldown;
    }

    public UsableSkill(SkillBase _skill)
    {
        skill = _skill;
        cooldown = _skill.skillData.skillCooldown;
        currentCooldown = 0;
    }
}