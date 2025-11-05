using UnityEngine;
using System;

[Serializable]
public class Stat_OffenseGroup 
{
    // Physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction; // 相手のアーマーの軽減率 (高いほど、攻撃を通しやすい)

    // Elemental Damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    // test

}
