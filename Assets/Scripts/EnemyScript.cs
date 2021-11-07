using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float firingDelay;
    [SerializeField] private GameObject projectile;
    private Rigidbody2D rb;
    private GameObject[] enemySpawn;
    private bool canShoot = true;
    private int hp = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //freeze the rotation to stop it from spinning upon colliding with something
        rb.freezeRotation = true;

        enemySpawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    // Update is called once per frame
    void Update()
    {
        //ignore collision with the top border, allows the enemy to enter the game screen
        Physics2D.IgnoreCollision(enemySpawn[0].GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //shoot projectile code reused from playerscript
        if (canShoot == true)
        {
            StartCoroutine(Projectile());
        }
    }
    private void shootEnemyProjectile()
    {
        GameObject enemyProjectile = Instantiate(projectile, transform.position, transform.rotation);
        enemyProjectile.transform.SetParent(this.transform);
        enemyProjectile.tag = "EnemyProjectile";
        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }
    public IEnumerator Projectile()
    {
        shootEnemyProjectile();
        canShoot = !canShoot;
        yield return new WaitForSeconds(firingDelay);
        canShoot = !canShoot;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //destroy the enemy if it collides with the ground or player
        //if it's hit by a player projectile, hp is reduced by 1

        //I think the explosion stuffs can be put here
        //either instantiate an explosion sprite then destroy it after 1s or so
        //or start an animation for the explosion
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("PlayerProjectile"))
        {
            if (hp > 1)
            {
                hp -= 1;
            }
            //enemy is destroyed when hp reaches 0
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
