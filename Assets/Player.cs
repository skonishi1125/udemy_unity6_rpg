using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerInputSet input {  get; private set; }

    private StateMachine stateMachine;

    public Player_IdleState idleState {  get; private set; }
    public Player_MoveState moveState {  get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;
    private bool facingRight = true;
    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
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

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        // ���ɓ����A�E�����łȂ��ꍇ
        if (xVelocity > 0 && facingRight == false)
            Flip();
        // ���ɓ����A�E�������Ă����ꍇ
        else if (xVelocity < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

}
