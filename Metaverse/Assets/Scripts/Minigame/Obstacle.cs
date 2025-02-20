using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{

    GameManager gameManager;
    public float widthPaddingMin = 3f; // 장애물 간 최소 간격
    public float widthPaddingMax = 5f; // 장애물 간 최대 간격

    public Transform Object;
    public void Start()
    {
        // 싱글톤 패턴을 사용하여 GameManager 인스턴스 가져오기
        gameManager = GameManager.Instance;
    }

    // 장애물을 랜덤 배치 하는 함수
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float widthPadding = Random.Range(widthPaddingMin, widthPaddingMax); // 장애물 간격 랜덤 설정
        //Debug.Log("Random widthPadding: " + widthPadding);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0); // 새로운 위치 계산


        transform.position = placePosition; // 장애물 위치 설정

        return placePosition;
    }

    // 플레이어가 장애물을 지나갈 때 점수를 추가하는 이벤트
    private void OnTriggerExit2D(Collider2D other)
    {
        MinigamePlayer player = other.GetComponent<MinigamePlayer>();
        if (player != null)
            gameManager.AddScore(1);
    }
}