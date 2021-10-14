using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour,IDamagable
{
    // Start is called before the first frame update



    int layerMask;
    public float health { get; set; }
    [SerializeField] float actualHealth;
    [SerializeField] 
    public float AttackPower;

    [SerializeField] float moveSpeed;
    [SerializeField] float distanceToPlayers;
    [SerializeField] float attackPoint_Radious;
    [SerializeField] float destroyTimer;
    [SerializeField] float destroyGameObjectTimer;
    [SerializeField] bool dead;

    public GameObject healthBar;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask playerLayers;
    Vector3 localScale;
    Friends_AI Friends_AI;

    Player player;
    bool inWalkMode;
   





    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    RaycastHit2D hit;
    GameManger gameManger;
    void Start()
    {
        gameManger = GameObject.Find("Game_Manger_GO").GetComponent<GameManger>();
        actualHealth = Random.Range(1f, 4f);
        localScale = healthBar.transform.localScale;
        health = actualHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        inWalkMode = true;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = health;
        healthBar.transform.localScale = localScale;
        DeadBool();
        RunOnGameOver();


    }
    private void FixedUpdate()
    {
        Movment();
        attackPlayer();
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
                if  (hit != null && dead != true && gameObject.tag != ("Killed"))
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
        

        if (health <= 0.1)
        {
          
            health = 0;
            moveSpeed = 0f;
            anim.SetTrigger("Die_Mode");
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", false);
            gameObject.layer = 11;
            Destroy(gameObject, destroyTimer);
            gameManger.enemyKilledCount += 1;

        }
    }
    public void DamageByPlayer()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        health = health - player.attackPower;


        if (health <=0)
        {
            health = 0;
            moveSpeed = 0f;
            anim.SetTrigger("Die_Mode");
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", false);
            gameObject.layer = 11;
            Destroy(gameObject, destroyTimer);
            gameManger.enemyKilledCount += 1;

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

    void DestroyGO()
    {
        Destroy(gameObject, destroyGameObjectTimer);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Destroy"))
        {
            DestroyGO();
            gameManger.CountEnemy();

        }
       

    }

    void DeadBool()
    {
        if(health<=0)
        {
            dead = true;
        }
    }

    void RunOnGameOver()
    {
        // enemy will rush towrd our City once they Win
        if (gameManger.gameOver)
        {
            anim.SetTrigger("Run_Mode");
            moveSpeed = 5f;
            movement = new Vector2(-moveSpeed * Time.fixedDeltaTime, 0);
        }
    }
}
