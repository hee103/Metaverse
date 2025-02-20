using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ���� ���
    float offsetX; // ī�޶� �ʱ� x��ġ
    float offsetY; // ī�޶� �ʱ� y��ġ
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        if (target == null)
            return;

        // ī�޶�� ����� ��ġ ���� ��� �� ����
        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null)
            return;

        // ���ο� ī�޶� ��ġ ���
        Vector3 newPosition = transform.position;
        newPosition.x = target.position.x + offsetX;
        newPosition.y = target.position.y + offsetY;
        if (currentScene == "Main Scene") // ���� �� �̸��� Main ���� ��
        {
            newPosition = Cameracontrol(newPosition); // ī�޶� ���� �Լ� ȣ��
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 0.45f); // ���� ���
        }
        else if (currentScene == "Game Scene") // ���� �� �̸��� Game Scene�� ��
        {
            // ���� ��� ���� �ٷ� ����
            transform.position = newPosition;
            newPosition = Cameracontrol(newPosition); 
        }
    }
    public Vector3 Cameracontrol(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, -5.1f, 5.1f); // x �̵� ���� ����
        pos.y = Mathf.Clamp(pos.y, -3.7f, 3.7f); // y �̵� ���� ����
        return pos;
    }

}

