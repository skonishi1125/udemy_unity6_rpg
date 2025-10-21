using UnityEngine;

public class Player_BasicAttackState : EntityState
{

    private float attackVelocityTimer;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GenerateAttackVelocity();
    }

    // �U�����̃^�X�N���`����
    // �y���O�ɐi�܂�����A�G�ɓ����������ǂ����Aanimation���I�������ǂ�������state�Ƃ���̂��B
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        // ���̒l��false�ƂȂ������_�ŁAidle��Ԃɖ߂�
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        // �^�C�}�[��0�ɂȂ�����Ax = 0�Ƃ��Ď~�߂�
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration; // �^�C�}�[�� < 0 �ɂȂ�܂Ői�܂���
        // �v���C���[�̈ʒu
        // x��attackVelocity.x�̕����� (�����Ă���������l������), y��attackVelocity.y�̕������i�܂���
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }

}
