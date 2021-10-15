using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends_AI : MonoBehaviour, IDamagable
{
    public float health { get; set; }
    [SerializeField] float currentHealth;
    [SerializeField] float moveSpeed;
    public float attackPower;
    float x2attackPower;
    [SerializeField] int layerMask;
    [SerializeField] int layerMaskF;
    [SerializeField] float distanceToEnemy;
    [SerializeField] float distanceToF;
    [SerializeField] float destroyTimer;
    [SerializeField] float attackPoint_Radious;
    [SerializeField] float increaseRate;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject avoidDetecd;
    [SerializeField] float destroyGameObjectTimer;
    GameManger gameManger;

    bool dead;
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
    Player player;

    bool canWalk;
    bool fightMode;



    void Start()
    {
        x2attackPower = attackPower * 2;
        gameManger = GameObject.Find("Game_Manger_GO").GetComponent<GameManger>();
        player = GameObject.Find("Player").GetComponent<Player>();
        dead = false;
        health = currentHealth;
        localScale = healthBar.transform.localScale;
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
        DeadBool();
        X2Attack();
        RunOnGameOver();
      

        Avoid();


    }

    private void FixedUpdate()
    {
        Movement();
        AttackMode();
    }

    void Movement()
    {
        layerMask = LayerMask.GetMask("Enemy");
        hit = Physics2D.Raycast(transform.position, Vector2.right, distanceToEnemy, layerMask);
        if (hit.collider == null && canWalk)
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

        if (hit.collider != null)
        {



            fightMode = true;
            anim.SetBool("Fight_Mode", true);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", false);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPoint_Radious, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                IDamagable hit = enemy.gameObject.GetComponent<IDamagable>();
                if (hit != null && dead != true && gameObject.tag != ("Killed"))
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
            anim.SetBool("Idle_Mode", false);
            anim.SetBool("Block_Mode", true);
            canWalk = false;

        }
        else if (hitF.collider == null && fightMode == false)
        {
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", true);
            anim.SetBool("Idle_Mode", false);
            anim.SetBool("Block_Mode", false);
            canWalk = true;
        }

    }
    public void Damage()
    {
        enemy_AI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_AI>();
        health = health - enemy_AI.AttackPower;

        if (health <= 0)
        {

            Destroy(avoidDetecd);
            health = 0;
            moveSpeed = 0f;
            anim.SetTrigger("Die_Mode");
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", false);
            anim.SetBool("Idle_Mode", false);
            gameObject.layer = 11;
            canWalk = false;
            Destroy(gameObject, destroyTimer);
            gameManger.friendlyKilledCount += 1;


        }
    }

    // i dont need the below it just used for the Idamagable Contract
    public void DamageByPlayer()
    {

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
        if (coll.gameObject.tag == ("Destroy_F"))
        {
            // increase health if player passed the barrier to enemy Raw


            if (player.health < 5 && player.died != true)
            {
                player.health = player.health + increaseRate;

            }
            gameManger.CountFriend();
            Destroy(avoidDetecd);

            //////////
            DestroyGO();

        }


    }
    void DeadBool()
    {
        if (health <= 0)
        {
            dead = true;
        }
    }
    void RunOnGameOver()
    {
        // enemy will rush towrd our City once they Win
        if (gameManger.gameOver)
        {
            anim.SetTrigger("Die_Mode");
            GetComponent<Collider2D>().enabled = false;
            moveSpeed = 0;

        }
    }

    public void X2Attack()
    {
        if(gameManger.x2attackPower)
        {
            attackPower = x2attackPower;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }

       else if (gameManger.x2attackPower!=true)
        {
            attackPower = x2attackPower/2;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        }
    }
}

