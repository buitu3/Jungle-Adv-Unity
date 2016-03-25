using UnityEngine;
using System.Collections;

public class EnemyPlayerChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.Instance.isGrounded && PlayerController.Instance.canActive)
        {
            //PlayerController.Instance.canActive = false;
            //Vector2 bounceDir = (other.transform.position - transform.position).normalized;
            //other.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, other.GetComponent<Rigidbody2D>().velocity.y);
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(bounceDir.x * 300f, 0f));
            //PlayerController.Instance.canActive = true;
            StartCoroutine(knockPlayerBack());
        }
    }

    public IEnumerator knockPlayerBack()
    {
        // Prevent Player from moving when being hurt
        PlayerController.Instance.canActive = false;
        PlayerController.Instance.playerAnimator.SetBool("Active", false);

        // Knock Player back based on impact direction
        Vector2 bounceDir = (PlayerController.Instance.playerTransform.position - transform.position).normalized;
        PlayerController.Instance.playerRigidbody.velocity = new Vector2(0f, PlayerController.Instance.playerRigidbody.velocity.y);
        PlayerController.Instance.playerRigidbody.AddForce(new Vector2(bounceDir.x * 5, 0f), ForceMode2D.Impulse);

        // Play Player hurt animation
        PlayerController.Instance.playerAnimator.SetTrigger("Hurt");

        // Decrease Player's health
        if (PlayerController.Instance.health - 1 > 0)
        {
            PlayerController.Instance.health -= 1;
            GameController.Instance.updateHealthBar();
        }
        else
        {
            PlayerController.Instance.health = 0;
            GameController.Instance.updateHealthBar();

            // Play Player die animation if out of health
            Vector2 dieDir = (PlayerController.Instance.transform.position - gameObject.transform.position);
            if ((PlayerController.Instance.isFacingLeft && dieDir.x > 0) ||
                (!PlayerController.Instance.isFacingLeft && dieDir.x < 0))
            {
                print("die left");
                PlayerController.Instance.playerAnimator.SetTrigger("DieLeft");
            }
            else 
            {
                print("die right");
                PlayerController.Instance.playerAnimator.SetTrigger("DieRight");
            }

            // Show game over scene
            GameController.Instance.gameOver();
        }

        // Make Player able to move again when done playing hurt animation
        yield return new WaitForSeconds(0.75f);
        PlayerController.Instance.canActive = true;
        PlayerController.Instance.playerAnimator.SetBool("Active", true);
    }
}
