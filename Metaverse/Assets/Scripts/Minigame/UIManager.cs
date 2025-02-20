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


    private int currentScore = 0; // 현재 점수
    private int highScore = 0; // 최고 점수

    public void Start()
    {

     
        GameInfoPannel();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }

    // 게임 설명 패널 활성화
    public void GameInfoPannel()
    {
        Time.timeScale = 0;
        gameInfoImage.gameObject.SetActive(true);


    }

    // 결과 패널 활성화
    public void ResultPannel()
    {
        
        resultImage.gameObject.SetActive(true);
    }

    // 점수 업데이트
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        currentScoreText.text = score.ToString();
        currentScore = score;
    }

    // 게임 종료 후 결과 점수 업데이트
    public void UpdateResultScore(int score)
    {
        resultImage.gameObject.SetActive(true);
        currentScoreText.text = score.ToString();
       
        // 최고 점수 갱신
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

    // 메인 화면으로 나가기
    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main Scene");
    }

    // 게임 재시작 
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
