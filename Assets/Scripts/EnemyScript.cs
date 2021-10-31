using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float firingDelay;
    [SerializeField] private GameObject projectile;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > 3.5f)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), GetComponent<Collider>(),false);
        }
        if (canShoot == true)
        {
            StartCoroutine(Projectile());
        }
    }
    private void shootEnemyProjectile()
    {
        GameObject enemyProjectile = Instantiate(projectile, transform.position, transform.rotation);
        enemyProjectile.transform.SetParent(this.transform);
        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }
    public IEnumerator Projectile()
    {
        shootEnemyProjectile();
        canShoot = !canShoot;
        yield return new WaitForSeconds(firingDelay);
        canShoot = !canShoot;
    }
}
