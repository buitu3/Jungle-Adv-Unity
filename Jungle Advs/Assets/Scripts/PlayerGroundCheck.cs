using UnityEngine;
using System.Collections;

public class PlayerGroundCheck : MonoBehaviour {

    public PlayerController playerControl;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            playerControl.isJumping = false;
            playerControl.isDoubleJumping = false;
            playerControl.isFalling = false;
            playerControl.isGrounded = true;

            playerControl.playerAnimator.SetBool("Grounded", true);
            print("grounded");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform" && !playerControl.isJumping)
        {
            playerControl.isGrounded = false;
            playerControl.isFalling = true;

            playerControl.playerAnimator.SetBool("Grounded", false);
        }
    }
}
