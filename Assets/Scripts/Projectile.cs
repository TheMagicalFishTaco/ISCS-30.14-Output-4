using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject[] playerProjectile, enemyProjectile;

    //tells the projectile to ignore collisions with the player
    private void Start()
    {
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        playerProjectile = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        enemyProjectile = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        foreach (var projectile in playerProjectile)
        {
            if (!(projectile.GetComponent<Collider2D>() == GetComponent<Collider2D>()))
                Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        foreach (var projectile in enemyProjectile)
        {
            if (!(projectile.GetComponent<Collider2D>() == GetComponent<Collider2D>()))
                Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    //This method makes sure that the projectile is destroyed whenever it collides with anything
    //So that would be the borders, the player, and the enemy
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemySpawn") || col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
