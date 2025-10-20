using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    /// <summary>
    /// C#でのコンストラクタ定義方法。
    /// EntityStateのインスタンスを作るときに自動で呼ばれ、引数をクラス変数に格納していく。
    /// </summary>
    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    protected EntityState(StateMachine stateMachine, string stateName)
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
