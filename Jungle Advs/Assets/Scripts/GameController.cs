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
        updateLifeCount();
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
        SceneManager.LoadScene("Level1 Scene");
        Time.timeScale = 1f;
    }

    public IEnumerator completeGame()
    {
        yield return new WaitForSeconds(4f);
        completeGamePanel.SetActive(true);
        Time.timeScale = 0f;
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
}
