using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public float playerWalkSpd;
    public float jumpForce;
    public float doubleJumpForce;

    public GameObject stone;
    public Transform stoneThrowPos;

    [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    public Rigidbody2D playerRigidbody;
    [HideInInspector]
    public Animator playerAnimator;

    [HideInInspector]
    public bool isGrounded = true;
    [HideInInspector]
    public bool isJumping = false;
    [HideInInspector]
    public bool isDoubleJumping = false;
    [HideInInspector]
    public bool canActive = true;
    private bool isFacingLeft = false;
    [HideInInspector]
    public bool isFalling = false;
    private bool moveLeftBtnPressed = false;
    private bool moveRightBtnPressed = false;

    //public enum playerStates
    //{
    //    idle = 0,
    //    left,
    //    right,
    //    jump,
    //    doublejump,
    //    falling,
    //    death
    //}
    Vector2 movement;

    void Awake()
    {
        Instance = this;
    }
	
	void Start ()
    {
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        StartCoroutine(checkMoveBtnPressed());
	}

    void Update()
    {
        print(moveRightBtnPressed);
        float h = Input.GetAxis("Horizontal");

        if (h != 0 && canActive)
        {
            Move(h);
        }

        playerAnimator.SetFloat("vspeed", playerRigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && canActive)
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (isJumping && !isFalling)
            {
                doubleJump();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (GameController.Instance.stoneCount > 0 && canActive)
            {
                GameController.Instance.stoneCount--;
                GameController.Instance.updateStoneText();
                throwStone();
            }
        }

        // Ressurect Player if fall down
        if (playerTransform.position.y < -20)
        {
            playerTransform.position = GameController.Instance.currentSpawnPoint.position;
        }
	}

    void Move(float h)
    {
        if ((h > 0 && isFacingLeft) || (h < 0 && !isFacingLeft))
        {
            Flip();
        }

        movement.Set(playerWalkSpd * h * Time.deltaTime, 0f);
        //playerTransform.Translate(movement);
        playerRigidbody.velocity = new Vector2(playerWalkSpd * h * Time.deltaTime, playerRigidbody.velocity.y);

        //print(playerRigidbody.velocity.x);
        playerAnimator.SetFloat("speed", Mathf.Abs(playerRigidbody.velocity.x));
    }

    void Jump()
    {
        playerRigidbody.AddForce(new Vector2(0f, jumpForce));
        isGrounded = false;
        isJumping = true;

        playerAnimator.SetTrigger("Jump");
        playerAnimator.SetBool("Grounded", false);
    }      

    void doubleJump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
        playerRigidbody.AddForce(new Vector2(0f, doubleJumpForce));
        isJumping = false;
        isDoubleJumping = true;

        playerAnimator.SetTrigger("DoubleJump");
    }

    public void throwStone()
    {
        
        Instantiate(stone, stoneThrowPos.position, Quaternion.identity);

    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        //playerTransform.localScale = new Vector3(playerTransform.localScale.x * -1, playerTransform.localScale.y, playerTransform.localScale.z);
        playerTransform.rotation = Quaternion.LookRotation(-playerTransform.forward, Vector3.up);
    }

    public IEnumerator movePlayerToTheEnd()
    {
        while (true)
        {
            playerRigidbody.velocity = new Vector2(playerWalkSpd * Time.deltaTime, playerRigidbody.velocity.y);
            yield return new WaitForEndOfFrame();
        }  
    }

    // Check Moving Btn State and call move method accordingly
    public void onBtnMoveLeftStateChanged(bool state)
    {
        moveLeftBtnPressed = state;
    }
    public void onBtnMoveRightStateChanged(bool state)
    {
        moveRightBtnPressed = state;
    }
    IEnumerator checkMoveBtnPressed()
    {
        while (true)
        {
            if (moveLeftBtnPressed)
            {
                Move(-1f);
            }
            else if (moveRightBtnPressed)
            {
                Move(1f);
            }
            else
            {
                Move(0f);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    // Make player jump when button Jump is pressed
    public void onBtnJumpPressed()
    {
        if (canActive && Time.timeScale != 0)
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (isJumping && !isFalling)
            {
                doubleJump();
            }
        }
    }

    // Make Player throw stone when button Fire is pressed
    public void onBtnFirePressed()
    {
        if (GameController.Instance.stoneCount > 0 && canActive)
        {
            GameController.Instance.stoneCount--;
            GameController.Instance.updateStoneText();
            throwStone();
        }
    }
}
