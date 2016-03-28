using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public float playerWalkSpd;
    public float jumpForce;
    public float doubleJumpForce;
    public float fireRate = 0.5f;
    public int health = 5;

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
    [HideInInspector]
    public bool isFacingLeft = false;
    [HideInInspector]
    public bool isFalling = false;
    private bool moveLeftBtnPressed = false;
    private bool moveRightBtnPressed = false;

    private float nextFire;

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
	}

    void Update()
    {
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

        if (Input.GetButtonDown("Fire2"))
        {
            if (GameController.Instance.stoneCount > 0 && canActive && Time.time > nextFire)
            {
                GameController.Instance.stoneCount--;
                GameController.Instance.updateStoneText();
                StartCoroutine(throwStone());

                nextFire = Time.time + fireRate;
            }
        }

        //// Ressurect Player if fall down
        //if (playerTransform.position.y < -20)
        //{
        //    reSpawn();
        //}
	}

    void FixedUpdate()
    {        
        float h = Input.GetAxis("Horizontal");

        if (h != 0 && canActive)
        {
            Move(h);
        }

        checkMoveBtnPressed();

        playerAnimator.SetFloat("speed", Mathf.Abs(playerRigidbody.velocity.x));

        playerAnimator.SetFloat("vspeed", playerRigidbody.velocity.y);

        // Respawn if Player fall into the deep
        if (playerTransform.position.y < -20)
        {
            if (GameController.Instance.lifeCount > 0)
            {
                GameController.Instance.lifeCount -= 1;
            }
            else
            {
                GameController.Instance.lifeCount = 0;
            }
            GameController.Instance.updateLifeCount();

            PlayerController.Instance.reSpawn();
        }
    }

    void Move(float h)
    {
        if ((h > 0 && isFacingLeft) || (h < 0 && !isFacingLeft))
        {
            Flip();

        }

        // Scrolling BG according to Player movement
        if (!Mathf.Approximately(playerRigidbody.velocity.x, 0.0f))
        {
            BackGroundScroller.current.Go(h);
        }
        else
        {
            BackGroundScroller.current.Go(0f);
        }

        movement.Set(playerWalkSpd * h * Time.deltaTime, 0f);
        //playerTransform.Translate(movement);
        playerRigidbody.velocity = new Vector2(playerWalkSpd * h, playerRigidbody.velocity.y);

        //print(playerRigidbody.velocity.x);
        //playerAnimator.SetFloat("speed", Mathf.Abs(playerRigidbody.velocity.x));
        //playerAnimator.SetFloat("speed", Mathf.Abs(h));
    }

    void Jump()
    {
        playerRigidbody.AddForce(new Vector2(0f, jumpForce));
        isGrounded = false;
        isJumping = true;
        isDoubleJumping = false;

        //playerAnimator.SetTrigger("Jump");
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

    private IEnumerator throwStone()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            playerAnimator.SetTrigger("ThrowStone");
            yield return new WaitForSeconds(0.4f);
            Instantiate(stone, stoneThrowPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(stone, stoneThrowPos.position, Quaternion.identity);
        }
    }

    public void reSpawn()
    {
        playerTransform.position = GameController.Instance.currentSpawnPoint.position;
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
            playerRigidbody.velocity = new Vector2(playerWalkSpd, playerRigidbody.velocity.y);
            yield return new WaitForFixedUpdate();
        }  
    }

    #region Mobile buttons control methods

    // Check Moving Btn State and call move method accordingly
    public void onBtnMoveLeftStateChanged(bool state)
    {
        moveLeftBtnPressed = state;       
    }
    public void onBtnMoveRightStateChanged(bool state)
    {
        moveRightBtnPressed = state;
    }
    void checkMoveBtnPressed()
    {
        if (canActive)
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
        if (GameController.Instance.stoneCount > 0 && canActive && Time.time > nextFire)
        {
            GameController.Instance.stoneCount--;
            GameController.Instance.updateStoneText();
            StartCoroutine(throwStone());

            nextFire = Time.time + fireRate;        }
    }
    #endregion
}
