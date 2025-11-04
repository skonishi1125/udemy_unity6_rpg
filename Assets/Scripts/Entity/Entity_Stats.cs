using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;

        return baseHp + bonusHp;
    }

    public float GetEvasion()
    {
        // Statクラスから作ったevasionで、GeValue()メソッドを呼ぶ。 
        // -> baseDamageはUnity上のインスペクタで割り当てることができるので、そっちで割り当てた値をdebuglogに出す
        Debug.Log(defense.evasion.GetValue());
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85; // 回避可能な最大の確率

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap); // 0 - 85の間でなければ、85を返すような処理

        return finalEvasion;
    }
}
