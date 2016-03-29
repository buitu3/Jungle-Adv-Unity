using UnityEngine;
using System.Collections;

public class EnemyPlayerChecker : MonoBehaviour {

    public Rigidbody2D enemyRigidbody;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                if (PlayerController.Instance.isGrounded && PlayerController.Instance.canActive)
                {
                    // Make Player knocked back and take damage
                    StartCoroutine(knockPlayerBack());
                }

                if (PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    other.attachedRigidbody.velocity =
                        new Vector2(other.attachedRigidbody.velocity.x, 0f);
                    other.attachedRigidbody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);

                    gameObject.GetComponentsInParent<BoxCollider2D>()[1].enabled = false;
                    gameObject.GetComponentInParent<CircleCollider2D>().enabled = false;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponentInParent<EnemyController>().enabled = false;
                    enemyRigidbody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
                    gameObject.transform.parent.rotation =
                        Quaternion.LookRotation(gameObject.transform.parent.forward, -gameObject.transform.parent.up);

                    Destroy(gameObject.transform.parent.gameObject, 2.0f);
                }
                break;

            case "Box":
                gameObject.GetComponentInParent<EnemyController>().Flip();
                break;

            default:
                break;
        }


        //if (other.tag == "Player" && PlayerController.Instance.isGrounded && PlayerController.Instance.canActive)
        //{
        //    // Make Player knocked back and take damage
        //    StartCoroutine(knockPlayerBack());
        //}

        //if (other.tag == "Player" && PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.y < 0)
        //{
        //    print("kill the snail");
        //    other.attachedRigidbody.velocity = 
        //        new Vector2(other.attachedRigidbody.velocity.x, 0f);
        //    other.attachedRigidbody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);

        //    gameObject.GetComponentsInParent<BoxCollider2D>()[1].enabled = false;
        //    gameObject.GetComponentInParent<CircleCollider2D>().enabled = false;
        //    gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //    gameObject.GetComponentInParent<EnemyController>().enabled = false;
        //    enemyRigidbody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        //    gameObject.transform.parent.rotation = 
        //        Quaternion.LookRotation(gameObject.transform.parent.forward, -gameObject.transform.parent.up);           

        //    Destroy(gameObject.transform.parent.gameObject, 2.0f);
        //}
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
        if (PlayerController.Instance.currentHealth - 1 > 0)
        {
            PlayerController.Instance.currentHealth -= 1;
            GameController.Instance.updateHealthBar();
        }
        else
        {
            // Player die if out of health
            PlayerController.Instance.currentHealth = 0;
            GameController.Instance.updateHealthBar();

            // Play Player die animation based on impact direction
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

            // Reduce Player life count
            // Show lifeOver UI and respawn Player
            if (GameController.Instance.lifeCount > 0)
            {
                GameController.Instance.lifeCount -= 1;
                yield return new WaitForSeconds(2f);
                StartCoroutine(GameController.Instance.lifeOver());
            }
            // Show gameover UI if Player run out of lives
            else
            {
                GameController.Instance.lifeCount = 0;
                yield return new WaitForSeconds(2f);
                GameController.Instance.gameOver();
            }
            GameController.Instance.updateLifeCount();

            yield break;
        }

        // Make Player able to move again when done playing hurt animation
        yield return new WaitForSeconds(0.75f);
        PlayerController.Instance.canActive = true;
        PlayerController.Instance.playerAnimator.SetBool("Active", true);
    }
}
