using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDir;
    private const int FirstComboIndex = 1; // We start combo index with number 1, this param is used in the Animator.
    private int comboIndex = 1;
    private int comboLimit = 3;


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Adjust comboLimit to match attack velocity array.");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        //if (player.moveInput.x != 0)
        //    attackDir = ((int)player.moveInput.x);
        //else
        //    attackDir = player.facingDir;
        // Define attack direction according to input
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;  // 入力があればその方向に、そうでなければ向いている方向に向かって攻撃


        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }



    // 攻撃中のタスクを定義する
    // 軽く前に進ませたり、敵に当たったかどうか、animationが終わったらどういったstateとするのか。
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        // この値がfalseとなった時点で、idle状態に戻すような処理
        if (triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        // remember time when we attacked
        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            // 実行時点のフレームでfalseとし、その後Coroutineで次フレーム時点でtrueとする
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        // タイマーが0になったら、x = 0として止める
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration; // タイマーが < 0 になるまで進ませる
        // プレイヤーの位置
        // xをattackVelocity.xの分だけ (向いている方向も考慮して), yをattackVelocity.yの分だけ進ませる
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        // if time we attacked was long ago, we reset comboIndex
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }

}
