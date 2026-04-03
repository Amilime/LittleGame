using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, player.rb.velocity.y); // ÐÞ¸´¿ÕÖÐÔË¶¯×´Ì¬
    }

    public override void Exit()
    {
        base.Exit();
    } 

    public override void Update()
    {
        base.Update();  // EÍ¶ÖÀÎäÆ÷ Êó±ê×ó¼ü¹¥»÷ ÓÒ¼üµ¯·´
        if (Input.GetKeyDown(KeyCode.E) && HasNoSword())
            stateMachine.ChangeState(player.aimSword);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
    }
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<SwordSkill_Controller>().ReturnSword();
        return false;
    }
}
