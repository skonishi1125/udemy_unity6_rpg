using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler connectHandler;

    [Header("Unlock detals")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skill details")]
    public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    [SerializeField] private string lockedColorHex = "#9D9D9D";
    private Color lastColor;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectHandler = GetComponent<UI_TreeConnectHandler>();

        UpdateIconColor(GetColorByHex(lockedColorHex)); // グレーみたいな色を被せて、アンロックっぽい見た目にする
    }

    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(GetColorByHex(lockedColorHex));

        skillTree.AddSkillPoints(skillData.cost);
        connectHandler.UnlockConnectionImage(false);

        // skill manager and reset skill
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white); // ロック解除っぽい見た目にする
        LockConflictNodes(); // 取得時、競合スキルがあればそちらのフラグを変更し、取得ができないようにする。

        skillTree.RemoveSkillPoints(skillData.cost);
        connectHandler.UnlockConnectionImage(true);

        // SkillManager側で、Skillをアンロックした場合の状態を決める
        skillTree.skillManager.GetSkillByType(skillData.skillType).SetSkillUpgrade(skillData.upgradeData);

    }
    private bool CanBeLocked()
    {
        // すでにロックされているか、アンロックされている場合はfalseを返す
        if (isLocked || isUnlocked)
            return false;

        if (skillTree.EnoughSkillPoints(skillData.cost) == false)
            return false;

        // 必要なスキルをアンロックしているか
        foreach (var node in neededNodes)
        {
            if (node.isUnlocked == false)
                return false;
        }

        // 競合スキルがアンロックされていたら、取得できなくする(どちらか一方しか取れないようにする)
        foreach (var node in conflictNodes)
        {
            if (node.isUnlocked)
                return false;
        }

        return true;
    }

    // 競合するスキルを取得不可とする。
    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
            node.isLocked = true;
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
            return;

        lastColor = skillIcon.color;

        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeLocked())
            Unlock();
        else if (isLocked)
            ui.skillToolTip.LockedSkillEffect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);

        if (isUnlocked || isLocked)
            return;

        ToggleNodeHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (isUnlocked || isLocked)
            return;

        ToggleNodeHighlight(false);

    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * .9f; highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastColor;

        UpdateIconColor(colorToApply);

    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }

    private void OnDisable()
    {
        if (isLocked)
            UpdateIconColor(GetColorByHex(lockedColorHex));

        if (isUnlocked)
            UpdateIconColor(Color.white);
    }

    private void OnValidate()
    {
        if (skillData == null)
            return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.cost;
        gameObject.name = "UI_TreeNode - " + skillData.displayName;
    }


}
