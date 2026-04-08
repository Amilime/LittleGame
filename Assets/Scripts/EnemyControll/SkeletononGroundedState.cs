using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletononGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform player;
    public SkeletononGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    { 
        base.Enter();
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
        {
            player = PlayerManager.instance.player.transform;
        }
        else
        {
            player = null;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player == null)
        {
            // 尝试重新获取玩家引用
            if (PlayerManager.instance != null && PlayerManager.instance.player != null)
            {
                player = PlayerManager.instance.player.transform;
            }
            else
            {
                // 如果玩家不存在，敌人应该进入空闲状态或停止追击
                return;
            }
        }
        if (player != null)
        {
            if (enemy.IsplayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            {
                stateMachine.ChangeState(enemy.battleState);
            }
        }
        else
        {
            // 玩家不存在，敌人应该进入空闲状态
            // 可以根据需要改变状态
            Debug.Log("Player not found, enemy staying in ground state");
        }
    }
}
