using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SaciController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject projectileCana;
    private Vector2[] defaultPositions = {
        new Vector2(9f, -0.9f),
        new Vector2(-9f, -0.9f)
    };
    private string[] defaultAtacks = {
        "furacao",
        "buraco_minhoca"
    };
    private Vector2 lookDirection = new Vector2(-1,0);
    private float intervalAtacks;
    private Random randNum = new Random();
    private string mode = "normal";
    private int projeteis = 0;
    public float rotationSpeed = 1.5f;
    Vector3 currentVelocity;
    public float lookAheadReturnSpeed = 0.5f;

    public int maxHealth = 200;
    public int health { get { return currentHealth; }}
    int currentHealth;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        intervalAtacks = randNum.Next(2, 5);
        currentHealth = maxHealth;
    }

    void Update(){

        if(lookDirection.x > 0) 
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        else if(lookDirection.x < 0)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,180,transform.eulerAngles.z);

        if(intervalAtacks > 0){
            intervalAtacks -= Time.deltaTime;
        }
        else{
            if(mode == "furacao"){
                if(projeteis >= 5){
                    CancelInvoke("lancarProjetil");
                    projeteis = 0;
                    mode = "normal";
                    intervalAtacks = randNum.Next(3, 5);
                    transform.localScale = new Vector2(1,1);
                }
            }
            else{
                if(mode == "normal"){
                    mode = RandomAtack();
                    if(mode != "furacao" && mode != "buraco_minhoca") intervalAtacks = randNum.Next(2, 5);
                }
            }
        }
    }

    public string RandomAtack(){
        int atack = randNum.Next(defaultAtacks.Length);
        Debug.Log(atack);
        switch(defaultAtacks[atack]){
            case "furacao":
                furacaoAtack();
                break;
            case "buraco_minhoca":
                buracoMinhocaAtack();
                break;
            default:
                Debug.Log('a');
                break;
        }

        return defaultAtacks[atack];
    }

    public void furacaoAtack(){
        transform.localScale = new Vector2(2,2);
        InvokeRepeating("lancarProjetil", 2f, 1f);
    }

    private void lancarProjetil()
    {
        GameObject projectileObject = Instantiate(projectileCana, rb.position + Vector2.up, Quaternion.identity);
        CanaController projectile = projectileObject.GetComponent<CanaController>();
        projectile.Launch(lookDirection, 350f);
        projeteis++;
    }

    public void buracoMinhocaAtack(){
        // girar 90 graus
        transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }

    public void TakeDamage(int damage){

        currentHealth = Mathf.Clamp(currentHealth + damage, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
