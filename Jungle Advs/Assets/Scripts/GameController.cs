using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public int lifeCount;

    public Text lifeText;
    public Text fruitText;
    public Text stoneText;

    public Transform currentSpawnPoint;

    private int fruitCount = 0;
    private int stoneCount = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        updateLifeCount();
    }

    public void updateLifeCount()
    {
        lifeText.text = lifeCount.ToString();
    }

	public void updateFruitText()
    {
        fruitCount++;
        fruitText.text = fruitCount.ToString();
    }

    public void updateStoneText()
    {
        stoneCount += 10;
        stoneText.text = stoneCount.ToString();
    }
}
