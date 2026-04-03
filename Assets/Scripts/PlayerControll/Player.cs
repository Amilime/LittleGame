using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{ // 这些特别的属性就在玩家类自己定义
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce = 13f;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }


    // 状态机
    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    #endregion
    protected override void Awake()
    { // 这里角色运动通过状态机控制
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this,stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this,stateMachine,"Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine,"WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
    }
    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    { // 更新状态机
        base.Update();
        stateMachine.currentState.Update();

        
        CheckForDashInput();
     

    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    // 冲刺输入
    private void CheckForDashInput()
    {
       

        if (Input.GetKeyDown(KeyCode.LeftShift)&& SkillManager.instance.dash.CanuseSkill())
        {
        
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
