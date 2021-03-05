using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    private AudioSource audioSource;
    [SerializeField] private AudioClip antStomp;

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float spawnRangeY = 1.37f;

    private float spawnRangeX = -7;

    private float spawnRangeZ = 7;

    

    

    private float timeLeft;

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        audioSource = GetComponent<AudioSource>();
        timeLeft = 20;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }

        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(spawnRangeX, spawnRangeY, Random.Range(-spawnRangeZ, spawnRangeZ));
        return spawnPosition;

    }

    public void AntStomp()
    {
        audioSource.PlayOneShot(antStomp);
    }
    

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;

    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Update()
    {
        CountDownTimer();
    }
    public void CountDownTimer()
    {
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeLeft);
            if (timeLeft < 0)
            {
                GameOver();
            }
        }
    }

}
