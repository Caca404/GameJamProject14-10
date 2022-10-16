using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        SaciController saci = other.GetComponent<SaciController>();
        Debug.Log(saci);
        if(saci != null)
            saci.TakeDamage(-10);
    }
}