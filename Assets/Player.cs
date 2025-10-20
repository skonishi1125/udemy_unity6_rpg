using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }

    private PlayerInputSet input;
    private StateMachine stateMachine;
    public Player_IdleState idleState {  get; private set; }
    public Player_MoveState moveState {  get; private set; }
    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
    }


    /// <summary>
    ///  スクリプトのライフサイクルの1つ。Awakeの後に実行される
    /// </summary>
    private void OnEnable()
    {
        input.Enable();

        //input.Player.Movement.started - input just begins (おしはじめ）
        //input.Player.Movement.performed - input is performed (ホールド) 移動など。
        //input.Player.Movement.canceled - input stops (キーを離す)
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
    }

}
