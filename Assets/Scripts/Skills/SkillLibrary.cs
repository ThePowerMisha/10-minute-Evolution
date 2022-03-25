using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillLibrary : MonoBehaviour
{
    public List<SkillBase> allSkills = new List<SkillBase>();

    public SkillBase GetSkillDataByID(int skillID)
    {
        var data = allSkills.Find(x => x.skillData.skillID == skillID);

        var newData = new SkillBase();
        newData.skillData = data.skillData;
        newData.onSkillUsed = data.onSkillUsed;
        newData.onSkillActivated = data.onSkillActivated;

        return newData;
    }
}
