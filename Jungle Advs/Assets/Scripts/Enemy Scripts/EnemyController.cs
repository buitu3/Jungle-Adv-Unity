using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float enemySpd;

    private Transform enemyTransform;
    private Rigidbody2D enemyRigidbody;
    private Vector2 movement;

    void Awake()
    {
        enemyTransform = GetComponent<Transform>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.Set(enemySpd * Time.deltaTime * enemyTransform.right.x, enemyRigidbody.velocity.y);
        //enemyRigidbody.MovePosition(transform.position + movement);
        enemyRigidbody.velocity = (movement);
    }

    public void Flip()
    {
        enemyTransform.rotation = Quaternion.LookRotation(-enemyTransform.forward, Vector3.up);
    }
}
