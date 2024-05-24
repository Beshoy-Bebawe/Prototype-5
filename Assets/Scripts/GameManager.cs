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
    private int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool isGameActive;
    public bool isGamePaused;
    public Button restartButton;
    public GameObject titlescreen;
    public GameObject pausescreen;
    public Slider volumeSlider;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = 1;
      
       volumeSlider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive == true)

         {
            if (isGamePaused == false)
            {
            Time.timeScale = 0;
            isGamePaused = true;
            pausescreen.gameObject.SetActive(true);
            return;
            }else if (isGamePaused == true)
            {
                Time.timeScale = 1;
                isGamePaused = false;
                pausescreen.gameObject.SetActive(false);
                return;
            }

             
         }
          
        
        
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        audioSource.volume = volumeSlider.value;
        Debug.Log(volumeSlider.value);
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
        lives += livesToSub;
        if (lives < 1)
        {
            lives = 0;
            GameOver();
        } 
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
        isGamePaused = false;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);
        titlescreen.gameObject.SetActive(false);
         spawnRate /= difficulty;

     
    }
    
}       


