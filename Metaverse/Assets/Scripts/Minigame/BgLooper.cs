using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;

    public int obstacleCount = 0; // 장애물의 개수와 마지막 장애물 위치  
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        // 게임 오브젝트 내의 모든 장애물 객체를 찾아 배열로 저장
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        // 첫 번째 장애물의 위치를 마지막 장애물 위치로 설정
        obstacleLastPosition = obstacles[0].transform.position;
        // 장애물의 갯수 업데이트
        obstacleCount = obstacles.Length;

        // 각 장애물 위치를 랜덤하게 설정
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) // 충돌이 발생했을 때 호출되는 함수
    {
        Debug.Log("Triggered: " + collision.name);

        if (collision.CompareTag("Ground"))// 충돌한 객체가 "Ground" 태그를 가진 경우
        {
            // 충돌한 객체가 TilemapCollider2D 타입인지 확인
            TilemapCollider2D tilemapCollider = collision.GetComponent<TilemapCollider2D>();

            if (tilemapCollider != null)
            {
                // 타일맵의 크기 구하고 이동할 양을 설정
                float widthOfBgObject = tilemapCollider.bounds.size.x;
                Vector3 pos = collision.transform.position;

                float moveDistance = widthOfBgObject * 0.25f;
                pos.x += moveDistance;

                collision.transform.position = pos;

            }
            else
            {
                Debug.LogWarning("Collision object does not have a TilemapCollider2D.");
            }

            return;
        }

        // 장애물과 충돌한 경우
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            // 장애물의 위치를 랜덤하게 재배치
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

}