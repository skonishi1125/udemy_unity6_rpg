using UnityEngine;

public class Player_BasicAttackState : EntityState
{

    private float attackVelocityTimer;

    private const int FirstComboIndex = 1; // We start combo index with number 1, this param is used in the Animator.
    private int comboIndex = 1;
    private int comboLimit = 3;

    private float lastTimeAttacked;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("I've adjust comboLimit. according to attack velocity array.");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        ResetComboIndexIfNeeded();

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }



    // 攻撃中のタスクを定義する
    // 軽く前に進ませたり、敵に当たったかどうか、animationが終わったらどういったstateとするのか。
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        // この値がfalseとなった時点で、idle状態に戻す
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        // remember time when we attacked
        lastTimeAttacked = Time.time;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        // タイマーが0になったら、x = 0として止める
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration; // タイマーが < 0 になるまで進ませる
        // プレイヤーの位置
        // xをattackVelocity.xの分だけ (向いている方向も考慮して), yをattackVelocity.yの分だけ進ませる
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        // if time we attacked was long ago, we reset comboIndex
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }

}
