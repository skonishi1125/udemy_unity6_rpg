using UnityEngine;

public abstract class PlayerState: EntityState
{
    protected Player player;
    protected PlayerInputSet input;


    /// <summary>
    /// C#でのコンストラクタ定義方法。
    /// EntityStateのインスタンスを作るときに自動で呼ばれ、引数をクラス変数に格納していく。
    /// </summary>
    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
        stats = player.stats;
    }

    // everitime state will be changed, enter will be called
    // 指定された状態に入るときに動かす処理。

    public override void Update()
    {
        base.Update();

        // ダッシュボタンを押し、ダッシュができる状態なら
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);

    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

    }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;

        if (stateMachine.currentState == player.dashState)
            return false;

        return true;
    }
}

