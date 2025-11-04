using UnityEngine;

public class Player_Combat : Entity_Combat
{

    [Header("Counter attack details")]
    [SerializeField] private float counterRecovery = .1f;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        // 攻撃判定に入ったコライダーを順に処理
        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterble = target.GetComponent<ICounterable>();

            // countableなインターフェースでない対象の場合は、スルーして次
            if (counterble == null)
                continue;

            if (counterble.CanBeCountered)
            {
                counterble.HandleCounter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;

}
