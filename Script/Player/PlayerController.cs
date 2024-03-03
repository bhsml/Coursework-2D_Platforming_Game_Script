using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement variables
    [Header("Movement")]
    [SerializeField]
    private float maxSpeed;
    private float move;
    
    // jumping variables
    [Header("Jump")]
    bool grounded = false;
    float groundCheckRadius = 0.05f;
    [SerializeField]
    private LayerMask groundLayer; // The layer that would trigger groundCheck if player is on ground
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float jumpHeight;

    // Body variables
    Rigidbody2D pRB;
    Animator anim;
    bool facingRight;
    Vector2 bodyVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing variables
        pRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
        jumpHeight *= .1f;
    }

    // Called just before performing any physics calculations
    // Physics code go to ensure they all work in sync
    void FixedUpdate()
    {
        Jump();
        UpdateGround();
        MoveLeftRight();
        UpdateAnimation();
    }


    /*********************************************/
    /*********************************************/

    /******************/
    // Use for Flipping Sprite
    /******************/

    // Flips character based on the direction of player movement
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 pScale = transform.localScale;
        pScale.x *= -1;
        transform.localScale = pScale;
    }

    // Flip the sprite to face value of velocity x
    void FaceDirection(float moveVelocityX)
    {
        // this.x is left of target
        if (facingRight && moveVelocityX < 0)
        {
            Flip();
        }
        // this.x is right of target
        else if (!facingRight && moveVelocityX > 0)
        {
            Flip();
        }
    }

    /******************/
    // Use for Moving.
    /******************/

    // Move left or right
    void MoveLeftRight()
    {
        move = Input.GetAxis("Horizontal"); // GetAxis would give float value going up to one or negative one.
        BodyVelocityUpdate(move * maxSpeed, pRB.velocity.y); // velocity y will remain as before
    }

    /******************/
    // Use for Updating Animation and RigidBody.
    /******************/
    // Updates velocity for rigidbody
    void BodyVelocityUpdate(float x, float y)
    {
        bodyVelocity.Set(x, y);
        pRB.velocity = bodyVelocity;
    }

    // Updates for flipping and movement of character
    void UpdateAnimation()
    {
        FaceDirection(pRB.velocity.x);
        anim.SetFloat("speed", Mathf.Abs(pRB.velocity.x)); // "speed" is variable for Animator
    }
    

    /******************/
    // Use for Jumping.
    /******************/
    
    // Falling or grounded
    void UpdateGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // check if we are grounded - if no, then we are in air
        anim.SetBool("isGrounded", grounded); // isGrounded is variable for Animator
        anim.SetFloat("verticalSpeed", pRB.velocity.y); // verticalSpeed is variable for Animator
    }

    // Checking if jump button is pressed, and it is touching ground
    // if true jump
    void Jump()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            grounded = false;
            anim.SetBool("isGrounded", grounded); // isGrounded is variable for Animator
            BodyVelocityUpdate(pRB.velocity.x, jumpHeight);
        }
    }
    
    // Only shows radius of groundCheck on editor.
    // Does nothing in game.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
