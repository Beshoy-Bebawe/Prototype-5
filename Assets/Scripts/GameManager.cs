using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titlescreen;
    // Start is called before the first frame update
    void Start()
    {
      
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
            {
                yield return new WaitForSeconds(spawnRate);
                int index = Random.Range(0 ,targets.Count);
                Instantiate(targets[index]);
            }


    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

    }
    public void UpdateLives(int livesToSub)
    {    
        lives --;
        livesText.text = "Lives:" + lives;
    }
 
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        
    }
    public void RestartGame()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
 
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 4;
        UpdateScore(0);
        UpdateLives(3);
        titlescreen.gameObject.SetActive(false);
         spawnRate /= difficulty;
    }
}       


