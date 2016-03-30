using UnityEngine;
using System.Collections;

public class ThrowStone : MonoBehaviour {

    private Rigidbody2D stoneRigidbody;
    public AudioClip enemyDieSound;

	void Start () {
        stoneRigidbody = GetComponent<Rigidbody2D>();
        stoneRigidbody.AddForce(new Vector2(8f * PlayerController.Instance.playerTransform.right.x, 3.5f), ForceMode2D.Impulse);

        Destroy(this.gameObject, 6f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            
            print("hit");
            Vector2 bounceDir = (other.transform.position - transform.position).normalized;
            print(bounceDir);

            // Disable unnecessary component
            this.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<CircleCollider2D>().enabled = false;
            other.GetComponentsInChildren<BoxCollider2D>()[1].enabled = false;
            other.GetComponent<EnemyController>().enabled = false;
            // Make the enemy bounce off the ground
            other.attachedRigidbody.AddForce(new Vector2(5f * bounceDir.x, 5f), ForceMode2D.Impulse);
            other.transform.rotation = Quaternion.LookRotation(other.transform.forward, -other.transform.up);
            //other.transform.localEulerAngles += new Vector3(180, 0, 0);
            
            Destroy(this.gameObject);
            Destroy(other.gameObject, 2.0f);

            SoundController.Instance.playSingleClip(enemyDieSound);
        }
    }
}
