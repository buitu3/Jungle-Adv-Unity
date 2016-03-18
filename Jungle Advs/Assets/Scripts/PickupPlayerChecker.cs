using UnityEngine;
using System.Collections;

public class PickupPlayerChecker : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (this.tag == "Food")
            {
                Destroy(gameObject);
                GameController.Instance.updateFruitText();
            }
            else if (this.tag == "Weapon")
            {
                Destroy(gameObject);
                GameController.Instance.updateStoneText();
            }
        }

    }
}
