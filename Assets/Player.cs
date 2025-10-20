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
    ///  �X�N���v�g�̃��C�t�T�C�N����1�BAwake�̌�Ɏ��s�����
    /// </summary>
    private void OnEnable()
    {
        input.Enable();

        //input.Player.Movement.started - input just begins (�����͂��߁j
        //input.Player.Movement.performed - input is performed (�z�[���h) �ړ��ȂǁB
        //input.Player.Movement.canceled - input stops (�L�[�𗣂�)
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
