using UnityEngine;

public class Player_FallState : EntityState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        // check if player detecting the ground below, if yes: go to idle state.
    }
}
