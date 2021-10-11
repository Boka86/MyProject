using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour,IDamagable
{
    // Start is called before the first frame update
    public float AttackPower;
    [SerializeField] float moveSpeed;
    [SerializeField] float distanceToPlayers;
    [SerializeField] float attackPoint_Radious;
    [SerializeField] float destroyTimer;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask playerLayers;
    
    public GameObject healthBar;
    Vector3 localScale;
    public float health { get; set; }
    Friends_AI Friends_AI;
    bool inWalkMode;
    int layerMask;
    
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    RaycastHit2D hit;
    void Start()
    {
        localScale = healthBar.transform.localScale;
        health = 1f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        inWalkMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = health;
        healthBar.transform.localScale = localScale;
        attackPlayer();
    }
    private void FixedUpdate()
    {
        Movment();
    }


    void Movment()
    {
        if(inWalkMode)
        {
            movement = new Vector2(-moveSpeed * Time.fixedDeltaTime, 0);
            rb.MovePosition(rb.position + movement);
            anim.SetBool("Walking_Mode", true);
            anim.SetBool("Attacking_Mode", false);

        }
      

    }
    void attackPlayer()
    {
        layerMask = LayerMask.GetMask("Freinds");
        hit = Physics2D.Raycast(transform.position, Vector2.left, distanceToPlayers, layerMask);

        if(hit.collider!=null)
        {
            inWalkMode = false;
            anim.SetBool("Attacking_Mode", true);
            anim.SetBool("Walking_Mode", false);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPoint_Radious, playerLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                IDamagable hit = enemy.gameObject.GetComponent<IDamagable>();
                if (hit != null)
                {
                    
                    hit.Damage();
                }
            }

        }
        if(hit.collider == null)
        {
            inWalkMode = true;
         
        }

    }

    public void Damage()
    {
        Friends_AI = GameObject.FindGameObjectWithTag("Freinds").GetComponent<Friends_AI>();
        health = health - Friends_AI.attackPower;

        if (health < 0.1)
        {
            //anim.SetTrigger("Die_Mode");
            health = 0;
            // Destroy(gameObject, destroyTimer) ;

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
