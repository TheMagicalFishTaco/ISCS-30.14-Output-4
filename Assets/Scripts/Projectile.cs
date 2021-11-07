using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject[] playerProjectile, enemyProjectile;
    public GameObject explosionEffect;
    private GameObject[] enemySpawn;

    //tells the projectile to ignore collisions with the player
    private void Start()
    {
        //tells enemy projectiles to ignore colliding with the enemy spawn border
        enemySpawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
        if (gameObject.tag == "EnemyProjectile")
        {
            Physics2D.IgnoreCollision(enemySpawn[0].GetComponent<Collider2D>(), GetComponent<Collider2D>());            
        }

        //tells all projectiles to ignore colliding with their parent (enemy for enemy projectile, player for player projectile)
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //The following code finds gameobjects tagges as player projectiles and enemyprojectiles, and tells the current projectile
        //to ignore collisions with them.
        //tl;dr, stops projectiles from colliding with one another
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

    //this method handles collisions and instantiates the explosion effect
    //split into player projectile and enemy projectile for ease of use
    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "PlayerProjectile")
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                GameObject projectileExplosion = Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
            if (col.gameObject.CompareTag("EnemySpawn") || col.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.tag == "EnemyProjectile")
        {
            if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player"))
            {
                GameObject projectileExplosion = Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }

    }
}
