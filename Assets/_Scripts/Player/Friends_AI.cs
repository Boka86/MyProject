using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends_AI : MonoBehaviour,IDamagable
{
    [SerializeField] float moveSpeed;
    public float attackPower;
    [SerializeField] int layerMask;
    [SerializeField] int layerMaskF;
    [SerializeField] float distanceToEnemy;
    [SerializeField] float distanceToF;
    [SerializeField] float destroyTimer;
    [SerializeField] float attackPoint_Radious;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Transform attackPoint;
    Enemy_AI enemy_AI;
    Vector3 localScale;
    public GameObject healthBar;
    float distance;
    Transform otherFreind;

    Rigidbody2D rb;
    public Animator anim;
    Vector2 movement;
    RaycastHit2D hit;
    RaycastHit2D hitF;
    
    bool canWalk;
    bool fightMode;
    public float health { get; set; }


    void Start()
    {
        health = 1f;
        localScale = healthBar. transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        otherFreind = GameObject.FindGameObjectWithTag("Freinds").GetComponent<Transform>();
        canWalk = true;

    }

    // Update is called once per frame
    void Update()
    {
      localScale.x = health;
      healthBar.transform.localScale = localScale;


        AttackMode();
        Avoid();


    }

    private void FixedUpdate()
    {
        Movement();

    }

    void Movement()
    {
        layerMask = LayerMask.GetMask("Enemy");
        hit = Physics2D.Raycast(transform.position, Vector2.right, distanceToEnemy, layerMask);
        if (hit.collider == null&&canWalk)
        {
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", true);
            anim.SetBool("Idle_Mode", false);
            movement = new Vector2(moveSpeed * Time.fixedDeltaTime, 0);
            rb.MovePosition(rb.position + movement);

        }


    }

    void AttackMode()
    {
       
        if(hit.collider!=null)
        {
           
            
            
            fightMode = true;
            anim.SetBool("Fight_Mode", true);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", false);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPoint_Radious, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                IDamagable hit = enemy.gameObject.GetComponent<IDamagable>();
                if (hit != null)
                {
                    hit.Damage();
                }
            }
        }
        else
        {
            fightMode = false;
        }

    }
   void Avoid()
    {
        layerMaskF = LayerMask.GetMask("Avoid");
        hitF = Physics2D.Raycast(transform.position, Vector2.right, distanceToF, layerMaskF);
        if (hitF.collider != null)
        {

            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", true);
            canWalk = false;

        }
        else if(hitF.collider==null&&fightMode==false)
        {
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", true);
            anim.SetBool("Idle_Mode", false);
            canWalk = true;
        }
       
    }
    public void Damage()
    {
        enemy_AI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_AI>();
        health = health - enemy_AI.AttackPower;

        if(health<0.1)
        {
            health = 0;
            anim.SetTrigger("Die_Mode");
            Destroy(gameObject, destroyTimer);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackPoint_Radious);
    }

}

