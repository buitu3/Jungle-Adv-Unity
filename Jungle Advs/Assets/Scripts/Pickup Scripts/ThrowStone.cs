using UnityEngine;
using System.Collections;

public class ThrowStone : MonoBehaviour {

    private Rigidbody2D stoneRigidbody;

	void Start () {
        stoneRigidbody = GetComponent<Rigidbody2D>();
        stoneRigidbody.AddForce(new Vector2(8f * PlayerController.Instance.playerTransform.right.x, 3.5f), ForceMode2D.Impulse);

        Destroy(this.gameObject, 6f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
