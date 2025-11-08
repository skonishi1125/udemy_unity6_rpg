using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#9D9D9D";
    private Color lastColor;
    public bool isUnlocked;
    public bool isLocked;

    private void Awake()
    {
        UpdateIconColor(GetColorByHex(lockedColorHex)); // グレーみたいな色を被せて、アンロックっぽい見た目にする
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white); // ロック解除っぽい見た目にする

        // プレイヤーのSkillManagerにアクセス
        // SkillManagerのSkillをアンロックする
    }
    private bool CanBeLocked()
    {
        if (isLocked || isUnlocked)
            return false;

        return true;
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
        else
            Debug.Log("Cannot be Unlocked..");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUnlocked == false)
            UpdateIconColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isUnlocked == false)
            UpdateIconColor(lastColor);

    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }

}
