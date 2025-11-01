using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, Transform damageDealer)
    {

        // 攻撃者がPlayerでなかった場合（敵だった時）は、TryEnterBattleStateで迎え撃つようにしている
        if (damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer);


        base.TakeDamage(damage, damageDealer);

    }
}
