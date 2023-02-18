using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;

    Vector3 mousePos;
    private bool flip = false;

    [Header("DashData")]
    public float dashPower = 24f;
    public float dashCD = 3f;
    public float dashCounter = 3f;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Dash();
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        FaceMouse();
    }

    void FaceMouse()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (mousePos.x < transform.position.x && flip == false)
        {
            transform.localScale = new Vector2(-1, 1);
            flip = true;
        }
        if (mousePos.x >= transform.position.x && flip == true)
        {
            transform.localScale = new Vector2(1, 1);
            flip = false;
        }

    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCounter >= dashCD)
            {
                dashCounter = 0; 
                rb.MovePosition(rb.position + (movement * dashPower * Time.fixedDeltaTime));

            }
        }
        if (dashCounter <= dashCD) { dashCounter = dashCounter + Time.fixedDeltaTime; }

    }
}
