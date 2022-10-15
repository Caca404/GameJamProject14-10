using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Save save;
    private SaveManager saveManager = new SaveManager();
    public float speed;
    private float moveInput;
    private bool IsGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private bool isInvincible;
    private float invincibleTimer;
    private float timeInvincible = 1f;

    void Start()
    {
        save = saveManager.LoadGame();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(IsGrounded == true && (Input.GetKey(KeyCode.Space) || Input.GetKey("w")))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey("w")) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("w")))
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        if(!isInvincible){
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    void TakeDamage(){
        if (isInvincible)
            return;
        
        isInvincible = true;
        invincibleTimer = timeInvincible;
    }

    public void TakeDamageAirAtack(){
        this.TakeDamage();
        

    }
}
