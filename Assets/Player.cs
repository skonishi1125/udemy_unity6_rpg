using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Player_WallSlideState wallSlideState { get; private set; }

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;

    [Range(0,1)]
    public float inAirMoveMultiplier = .7f; // should be from 0 to 1.
    [Range(0,1)]
    public float wallSlideSlowMultiplier = .7f;
    private bool facingRight = true;
    private int facingDir = 1; // �����Ă������ �E: 1 ��: -1
    public Vector2 moveInput { get; private set; }

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

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
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        
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
        HandleCollisionDetection();
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

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }

}
