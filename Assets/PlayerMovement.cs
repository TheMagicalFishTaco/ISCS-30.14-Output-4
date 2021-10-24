using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    //Awake method is called the moment the script is run
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //freeze the rotation to stop it from spinning upon colliding with something
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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


        //if-else block in charge of flipping the sprite if horizontal input is negative (horizontal input is going left)
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //sets the velocity of the sprite's rigidbody
        //rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    MoveUp();
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    MoveDown();
        //}
    }

    private void MoveUp()
    {
        //just sets the vertical velocity to speed
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }
    private void MoveDown()
    {
        //just sets the vertical velocity to negative speed to go down
        rb.velocity = new Vector2(rb.velocity.x, -speed);
    }

}
