using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    public Collider2D[] targetColliders;

    // ターゲット検知
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {

        foreach (var target in GetDetectedColliders())
        {
            Debug.Log("Attacking " + target.name);
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
