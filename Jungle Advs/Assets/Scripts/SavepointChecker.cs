using UnityEngine;
using System.Collections;

public class SavepointChecker : MonoBehaviour {

    public AudioClip checkPointSound;
    private Animator checkPointAniamtor;
    private bool isActive = false;
	
	void Start () {
        checkPointAniamtor = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isActive)
        {
            isActive = true;
            checkPointAniamtor.SetBool("Activate", true);
            GameController.Instance.currentSpawnPoint = transform;
            SoundController.Instance.playSingleClip(checkPointSound);
        }

    }
}
