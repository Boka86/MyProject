using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    public float health { get; set; }
    [SerializeField] float speed;
    [SerializeField] float attackPower;
    [SerializeField] float attackDelayTime;
    [SerializeField] float attackPoint_Radious;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Transform attackPoint;
    float horizon;
    float vertical;
   

    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    Enemy_AI enemy_AI;

    [SerializeField] bool canAttackAgain;
    void Start()
    {
        health = 1f;

        

        canAttackAgain = true;
        rb   =  GetComponent<Rigidbody2D>();
        anim =  GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerMovment();
        PlayerAttack();
    }

    private void FixedUpdate()
    {
        
    }

    void PlayerMovment()
    {
        movement = new Vector2(horizon, vertical).normalized;
        horizon = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        anim.SetFloat("IsWalking_hor", Mathf.Abs(horizon));
        anim.SetFloat("IsWalking_vert", Mathf.Abs(vertical));
    }

    void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && canAttackAgain)
        {
            anim.SetTrigger("IsAttacking");
            canAttackAgain = false;
            Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackPoint_Radious, enemyLayers);
            foreach(Collider2D enemy in hitEnemies )
            {
                IDamagable hit = enemy.gameObject.GetComponent<IDamagable>();
                if (hit != null)
                {
                    hit.Damage();
                }
            }
            
            StartCoroutine("DelayAttack");
        }
    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelayTime);
        canAttackAgain = true;
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint==null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackPoint_Radious);
    }

public void Damage()
    {
        enemy_AI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_AI>();
     
        health = health - enemy_AI.AttackPower;
        Debug.Log(this.gameObject.name + " health IS " + health);
    }

   
}
