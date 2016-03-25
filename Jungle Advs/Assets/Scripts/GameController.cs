using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public int worldNumber;
    public int levelNumber;
    public int lifeCount;

    public Text lifeText;
    public Text fruitText;
    public Text stoneText;
    public Slider healthBar;

    public GameObject pauseGamePanel;
    public GameObject completeGamePanel;

    public Transform currentSpawnPoint;

    [HideInInspector]
    public int fruitCount = 0;
    [HideInInspector]
    public int stoneCount = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Init UI stats
        updateLifeCount();
        healthBar.maxValue = PlayerController.Instance.health;
        healthBar.value = PlayerController.Instance.health;
    }

    public void pauseGame()
    {
        pauseGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        pauseGamePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Level" + levelNumber + " " + worldNumber  + " Scene");
        Time.timeScale = 1f;
    }

    public void nextGame()
    {
        SceneManager.LoadScene("Level" + (levelNumber + 1) + " " + worldNumber + " Scene");
        Time.timeScale = 1f;
    }

    public void completeGame()
    {
        completeGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void gameOver()
    {
        print("died");
    }

    public void updateLifeCount()
    {
        lifeText.text = lifeCount.ToString();
    }

	public void updateFruitText()
    {
        fruitText.text = fruitCount.ToString();
    }

    public void updateStoneText()
    {
        stoneText.text = stoneCount.ToString();
    }

    public void updateHealthBar()
    {
        healthBar.value = PlayerController.Instance.health;
    }
}
