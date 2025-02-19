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
    public Image gameInfoImage;
    public Image resultImage;
    public Button start;
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
        gameInfoImage.gameObject.SetActive(false);
        resultImage.gameObject.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }

    // 게임 설명 패널 활성화
    public void GameInfoPannel()
    {
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
            PlayerPrefs.Save();
        }

        highScoreText.text = highScore.ToString();
    }

    // 메인 화면으로 나가기
    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main Scene");
    }

    // 게임 재시작 (결과 화면 닫기 + 게임 정보 닫기)
    public void ReStart()
    {
        gameInfoImage.gameObject.SetActive(false); // 게임 정보 패널 닫기
        GameManager.Instance.RestartGame();
    }
}
