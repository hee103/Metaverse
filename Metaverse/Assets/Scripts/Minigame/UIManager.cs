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
    public TextMeshProUGUI isBestScoreSuc;
    public TextMeshProUGUI isBestScoreFai;
    public Image gameInfoImage;
    public Image resultImage;
    public Button start;
    public Button exit;
    public Button reStart;


    private int currentScore = 0; // ���� ����
    private int highScore = 0; // �ְ� ����

    public void Start()
    {

     
        GameInfoPannel();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }

    // ���� ���� �г� Ȱ��ȭ
    public void GameInfoPannel()
    {
        Time.timeScale = 0;
        gameInfoImage.gameObject.SetActive(true);


    }

    // ��� �г� Ȱ��ȭ
    public void ResultPannel()
    {
        
        resultImage.gameObject.SetActive(true);
    }

    // ���� ������Ʈ
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        currentScoreText.text = score.ToString();
        currentScore = score;
    }

    // ���� ���� �� ��� ���� ������Ʈ
    public void UpdateResultScore(int score)
    {
        resultImage.gameObject.SetActive(true);
        currentScoreText.text = score.ToString();
       
        // �ְ� ���� ����
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            isBestScoreSuc.gameObject.SetActive(true);
            
            PlayerPrefs.Save();
        }
        else
        {
            isBestScoreFai.gameObject.SetActive(true);
        }
        
        highScoreText.text = highScore.ToString();
    }

    // ���� ȭ������ ������
    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main Scene");
    }

    // ���� ����� 
    public void ReStart()
    {

        GameManager.Instance.RestartGame();
    }
    public void StartButtonClick()
    {
        gameInfoImage.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
