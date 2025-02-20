using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    UIManager uiManager;

    public static GameManager Instance
    {
        get { return gameManager; }
    }
    public UIManager UIManager
    {
        get { return uiManager; }
    }

    // 현재 점수 변수
    private int currentScore = 0;



    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>(); // UIManager 객체 찾기
    }

    private void Start()
    {
        // 게임 시작 시 점수를 0으로 초기화하여 UI에 반영
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // 결과 패널 활성화 및 최종 점수 표시
        uiManager.ResultPannel();
        uiManager.UpdateResultScore(currentScore);

    }

    public void RestartGame()
    {
        // 현재 씬을 다시 불러와 게임 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score; // 점수 증가
        uiManager.UpdateScore(currentScore); // UI에 업데이트
        Debug.Log("Score: " + currentScore);
    }


}