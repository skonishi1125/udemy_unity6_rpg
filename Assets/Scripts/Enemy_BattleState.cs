using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{

    private Transform player;
    private float lastTimeWasInBattle;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        //if (player == null)
        //    player = enemy.GetPlayerReference();
        player ??= enemy.GetPlayerReference(); // 同じ書き方。nullチェック


        if (ShouldRetreat())
        {
            // enemy.SetVelocityで引かせると、後ろを向いて引いてしまうのでベクトルだけを変える
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }

    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected())
            UpdateBattleTimer();

        if (battleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if (WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.attackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;

    private bool battleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;

    // 敵とplayerとの距離を確認し、近ければtrueを返して攻撃できるようにしている
    //private bool WithinAttackRange()
    //{
    //    return DistanceToPlayer() < enemy.attackDistance;
    //}
    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;


    private float DistanceToPlayer()
    {
        // playerがいない(感知されていないなら、playerは非常に遠い位置にいるはず
        if (player == null)
            return float.MaxValue;

        // 絶対値を返す (ex: 8 - 16 で-8がreturnされても、距離自体は8のはず)
        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    // 敵の向き先
    // 敵xが 1, プxが 10なら、 +の方向に進む必要があるので 1  を返す
    // 敵xが 1, プxが -10なら、-の方向に進む必要があるので -1 を返す
    private int DirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }


}
