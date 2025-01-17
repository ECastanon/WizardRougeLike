using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float currentMoveSpeed;
    public bool canMove = true;
    private Rigidbody2D rb;
    private Animator animator;

    Vector2 movement;
    Vector2 moveDirection;

    Vector3 mousePos;
    public bool flip = false;

    public Image dashBar;
    private GameObject gameManager;

    [Header("DashData")]
    private TrailRenderer trailRenderer;
    public bool canDash = true;
    public bool isDashing;
    //How much force is applied
    public float dashPower = 40f;
    public float currentDashPower;
    //Dash Cooldowns
    public float dashCD = 2f;
    public float currentDashCD;
    public float dashTime;
    //How long the dash lasts
    public float dashDuration = .1f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        dashBar = GameObject.Find("dashBar").GetComponent<Image>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
        dashTime = dashCD;

        currentMoveSpeed = moveSpeed;
        currentDashPower = dashPower;
        currentDashCD = dashCD;
        
    }

    void Update()
    {
        if(canMove == true)
        {
            movement.x = UserInput.instance.MoveInput.x;
            movement.y = UserInput.instance.MoveInput.y;
            //movement.x = Input.GetAxisRaw("Horizontal");
            //movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("w") ||
            Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            animator.SetBool("isRunning", true);
        } else {animator.SetBool("isRunning", false);}

            moveDirection = new Vector2(movement.x, movement.y).normalized;
            if(dashTime < dashCD)
            {
                dashTime += Time.deltaTime;

                dashBar.fillAmount = 1 - (dashTime / dashCD);
            }
            if (UserInput.instance.DashInput && canDash && dashTime >= dashCD)
            {
                StartCoroutine(Dash());
            }
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

        //Movement
        rb.MovePosition(rb.position + (movement * currentMoveSpeed * Time.fixedDeltaTime));
    }

    void FaceMouse()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (mousePos.x < transform.position.x && flip == false)
        {
            transform.localScale = new Vector2(2, 2);
            flip = true;
        }
        if (mousePos.x >= transform.position.x && flip == true)
        {
            transform.localScale = new Vector2(-2, 2);
            flip = false;
        }

    }

    private void EStone()
    {
        if (gameManager.GetComponent<RelicEffects>().eStoneLvl > 0)
        {
            currentDashPower = dashPower + (dashPower * (gameManager.GetComponent<RelicEffects>().eStoneLvl * .1f));
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        EStone();

        //Adds force to the direction of the arrows keys being held
        rb.velocity = new Vector2(moveDirection.x * currentDashPower, moveDirection.y * currentDashPower);
        //rb.AddForce(movement * dashPower, ForceMode2D.Impulse);

        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        trailRenderer.emitting = false;
        canDash = true;
        isDashing = false;

        dashTime = 0f;
    }
    //Used for temporary movement changes
    //Reverts to baseSpeed + sandals after the effect
    public IEnumerator AdjustSpeed(string operation, float newSpeed, float seconds, bool isDebuff)
    {
        RelicEffects re = GameObject.Find("GameManager").GetComponent<RelicEffects>();
        if(operation == "add")
        {
            if(isDebuff){newSpeed *= (1- (re.aTalismanLvl*0.15f));} //Reduces the effect by 15% for each aTalismanLvl
            currentMoveSpeed += newSpeed;
        }
        if(operation == "multiply")
        {
            if(isDebuff){newSpeed *= (1- (re.aTalismanLvl*0.15f));} //Reduces the effect by 15% for each aTalismanLvl
            currentMoveSpeed *= newSpeed;
        } else {
            Debug.Log("Invalid Operation! Can only Add or Multiply!");
        }
        yield return new WaitForSeconds(seconds);

        //Perfrom the reverse operation to return to normal
        if(operation == "add")
        {
            if(isDebuff){newSpeed *= (1- (re.aTalismanLvl*0.15f));}
            currentMoveSpeed -= newSpeed;
        }
        if(operation == "multiply")
        {
            if(isDebuff){newSpeed *= (1- (re.aTalismanLvl*0.15f));}
            currentMoveSpeed = currentMoveSpeed / newSpeed;
        } else {
            Debug.Log("Invalid Operation to Reverse!");
        }
    }
    public void ResetSpeed() //Resets speed to be baseSpeed with sandals
    {
        currentMoveSpeed = (moveSpeed * (1.0f + gameManager.GetComponent<RelicEffects>().hSandals * .15f));
    }
}
