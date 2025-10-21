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
        if (rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }

}
