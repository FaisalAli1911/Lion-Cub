using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float movspeed;
    private float dirx;
    private float vertical;
    private bool facingRight = true;
    private Vector3 localScale;
   public bool grounded;
    public bool dead=false;
    private bool isLadder;
    private bool isClimbing;
    public Collider2D coll;
    private spawning spawn;
    public static bool gameOver=false;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        localScale=transform.localScale;
        movspeed = 5f;
        spawn= GameObject.Find("Spawning Barrels").GetComponent<spawning>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if(isLadder)
        {
            isClimbing= true;
        }
        if (!gameOver)
        {
            dirx = Input.GetAxisRaw("Horizontal") * movspeed;
            rb.velocity = new Vector2(dirx, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump")&& grounded==true && !gameOver)
        {
            rb.AddForce(Vector2.up*750f);
        }
        if (Mathf.Abs(dirx) > 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        { anim.SetBool("IsRunning", false); }
        if (grounded==false)
        {
            anim.SetBool("isJumping", true);
           
        }
       else if (grounded)
        {
            anim.SetBool("isJumping", false);
        }
        if (dead)
        {
            anim.SetBool("Dead", true);
        }
        // if(rb.velocity.y<0) 
        //{
        //anim.SetBool("isJumping", false);
        //  anim.SetBool("isFalling", false);
        //}
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            coll.isTrigger= true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * movspeed);
            grounded= false;
        }
        else
        {
            rb.gravityScale = 4.02f;
        }
    }

    private void LateUpdate()
    {
        if (dirx > 0)
        {
            facingRight = true; }

        else if (dirx < 0)
        { 
            facingRight = false;
        }
        if ((facingRight) && (localScale.x < 0) || (!facingRight) && (localScale.x > 0))
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
      }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            {
           
            grounded = true;
            Debug.Log("Ground Collisionaa");
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {

            grounded = true;
            Debug.Log("barrel Collision");
            gameOver = true;
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Barrel"))
        {
           
            dead = true;
            
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            dead = true;
           

        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder= true;
            Debug.Log("climbing");
        }
        if (collision.gameObject.CompareTag("Barrel")&& gameOver==false)
        {
           spawn.UpdateScore(5);
        }
        if (collision.gameObject.CompareTag("trophy"))
        {
            spawn.UpdateScore(500);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            coll.isTrigger = false;
            grounded = true;
        }
    }
        private void OnCollisionExit2D(Collision2D collision)
   {
        if (collision.gameObject.CompareTag("Ground"))
        {

            grounded = false;
           
        }
       

    }

}
