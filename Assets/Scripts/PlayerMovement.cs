using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float firingDelay;
    [SerializeField] private GameObject projectile;
    private Rigidbody2D rb;
    private bool canShoot = true;

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
        rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);

    }
    //instantiates a projectile and sets it's speed to 0 along the x-axis, and the speed variable along the y-axis
    private void shootPlayerProjectile()
    {
        GameObject playerProjectile = Instantiate(projectile, transform.position, transform.rotation);
        playerProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }
    //stops the player from firing projectiles constantly
    public IEnumerator Projectile()
    {
        shootPlayerProjectile();
        canShoot = !canShoot;
        yield return new WaitForSeconds(firingDelay);
        canShoot = !canShoot;
    }
}
