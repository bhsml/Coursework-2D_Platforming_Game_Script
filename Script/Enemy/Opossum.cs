using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : EnemyBehavior
{
    // Attack variables
    [Header("Attack")]
    Vector3 attackDir;
    [SerializeField]
    bool canAttack;

    // Sets Range of Random attack cool down
    [SerializeField] [Range(0.1f, 1f)]
    private float attackCoolDownMin;
    [SerializeField] [Range(0.1f, 1f)]
    private float attackCoolDownMax;
    float attackCoolDown;
    
    // Make sure that the max does not go below min
    private void OnValidate()
    {
        if(attackCoolDownMax < attackCoolDownMin)
        {
            attackCoolDownMax = attackCoolDownMin;
        }
    }


    protected override void Start()
    {
        base.Start();
        canAttack = true;
    }

    protected override void Update()
    {
        base.Update();
        enemyAnim.SetFloat("speed", Mathf.Abs(enemyRB.velocity.x)); // Set speed of animator
    }

    // Attack
    protected override void Attack()
    {
        if (canAttack)
        {
            StartCoroutine(OpossumAttack());
        }
    }

    // The Opossum charges and has a cool down to attack
    // The Attack adds force to body
    // The random attackCoolDown makes it attack at random intervals
    IEnumerator OpossumAttack()
    {
        canAttack = false;
        float x = target.x - transform.position.x + bufferRange * Random.Range(-1, 1); // Randomizes bufferRange from -1 - 1
        // If Enemy is in front of target
        if (x < 0)
        {
            attackDir = Vector3.left;
        }
        // If Enemy is behind target
        else if (x > 0)
        {
            attackDir = Vector3.right;
        }
        enemyRB.velocity = Vector2.zero;
        enemyRB.AddForce(attackDir * enemyAttackForce * Random.Range(0.5f, 1f), ForceMode2D.Impulse); // Randomizes Force by Force * Range[0.5,1]
        FaceTarget(x);
        enemyRB.velocity = Vector2.ClampMagnitude(enemyRB.velocity, maxSpeed);
        attackCoolDown = Random.Range(attackCoolDownMin, attackCoolDownMax);
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
        yield return null;
    }


}

