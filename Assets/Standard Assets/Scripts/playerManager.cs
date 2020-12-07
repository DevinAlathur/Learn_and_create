using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;
    public static int numOfCoins;
    public Text coinsText;
    //public Text highscoreText;

    public Transform player;
    
    public Text score;
    public float number;

    public Text highscoreText;


    void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore", 0).ToString("0");
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
       

        //Coin UI
        coinsText.text = "Coins: " + numOfCoins;

        //score UI
        number = player.position.z;
        score.text = number.ToString("0");

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

        }

        //Highscore UI
        if (number > PlayerPrefs.GetFloat("Highscore", 0))
        {
            PlayerPrefs.SetFloat("Highscore", number);
            highscoreText.text = "Highscore: " + number.ToString("0");
        }


        //highscoreText.text = "Highscore: " + playerController.direction.z;

        if (SwipeController.tap || Input.GetKeyDown(KeyCode.Space))
        {
            isGameStarted = true;
            Destroy(startingText);
            //destroy highscore text <<
            
        }

         
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteAll();
        highscoreText.text = "Highscore: 0";
    }
}
