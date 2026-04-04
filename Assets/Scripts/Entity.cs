using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{ // inherit继承思想 在这里可以同时有player和enemy的一些特性，继承entity的player只需要重写就好
  // virtual虚函数，可以在必要的时候overide重写
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius; //攻击检测半径
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    // 这里定义组件
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }

    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    #endregion

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knocklbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;


    public System.Action onFlipped;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        // 这里负责绑定组件和初始化
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {

    }
    // 这里后面要全部改成DoDamage
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");

        Debug.Log(gameObject.name + "was hurt");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knocklbackDirection.x * -facingDir, knocklbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;

    }

    // 画线检查墙面和地形碰撞和翻转和速度
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);    
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        onFlipped?.Invoke();
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion

    #region Velocity
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    public virtual void Die()
    {
        
    }
}
