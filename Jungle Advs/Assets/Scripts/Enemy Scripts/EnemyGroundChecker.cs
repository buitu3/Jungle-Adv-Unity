using UnityEngine;
using System.Collections;

public class EnemyGroundChecker : MonoBehaviour {

    public EnemyController enemyControl;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            enemyControl.Flip();
        }
    }
}
