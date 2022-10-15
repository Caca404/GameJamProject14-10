using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanaController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        Destroy(gameObject);
    }
}
