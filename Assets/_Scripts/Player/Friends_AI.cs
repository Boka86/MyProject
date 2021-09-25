using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends_AI : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float attackPower;
    [SerializeField] int layerMask;
    [SerializeField] float distanceToEnemy;
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    RaycastHit2D hit;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
   

        
    }

    // Update is called once per frame
    void Update()
    {
         layerMask = LayerMask.GetMask("Enemy");
         hit = Physics2D.Raycast(transform.position, Vector2.right, distanceToEnemy, layerMask);

        AttackMode();

    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (hit.collider == null)
        {
            anim.SetBool("Fight_Mode", false);
            anim.SetBool("Walking_Mode", true);
            movement = new Vector2(moveSpeed * Time.fixedDeltaTime, 0);
            rb.MovePosition(rb.position + movement);

        }

    }

    void AttackMode()
    {
       
        if(hit.collider!=null)
        {
            Debug.Log(hit.point + hit.collider.gameObject.name);
            anim.SetBool("Fight_Mode", true);
            anim.SetBool("Walking_Mode", false);
        }

    }

}
