using UnityEngine;
using System.Collections;

public class EndgameChecker : MonoBehaviour {

    public AudioClip levelClearSound;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {      
            print("Game End");
            AutoCamEdited.Instance.enabled = false;
            PlayerController.Instance.canActive = false;
            StartCoroutine(PlayerController.Instance.movePlayerToTheEnd());
            SoundController.Instance.playSingleClip(levelClearSound);
            yield return new WaitForSeconds(4f);
            GameController.Instance.completeGame();
        }

    }
}
