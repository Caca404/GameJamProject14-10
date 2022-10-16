using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Save save;
    private SaveManager saveManager = new SaveManager();

    public int maxHealth = 100;
    public int health { get { return currentHealth; }}
    int currentHealth;

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

    private bool isAtacking = false;
    private float atackingTimer = 1f;
    private GameObject atackArea = default;

    void Start()
    {
        save = saveManager.LoadGame();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        atackArea = transform.GetChild(1).gameObject;
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
        else {
            if(isAtacking){
                atackingTimer -= Time.deltaTime;
                if(atackingTimer <= 0){
                    atackingTimer = 1f;
                    isAtacking = false;
                    atackArea.SetActive(isAtacking);
                }
            }
            else{
                if (Input.GetMouseButtonDown(0)){
                    isAtacking = true;
                    atackArea.SetActive(isAtacking);
                }
            }
        }
        


        // if(Input.GetKey("s")){
        //     Collider2D colliderPlayer = GetComponent<Collider2D>();

        //     // colliderPlayer.Size = 

        // }


        if(IsGrounded == true && (Input.GetKey(KeyCode.Space) || Input.GetKey("w"))  && !isInvincible)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey("w")) && !isInvincible && isJumping == true)
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

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("w")) && !isInvincible)
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

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
