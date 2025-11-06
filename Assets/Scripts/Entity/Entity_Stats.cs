using UnityEngine;


public class Entity_Stats : MonoBehaviour
{
    public Stat_SetUpSO defaultSetup;

    public Stat_ResourceGroup resources;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;
    public Stat_MajorGroup major;

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();
        float bonusElementalDamage = major.intelligence.GetValue();

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }

        if (lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }


        if (highestDamage <= 0)
        {
            element = ElementType.None;
            return 0;
        }

        float bonusFire = (fireDamage == highestDamage) ? 0 : fireDamage * .5f;
        float bonusIce = (iceDamage == highestDamage) ? 0 : iceDamage * .5f;
        float bonusLightning = (lightningDamage == highestDamage) ? 0 : lightningDamage * .5f;

        float weakerElementsDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;

    }

    public float GetElementaResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * .5f;

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defense.fireRes.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defense.iceRes.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defense.lightningRes.GetValue();
                break;
        }

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f;
        float finalResistance = Mathf.Clamp(resistance, 0, resistanceCap) / 100;

        return finalResistance;


    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1)
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

        return finalDamage * scaleFactor;

    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue();
        float totalArmor = baseArmor + bonusArmor;

        // Mathf.Clamp( 1 - armorReduction, 0, 1); の短縮形
        float reductionMultiplier = Mathf.Clamp01(1 - armorReduction); // 1 - .4f = .6f
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
        float baseMaxHealth = resources.maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public Stat GetStatBytype(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return resources.maxHealth;
            case StatType.HealthRegen: return resources.healthRegen;

            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Intelligence: return major.intelligence;
            case StatType.Vitality: return major.vitality;

            case StatType.AttackSpeed: return offense.attackSpeed;
            case StatType.Damage: return offense.damage;
            case StatType.CritChance: return offense.critChance;
            case StatType.CritPower: return offense.critPower;
            case StatType.ArmorReduction: return offense.armorReduction;

            case StatType.FireDamage: return offense.fireDamage;
            case StatType.IceDamage: return offense.iceDamage;
            case StatType.LightningDamage: return offense.lightningDamage;

            case StatType.Armor: return defense.armor;
            case StatType.Evasion: return defense.evasion;

            case StatType.IceResistance: return defense.iceRes;
            case StatType.FireResistance: return defense.fireRes;
            case StatType.LightningResistance: return defense.lightningRes;

            default:
                Debug.LogWarning($"StatType {type} not implemented yet.");
                return null;


        }
    }

    [ContextMenu("Update Default Stats Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultSetup == null)
        {
            Debug.Log("No default stat setup assigned");
            return;
        }

        resources.maxHealth.SetBaseValue(defaultSetup.maxHealth);
        resources.healthRegen.SetBaseValue(defaultSetup.healthRegen);

        major.strength.SetBaseValue(defaultSetup.strength);
        major.agility.SetBaseValue(defaultSetup.agiity);
        major.intelligence.SetBaseValue(defaultSetup.intelligence);
        major.vitality.SetBaseValue(defaultSetup.vitality);

        offense.attackSpeed.SetBaseValue(defaultSetup.attackSpeed);
        offense.damage.SetBaseValue(defaultSetup.damage);
        offense.critChance.SetBaseValue(defaultSetup.critChance);
        offense.critPower.SetBaseValue(defaultSetup.critPower);
        offense.armorReduction.SetBaseValue(defaultSetup.armorReduction);

        offense.iceDamage.SetBaseValue(defaultSetup.iceDamage);
        offense.fireDamage.SetBaseValue(defaultSetup.fireDamagee);
        offense.lightningDamage.SetBaseValue(defaultSetup.lightningDamage);

        defense.armor.SetBaseValue(defaultSetup.armor);
        defense.evasion.SetBaseValue(defaultSetup.evasion);

        defense.iceRes.SetBaseValue(defaultSetup.iceResistance);
        defense.fireRes.SetBaseValue(defaultSetup.fireResistance);
        defense.lightningRes.SetBaseValue(defaultSetup.lightningResistance);



    }

}
