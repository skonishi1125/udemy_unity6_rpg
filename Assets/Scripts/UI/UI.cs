using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToopTip skillToolTip;


    private void Awake()
    {
        skillToolTip = GetComponentInChildren<UI_SkillToopTip>();
    }

}
