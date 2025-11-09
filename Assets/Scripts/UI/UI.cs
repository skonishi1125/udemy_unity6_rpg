using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToopTip skillToolTip;
    public UI_SkillTree skillTree;
    private bool skillTreeEnabled;

    private void Awake()
    {
        skillToolTip = GetComponentInChildren<UI_SkillToopTip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true); // trueとすると、Hierarchyが非表示になっている場合でも取得できる
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }

}
