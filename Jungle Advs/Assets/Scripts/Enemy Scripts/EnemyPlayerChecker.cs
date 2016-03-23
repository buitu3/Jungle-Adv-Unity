using UnityEngine;
using System.Collections;

public class EnemyPlayerChecker : MonoBehaviour {

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player" && PlayerController.Instance.isGrounded)
    //    {
    //        //PlayerController.Instance.canActive = false;
    //        //Vector2 bounceDir = (other.transform.position - transform.position).normalized;
    //        //other.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, other.GetComponent<Rigidbody2D>().velocity.y);
    //        //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(bounceDir.x * 300f, 0f));
    //        //PlayerController.Instance.canActive = true;
    //        StartCoroutine(knockPlayerBack());
    //    }

    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        print("aaa");
        if (other.gameObject.tag == "Player" && PlayerController.Instance.isGrounded)
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
        PlayerController.Instance.canActive = false;
        Vector2 bounceDir = (PlayerController.Instance.playerTransform.position - transform.position).normalized;
        PlayerController.Instance.playerRigidbody.velocity = new Vector2(0f, PlayerController.Instance.playerRigidbody.velocity.y);
        PlayerController.Instance.playerRigidbody.AddForce(new Vector2(bounceDir.x * 5, 0f), ForceMode2D.Impulse);
        PlayerController.Instance.playerAnimator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.75f);
        PlayerController.Instance.canActive = true;

    }
}
