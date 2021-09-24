using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] float attackPower;
    [SerializeField] float attackDelayTime;
    float horizon;
    float vertical;


    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;

    [SerializeField] bool canAttackAgain;
    void Start()
    {
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
            StartCoroutine("DelayAttack");
        }
    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelayTime);
        canAttackAgain = true;
    }

}
