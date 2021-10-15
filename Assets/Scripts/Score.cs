using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    void Update()
    {
        if(Ball.GetZ() == 0)
        {
            bestScoreText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            bestScoreText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }

        scoreText.text = GameController.instance.score.ToString();

        if (GameController.instance.score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", GameController.instance.score);

        bestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
