using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //[Header("Target to be attacked")]
    //[SerializeField]
    protected Vector3 target;
    //[SerializeField]
    protected bool targetDetected;

    // Attack Variables
    [Header("Attack")]
    [SerializeField]
    protected float enemyAttackForce;

    // Movement Variables
    [Header("Movement")]
    [SerializeField]
    protected float maxSpeed;
    [Tooltip("Space from target position.")]
    [SerializeField]
    protected float bufferRange;

    // facing
    bool facingRight = false;

    //[Header("Body")]
    // Body
    //[SerializeField]
    protected Transform enemyT;
    //[SerializeField]
    protected Rigidbody2D enemyRB;
    //[SerializeField]
    protected Animator enemyAnim;


    // Start is called before the first frame update
    virtual protected void Start()
    {
        targetDetected = false;
        enemyT = transform.parent;
        enemyAnim = GetComponentInParent<Animator>();
        enemyRB = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (targetDetected)
        {
            Attack();
        }
    }
    /**************************/
    /**************************/
    // Triggers
    /**************************/
    /**************************/
    // What all these triggers do is that they update target variable on
    // player position
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform.position;
            targetDetected = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform.position;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform.position;
            targetDetected = false;
        }
    }


    /**************************/
    /**************************/
    // Attack
    /**************************/
    /**************************/
    // Move Enemy by attacking
    virtual protected void Attack()
    {
        AttackAxisX();
        AttackAxisY();
        enemyRB.velocity = Vector2.ClampMagnitude(enemyRB.velocity, maxSpeed);
    }

    // Attacking on X axis movement
    virtual protected void AttackAxisX()
    {
        float x = target.x - transform.position.x;
        // Prevent the Enemy does go back and forth when nearing target.position
        if (Mathf.Abs(x) < bufferRange)
            return;
        if (x < 0)
        {
            enemyRB.AddForce(new Vector2(-enemyAttackForce, 0));
        }
        else if (x > 0)
        {
            enemyRB.AddForce(new Vector2(enemyAttackForce, 0));
        }
        FaceTarget(x);
    }

    // Attacking on Y axis movement
    virtual protected void AttackAxisY()
    {
        float y = target.y - transform.position.y;
        // Prevent the Enemy does go back and forth when nearing target.position
        if (Mathf.Abs(y) < bufferRange)
            return;
        if (y < 0)
        {
            enemyRB.AddForce(new Vector2(0, -enemyAttackForce));
        }
        else if (y > 0)
        {
            enemyRB.AddForce(new Vector2(0, enemyAttackForce));
        }
    }


    /**************************/
    /**************************/
    // Animation
    /**************************/
    /**************************/

    // Flip the sprite to face target based on Enemy x position away from target
    protected void FaceTarget(float xAwayFromTarget)
    {
        // this.x is left of target
        if (facingRight && xAwayFromTarget < 0)
        {
            Flip();
        }
        // this.x is right of target
        else if (!facingRight && xAwayFromTarget > 0)
        {
            Flip();
        }
    }

    // Flip the Sprite
    protected void Flip()
    {
        facingRight = !facingRight; // Set reverse boolean
        Vector2 eScale = enemyT.localScale;
        eScale.x *= -1;
        enemyT.localScale = eScale;
    }

    
}
