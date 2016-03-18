using UnityEngine;
using System.Collections;

public class SavepointChecker : MonoBehaviour {

    private Animator checkPointAniamtor;
	
	void Start () {
        checkPointAniamtor = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            checkPointAniamtor.SetBool("Activate", true);
            GameController.Instance.currentSpawnPoint = transform;
        }

    }
}
