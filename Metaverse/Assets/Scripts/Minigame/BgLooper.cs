using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;

    public int obstacleCount = 0; // ��ֹ��� ������ ������ ��ֹ� ��ġ  
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        // ���� ������Ʈ ���� ��� ��ֹ� ��ü�� ã�� �迭�� ����
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        // ù ��° ��ֹ��� ��ġ�� ������ ��ֹ� ��ġ�� ����
        obstacleLastPosition = obstacles[0].transform.position;
        // ��ֹ��� ���� ������Ʈ
        obstacleCount = obstacles.Length;

        // �� ��ֹ� ��ġ�� �����ϰ� ����
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) // �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    {
        Debug.Log("Triggered: " + collision.name);

        if (collision.CompareTag("Ground"))// �浹�� ��ü�� "Ground" �±׸� ���� ���
        {
            // �浹�� ��ü�� TilemapCollider2D Ÿ������ Ȯ��
            TilemapCollider2D tilemapCollider = collision.GetComponent<TilemapCollider2D>();

            if (tilemapCollider != null)
            {
                // Ÿ�ϸ��� ũ�� ���ϰ� �̵��� ���� ����
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

        // ��ֹ��� �浹�� ���
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            // ��ֹ��� ��ġ�� �����ϰ� ���ġ
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

}