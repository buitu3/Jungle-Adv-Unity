using UnityEngine;
using System.Collections;

public class PlayerDeathFallChecker : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        // Respawn if Player fall into the deep
        if (other.tag == "Player" && PlayerController.Instance.canActive)
        {
            PlayerController.Instance.canActive = false;
            if (GameController.Instance.lifeCount > 0)
            {
                GameController.Instance.lifeCount -= 1;
            }
            else
            {
                GameController.Instance.lifeCount = 0;
            }
            GameController.Instance.updateLifeCount();

            PlayerController.Instance.reSpawn();
            PlayerController.Instance.canActive = true;
        }
    }
}
        
