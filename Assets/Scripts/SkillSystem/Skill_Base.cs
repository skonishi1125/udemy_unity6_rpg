using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public Player player {  get; private set; }

    [Header("General details")]
    [SerializeField] SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;
    [SerializeField] protected float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
        // ゲーム開始時、すぐにスキルが使えるようにしておく
        lastTimeUsed = lastTimeUsed - cooldown;
    }

    public virtual void TryUseSkill()
    {

    }


    public void SetSkillUpgrade(UpgradeData upgrade)
    {
        upgradeType = upgrade.upgradeType;
        cooldown = upgrade.cooldown;
    }

    public bool CanUseSkill()
    {
        if (upgradeType == SkillUpgradeType.None)
            return false;

        if (OnCoolDown())
        {
            Debug.Log("On Cooldown");
            return false;
        }

        // TODO: MPがあるか
        // スキルをアンロックしているかなども確認

        return true;
    }

    protected bool Unlocked(SkillUpgradeType upgradeToCheck) => upgradeType == upgradeToCheck;


    // ゲーム開始後15秒後、5秒のcooldownスキルを使った場合、 20秒まで使用不可
    protected bool OnCoolDown() => Time.time < lastTimeUsed + cooldown;
    public void SetSkillOnCooldown() => lastTimeUsed = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed = lastTimeUsed + cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;

}
