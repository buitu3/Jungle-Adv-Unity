using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float enemySpd;

    private Transform enemyTransform;
    private Rigidbody2D enemyRigidbody;
    private Vector3 movement;

    void Awake()
    {
        enemyTransform = GetComponent<Transform>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.Set(enemySpd * Time.deltaTime * enemyTransform.right.x, 0f, 0f);
        //enemyRigidbody.MovePosition(transform.position + movement);
        //enemyRigidbody.AddForce(transform.right * enemySpd);
    }
}
