using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow =2;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(0); // 攻击音效
        if (comboCounter > 2 || Time.time >= lastTimeAttacked +comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1.1f; // 这里控制攻击速度

        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);
    }
    

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .1f);

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * xInput * .2f, player.rb.velocity.y);
        }
        else
        {
            // 没有输入时停止移动
            player.SetVelocity(0, player.rb.velocity.y);
        }
    }
}
