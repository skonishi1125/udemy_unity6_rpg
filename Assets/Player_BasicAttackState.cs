using UnityEngine;

public class Player_BasicAttackState : EntityState
{

    private float attackVelocityTimer;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GenerateAttackVelocity();
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

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        // タイマーが0になったら、x = 0として止める
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration; // タイマーが < 0 になるまで進ませる
        // プレイヤーの位置
        // xをattackVelocity.xの分だけ (向いている方向も考慮して), yをattackVelocity.yの分だけ進ませる
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }

}
