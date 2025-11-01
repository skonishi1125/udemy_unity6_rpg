using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public float damage = 10;

    // ターゲット検知
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {

        // 攻撃判定に入った物理要素たちを順に処理
        foreach (var target in GetDetectedColliders())
        {

            Entity_Health targetHealth = target.GetComponent<Entity_Health>();

            //if (targetHealth != null)
            //    targetHealth.TakeDamage(damage);
            //Debug.Log(transform.position); // このtransformは、攻撃者自身の座標情報
            targetHealth?.TakeDamage(damage, transform); // 同じ

        }

    }

    private Collider2D[] GetDetectedColliders()
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
