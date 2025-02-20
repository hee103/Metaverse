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

    // ���� ���� ����
    private int currentScore = 0;



    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>(); // UIManager ��ü ã��
    }

    private void Start()
    {
        // ���� ���� �� ������ 0���� �ʱ�ȭ�Ͽ� UI�� �ݿ�
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // ��� �г� Ȱ��ȭ �� ���� ���� ǥ��
        uiManager.ResultPannel();
        uiManager.UpdateResultScore(currentScore);

    }

    public void RestartGame()
    {
        // ���� ���� �ٽ� �ҷ��� ���� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score; // ���� ����
        uiManager.UpdateScore(currentScore); // UI�� ������Ʈ
        Debug.Log("Score: " + currentScore);
    }


}