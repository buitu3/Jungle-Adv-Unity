using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float playerWalkSpd;
    public float jumpForce;
    public float doubleJumpForce;

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    [HideInInspector]
    public bool isGrounded = true;
    [HideInInspector]
    public bool isJumping = false;
    [HideInInspector]
    public bool isDoubleJumping = false;
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
	
	void Start () {
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        float h = Input.GetAxis("Horizontal");

        if (h != 0)
        {
            Move(h);
        }

        if (Input.GetButtonDown("Jump"))
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
    }

    void Jump()
    {
        playerRigidbody.AddForce(new Vector2(0f, jumpForce));
        isGrounded = false;
        isJumping = true;
    }      

    void doubleJump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
        playerRigidbody.AddForce(new Vector2(0f, doubleJumpForce));
        isJumping = false;
        isDoubleJumping = true;
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        playerTransform.localScale = new Vector3(playerTransform.localScale.x * -1, playerTransform.localScale.y, playerTransform.localScale.z);
    }
}
