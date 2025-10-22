using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // make object go up, increase Y velocity
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        // if Y velocity goes down, character is falling. transfer to fallState
        // We need to be sure we are not in jump attack state when we transfer to fall state.
        // && 移行のstateチェックを行わないと、
        // fallStateとjumpAttackState(yの加速度をマイナスとしたとき)同フレームで条件があってしまい、
        // 片方のアニメーションしか実行されなくなるのでそれを防ぐ。
        if (rb.linearVelocity.y < 0 && stateMachine.currentState != player.jumpAttackState)
            stateMachine.ChangeState(player.fallState);
    }
}
