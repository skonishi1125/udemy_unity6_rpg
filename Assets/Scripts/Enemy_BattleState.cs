using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{

    private Transform player;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Debug.Log("I entered battle state.");
        //player = GameObject.Find("player").transform; // これでもplayerの情報を得られるが、重い

        if (player == null)
            player = enemy.PlayerDetection().transform; // Battlestateは感知したときだけ動くので、こちらを使えばよい

    }

    public override void Update()
    {
        base.Update();

        if (WithinAttackRange())
            stateMachine.ChangeState(enemy.attackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }

    // 敵とplayerとの距離を確認し、近ければtrueを返して攻撃できるようにしている
    //private bool WithinAttackRange()
    //{
    //    return DistanceToPlayer() < enemy.attackDistance;
    //}
    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;


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
