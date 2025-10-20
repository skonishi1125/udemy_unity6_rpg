using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    public Player_IdleState idleState {  get; private set; }
    public Player_MoveState moveState {  get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "idle_p");
        moveState = new Player_MoveState(this, stateMachine, "move_p");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

}
