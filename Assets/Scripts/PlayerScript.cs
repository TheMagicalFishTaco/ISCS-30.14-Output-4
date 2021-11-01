using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float playerSpeed, enemySpeed, firingDelay, spawnDelay;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject[] enemyList;
    private Rigidbody2D rb;
    private bool canShoot = true;
    private bool canSpawn = true;

    //Awake method is called the moment the script is run
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //freeze the rotation to stop it from spinning upon colliding with something
        rb.freezeRotation = true;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Application closes when Escape is pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();            
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (canShoot == true)
            {
                StartCoroutine(Projectile());                
            }

        }
        //sets the velocity of the sprite's rigidbody
        rb.velocity = new Vector2(horizontalInput * playerSpeed, verticalInput * playerSpeed);

        //spawns an enemy
        if (canSpawn == true)
        {
            StartCoroutine(SpawnEnemy());            
        }


    }
    //instantiates a projectile and sets it's speed to 0 along the x-axis, and the speed variable along the y-axis
    private void shootPlayerProjectile()
    {
        GameObject playerProjectile = Instantiate(projectile, transform.position, transform.rotation);
        playerProjectile.transform.SetParent(this.transform);
        playerProjectile.tag = "PlayerProjectile";
        playerProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerSpeed);
    }
    //stops the player from firing projectiles constantly
    public IEnumerator Projectile()
    {
        shootPlayerProjectile();
        canShoot = !canShoot;
        yield return new WaitForSeconds(firingDelay);
        canShoot = !canShoot;
    }

    //Enemy Spawn Code is placed here since the enemy doesn't exist at the game start
    //So attaching this code to the enemy wouldn't work since the script wouldn't be running
    //Enemies are spawned using an array of serialized gameobjects, and a randomized x position
    public IEnumerator SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyList[Random.Range(0,enemyList.Length)], new Vector3(Random.Range(-5.5f,5.5f), 7, 0), Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemySpeed);
        canSpawn = !canSpawn;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = !canSpawn;
    }
}
