using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : EnemyBehavior
{
    // jumping variables
    [Header("Jumping")]
    bool grounded = false;
    float groundCheckRadius = 0.05f;
    [SerializeField]
    private LayerMask groundLayer; // The layer that would trigger groundCheck if enemy is on ground
    [SerializeField]
    private Transform groundCheck;

    // Attack variables
    [Header("Attack")]
    //[SerializeField]
    Vector3 attackDir;
    //[SerializeField]
    bool canAttack;
    //[SerializeField]
    int angle;
    [SerializeField]
    float attackCoolDown;

    // Calls base class start first then initializes
    override protected void Start()
    {
        base.Start();
        canAttack = true;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (targetDetected)
        {
            Attack();
        }
        UpdateGround();
    }
    
    // Attack function
    override protected void Attack()
    {
        if (grounded && canAttack)
        {
            StartCoroutine(FrogAttack());
        }
    }

    // In air or grounded
    void UpdateGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // check if we are grounded - if no, then we are in air
        enemyAnim.SetBool("isGrounded", grounded); // isGrounded is variable for Animator
        enemyAnim.SetFloat("verticalSpeed", enemyRB.velocity.y); // verticalSpeed is variable for Animator
    }

    // The Frog jumps and has a cool down to attack
    // The Attack adds force to body based on angle
    // to launch frog
    IEnumerator FrogAttack()
    {
        canAttack = false;
        angle = Random.Range(30, 60); // Range of 30 to 60 degrees
        float x = target.x - transform.position.x; // For getting the 
        // If Enemy is in front of target
        if (x < 0)
        {
            // angle is degrees, and Vector3.foward is (0, 0, 1) where Z is the axis of rotation
            // The AngleAxis direction is counterclockwise
            attackDir = Quaternion.AngleAxis(-angle, Vector3.forward) * Vector3.left;
        }
        // If Enemy is behind target
        else if (x > 0)
        {
            attackDir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        }
        FaceTarget(x);
        enemyRB.AddForce(attackDir * enemyAttackForce, ForceMode2D.Impulse);
        //enemyRB.velocity = Vector2.ClampMagnitude(enemyRB.velocity, maxSpeed);
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
        yield return null;
    }

    // Only shows radius of groundCheck on editor.
    // Does nothing in game.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
