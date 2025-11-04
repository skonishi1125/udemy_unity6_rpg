using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    public float damage = 10;

    // ターゲット検知
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
    }

    public void PerformAttack()
    {

        // 攻撃判定に入った物理要素たちを順に処理
        foreach (var target in GetDetectedColliders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue; // スキップして次のtargetへ

            bool targetGotHit = damagable.TakeDamage(damage, transform); // このtransformは、攻撃者自身の座標情報

            if (targetGotHit)
                vfx.CreateOnHitVFX(target.transform);
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
