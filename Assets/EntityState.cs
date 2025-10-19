using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;

    /// <summary>
    /// C#�ł̃R���X�g���N�^��`���@�B
    /// EntityState�̃C���X�^���X�����Ƃ��Ɏ����ŌĂ΂�A�������N���X�ϐ��Ɋi�[���Ă����B
    /// </summary>
    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        // everitime state will be changed, enter will be called
        Debug.Log("I enter " + stateName);
    }

    public virtual void Update()
    {
        // we going to run logic of the state here
        Debug.Log("I run update of " + stateName);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to a new one
        Debug.Log("I exit " + stateName);
    }
}
