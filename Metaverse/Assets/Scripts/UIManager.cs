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

    private int currentScore = 0; // ���� ����
    private int highScore = 0; // �ְ� ����

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

    // ���� ���� �г� Ȱ��ȭ
    public void GameInfoPannel()
    {
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
            PlayerPrefs.Save();
        }

        highScoreText.text = highScore.ToString();
    }

    // ���� ȭ������ ������
    public void ExitButtonClick()
    {
        SceneManager.LoadScene("Main Scene");
    }

    // ���� ����� (��� ȭ�� �ݱ� + ���� ���� �ݱ�)
    public void ReStart()
    {
        gameInfoImage.gameObject.SetActive(false); // ���� ���� �г� �ݱ�
        GameManager.Instance.RestartGame();
    }
}
