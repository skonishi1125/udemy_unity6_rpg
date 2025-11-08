using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill Data - ")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;

    [Header("Skill description")]
    public string displayName;
    [TextArea]
    public string description;
    public Sprite icon;

    // アンロック済みかどうか

}
