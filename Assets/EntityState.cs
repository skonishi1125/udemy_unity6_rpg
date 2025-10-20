using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    /// <summary>
    /// C#でのコンストラクタ定義方法。
    /// EntityStateのインスタンスを作るときに自動で呼ばれ、引数をクラス変数に格納していく。
    /// </summary>
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    protected EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;
    }

    // everitime state will be changed, enter will be called
    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
    }

    // we going to run logic of the state here
    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    // this will be called, everytime we exit state and change to a new one
    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }
}
