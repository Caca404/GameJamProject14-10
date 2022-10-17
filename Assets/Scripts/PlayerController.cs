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

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    void Start()
    {
        save = saveManager.LoadGame();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        atackArea = transform.GetChild(1).gameObject;
        animator = GetComponent<Animator>();
        animator.SetFloat("Move X", 0);
        animator.SetFloat("Move Y", 0);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            animator.SetFloat("Move X", lookDirection.x);
        }
        else{
            lookDirection.Set(move.x, move.y);
            animator.SetFloat("Move X", lookDirection.x);
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
                    // PolygonCollider2D polygono = atackArea.GetComponent<PolygonCollider2D>();
                    // for (int i = 0; i < polygono.points.Length; i++)
                    // {
                    //     Debug.Log(lookDirection.x < 0);
                    //     if(lookDirection.x < 0){
                    //         polygono.points[i] = new Vector2(polygono.points[i].x * -1, polygono.points[i].y);
                    //         Debug.Log(polygono.points[i]);
                    //     }
                    //     else
                    //         polygono.points[i] = new Vector2(Mathf.Abs(polygono.points[i].x), polygono.points[i].y);
                    // }
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
