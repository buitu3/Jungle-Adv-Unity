using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public int worldNumber;
    public int levelNumber;
    public int lifeCount;

    // GamePlay UI
    public Text lifeText;
    public Text fruitText;
    public Text stoneText;
    public Slider healthBar;

    public GameObject pauseGamePanel;
    public GameObject completeGamePanel;
    // LifeOver Panel
    public GameObject lifeOverPanel;
    public Text worldNumberText;
    public Text levelNumberText;
    public Text lifeNumberText;
    // GameOver Panel
    public GameObject gameOverPanel;

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
        healthBar.maxValue = PlayerController.Instance.maxHealth;
        healthBar.value = PlayerController.Instance.maxHealth;
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

    
    public IEnumerator lifeOver()
    {
        // Show life over UI
        lifeOverPanel.SetActive(true);
        worldNumberText.text = worldNumber.ToString();
        levelNumberText.text = levelNumber.ToString();
        lifeNumberText.text = lifeCount.ToString();
        yield return new WaitForSeconds(3f);

        // Disable life over UI
        lifeOverPanel.SetActive(false);
        PlayerController.Instance.reSpawn();
    }

    public void gameOver()
    {
        //print("gameOver");
        gameOverPanel.SetActive(true);
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
        healthBar.value = PlayerController.Instance.currentHealth;
    }
}
