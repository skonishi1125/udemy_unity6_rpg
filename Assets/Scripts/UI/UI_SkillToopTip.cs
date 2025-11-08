using TMPro;
using UnityEngine;

public class UI_SkillToopTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirement;

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }

    // オーバーライドする
    public void ShowToolTip(bool show, RectTransform targetRect, Skill_DataSO skillData)
    {
        base.ShowToolTip(show, targetRect);

        if (show == false)
            return;

        skillName.text = skillData.displayName;
        skillDescription.text = skillData.description;
        skillRequirement.text = "Requirements: \n " + " - " + skillData.cost + " skill point.";
    }

}
