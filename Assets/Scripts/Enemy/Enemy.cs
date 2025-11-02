using System.Net.Mail;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    [Header("Battle details")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 2;
    public float battleTimeDuration = 5;
    public float minRetreatDistance = 1;
    public Vector2 retreatVelocity;

    [Header("Movement details")]
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;
    public Transform player { get; private set; }

    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deadState);
    }

    public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == battleState)
            return;

        if (stateMachine.currentState == attackState)
            return;

        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetPlayerReference()
    {
        // player = GameObject.Find("player").transform; // これでもplayerの情報を得られるが、重い
        if (player == null)
            player = PlayerDetected().transform; // Battlestateは感知したときだけ動くので、こちらを使えばよい

        return player;
    }


    public RaycastHit2D PlayerDetected()
    {
        // 感知したもの（地面 or プレイヤー)かを確認
        RaycastHit2D hit =  Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        // なし or Player 以外かのレイヤーかどうかで分岐させる。
        // この場合、地面だった場合はデフォルトの値を返している
        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default; // colliderがnull, pointが(0,0)など、すべてデフォルトの形で返す

        // Playerが感知されていたら、Player情報がhitに格納されているのでそれを返している
        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));



    }

}
