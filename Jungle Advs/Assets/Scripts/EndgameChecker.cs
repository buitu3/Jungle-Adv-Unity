using UnityEngine;
using System.Collections;

public class EndgameChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("Game End");
            AutoCamEdited.Instance.enabled = false;
            PlayerController.Instance.canActive = false;
            StartCoroutine(PlayerController.Instance.movePlayerToTheEnd());
            StartCoroutine(GameController.Instance.completeGame());
        }

    }
}
