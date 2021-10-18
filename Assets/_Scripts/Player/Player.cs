using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    public float health { get; set; }
    [SerializeField] float speed;
     public float attackPower;
    [SerializeField] float attackDelayTime;
    [SerializeField] float attackPoint_Radious;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Transform attackPoint;
    [SerializeField] public bool died;
    
    float horizon;
    float vertical;
   

    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    Enemy_AI enemy_AI;

    [SerializeField] bool canAttackAgain;
    void Start()
    {
        health = 5f;

        

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
        if (Input.GetMouseButtonDown(0) && canAttackAgain&&died!=true)
        {
            anim.SetTrigger("IsAttacking");
            canAttackAgain = false;
            Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackPoint_Radious, enemyLayers);
            foreach(Collider2D enemy in hitEnemies )
            {
                IDamagable hit = enemy.gameObject.GetComponent<IDamagable>();
                if (hit != null)
                {
                    hit.DamageByPlayer();
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
        if (health <= 0)
        {
            died = true;
            health = 0;
            speed = 0f;
            anim.SetTrigger("Die_Mode");
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", false);
            gameObject.layer = 11;
           // Destroy(gameObject, destroyTimer);
           if(died!=false)
            {
                gameObject.GetComponent<Player>().enabled = false;
                gameObject.GetComponent<Player_Raw_System>().enabled = false;
                

            }
        }
    }

    // i dont need the below it just used for the Idamagable Contract
    public void DamageByPlayer()
    {

    }

    
}
