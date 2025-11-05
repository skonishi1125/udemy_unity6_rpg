using Unity.Cinemachine;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetPhysicalDamage(out bool isCrit)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f; // 0.3% per AGI

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue();
        float critPower = (baseCritPower + bonusCritPower) / 100; // (ex: 150 / 100 = 1.5f - multiplier)

        isCrit = Random.Range(0, 100) < baseCritChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;

    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue();
        float totalArmor = baseArmor + bonusArmor;

        // Mathf.Clamp( 1 - armorReduction, 0, 1); の短縮形
        float reductionMultiplier = Mathf.Clamp01( 1 - armorReduction); // 1 - .4f = .6f
        float effectiveArmor = totalArmor * reductionMultiplier;


        float mitigation = effectiveArmor / (effectiveArmor + 100); // 単純にアーマーを重ねて、物理防御が上がりすぎるのを防ぐ式
        float mitigationCap = .85f; // 最大85%まで軽減ができる
        
        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        float finalReduction = offense.armorReduction.GetValue() / 100;

        return finalReduction;
    }


    public float GetEvasion()
    {
        // Statクラスから作ったevasionで、GeValue()メソッドを呼ぶ。 
        // -> baseDamageはUnity上のインスペクタで割り当てることができるので、そっちで割り当てた値をdebuglogに出す
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85; // 回避可能な最大の確率

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap); // 0 - 85の間でなければ、85を返すような処理

        return finalEvasion;
    }


    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

}
