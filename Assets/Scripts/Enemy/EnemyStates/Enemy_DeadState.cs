using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private Collider2D col;

    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        col = enemy.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        //base.Enter(); // DeadStateには特にどのanimも割り当てないので、従来のbase.Enter()は実行する必要がない
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        //　enemy.SetVelocity(rb.linearVelocity.x, 15); を使うと、knockbackなどが考慮されてしまう
        // なのでそのまま、y軸に力を加えてあげて、gravityScaleを高めて落としてやるとそれっぽい
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        // DeadStateとなった場合、状態が切り替わることはなくなるという想定で作る
        stateMachine.SwitchOffStateMachine();


    }

}
