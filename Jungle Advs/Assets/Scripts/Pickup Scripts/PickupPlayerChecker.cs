using UnityEngine;
using System.Collections;

public class PickupPlayerChecker : MonoBehaviour {

    public AudioClip fruitTakenSound;
    public AudioClip stoneTakenSound;
    [HideInInspector]
    public Vector3 localPosition;

    void Update()
    {
        transform.localPosition = new Vector2(transform.position.x, transform.localPosition.y + localPosition.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (this.tag == "Food")
            {
                Destroy(gameObject);
                GameController.Instance.fruitCount++;
                GameController.Instance.updateFruitText();
                SoundController.Instance.playSingleClip(fruitTakenSound);
            }
            else if (this.tag == "Weapon")
            {
                Destroy(gameObject);
                GameController.Instance.stoneCount += 10;
                GameController.Instance.updateStoneText();
                SoundController.Instance.playSingleClip(stoneTakenSound);
            }
        }

    }
}
