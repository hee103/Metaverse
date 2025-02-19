using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public Image resultImage;
    public Button exit;
    public Button reStart;

    private int currentScore = 0; // 현재 점수
    private int highScore = 0; // 최고 점수

    public void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }

        resultImage.gameObject.SetActive(false);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString(); 
    }

    public void ResultPannel()
    {
        resultImage.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        currentScoreText.text = score.ToString();
        currentScore = score;

    }

    public void UpdateResultScore(int score)
    {
        resultImage.gameObject.SetActive(true);
        currentScoreText.text = score.ToString();


        // 최고 점수 갱신
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

        }

        highScoreText.text = highScore.ToString(); 
    }

    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ReStart()
    {
        GameManager.Instance.RestartGame();
    }
}