using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //tells the projectile to ignore collisions with the player
    private void Start()
    {
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    //if the projectile collides with the border, destroy it
    //put the code to destroy the projectile when hitting an enemy here
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
