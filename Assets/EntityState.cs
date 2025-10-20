using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;

    /// <summary>
    /// C#�ł̃R���X�g���N�^��`���@�B
    /// EntityState�̃C���X�^���X�����Ƃ��Ɏ����ŌĂ΂�A�������N���X�ϐ��Ɋi�[���Ă����B
    /// </summary>
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.anim;
    }

    protected EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;
    }

    public virtual void Enter()
    {
        // everitime state will be changed, enter will be called
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // we going to run logic of the state here
        Debug.Log("I run update of " + animBoolName);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to a new one
        anim.SetBool(animBoolName, false);
    }
}
