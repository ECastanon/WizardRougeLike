using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 moveDirection;

    Vector3 mousePos;
    private bool flip = false;

    public Image dashBar;

    [Header("DashData")]
    [SerializeField] private TrailRenderer trailRenderer;
    public bool canDash = true;
    public bool isDashing;
    //How much force is applied
    public float dashPower = 40f;
    //Dash Cooldowns
    public float dashCD = 2f;
    public float dashTime;
    //How long the dash lasts
    public float dashDuration = .1f;

    void Start()
    {
        trailRenderer.emitting = false;
        dashTime = dashCD;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(movement.x, movement.y).normalized;

        if(dashTime < dashCD)
        {
            dashTime += Time.deltaTime;

            dashBar.fillAmount = dashTime / dashCD;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        FaceMouse();

        if (Input.GetKeyDown(KeyCode.Space) && canDash && dashTime >= dashCD)
        {
            StartCoroutine(Dash());
        }

        //Movement
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //Adds force to the direction of the arrows keys being held
        rb.velocity = new Vector2(moveDirection.x * dashPower, moveDirection.y * dashPower);
        //rb.AddForce(movement * dashPower, ForceMode2D.Impulse);

        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        trailRenderer.emitting = false;
        canDash = true;
        isDashing = false;

        dashTime = 0f;
    }
}
