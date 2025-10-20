using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Jump.WasPerformedThisFrame())
        {
            Debug.Log("Jump!");
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
