using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    // ターゲット検知
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        // 攻撃判定に入った物理要素たちを順に処理
        foreach (var target in GetDetectedColliders())
        {
            IDamagable damegable = target.GetComponent<IDamagable>();

            if (damegable == null)
                continue; // スキップして次のtargetへ

            float elementalDamage = stats.GetElementalDamage();
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            bool targetGotHit = damegable.TakeDamage(damage,elementalDamage, transform); // このtransformは、攻撃者自身の座標情報

            if (targetGotHit)
                vfx.CreateOnHitVFX(target.transform, isCrit);
        }

    }

    protected Collider2D[] GetDetectedColliders()
    {
        // Physicsチェック
        // 該当のpositon, 半径以内に, 指定したレイヤーがあればそれを格納する
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
