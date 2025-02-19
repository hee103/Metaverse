using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + collision.name);

        if (collision.CompareTag("Ground"))
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

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

}