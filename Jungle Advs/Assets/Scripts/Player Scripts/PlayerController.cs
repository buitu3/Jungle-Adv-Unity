using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public float playerWalkSpd;
    public float jumpForce;
    public float doubleJumpForce;

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
	
	void Update ()
    {
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
                print("jumped");
            }
            else if (isJumping && !isFalling)
            {
                doubleJump();
                print("doublejumped");
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

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        playerTransform.localScale = new Vector3(playerTransform.localScale.x * -1, playerTransform.localScale.y, playerTransform.localScale.z);
    }

    public IEnumerator movePlayerToTheEnd()
    {
        while (true)
        {
            playerRigidbody.velocity = new Vector2(playerWalkSpd * Time.deltaTime, playerRigidbody.velocity.y);
            yield return new WaitForEndOfFrame();
        }  
    }

    //public IEnumerator knockPlayerBack()
    //{
    //    canActive = false;
    //    Vector2 bounceDir = (playerTransform.position - transform.position).normalized;
    //    print(bounceDir);
    //    //playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
    //    //playerRigidbody.AddForce(new Vector2(bounceDir.x * 300f, 0f), ForceMode2D.Impulse);
    //    playerRigidbody.AddForce(bounceDir * 100, ForceMode2D.Impulse);
    //    yield return new WaitForSeconds(1f);
    //    canActive = true;
        
    //}
}
