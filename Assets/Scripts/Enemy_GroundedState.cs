using Unity.VisualScripting;
using UnityEngine;

// idle, moveの親クラス
public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetection() == true)
            stateMachine.ChangeState(enemy.battleState);

    }
}
