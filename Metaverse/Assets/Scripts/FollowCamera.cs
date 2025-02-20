using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 따라갈 대상
    float offsetX; // 카메라 초기 x위치
    float offsetY; // 카메라 초기 y위치
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장
        if (target == null)
            return;

        // 카메라와 대상의 위치 차이 계산 후 저장
        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null)
            return;

        // 새로운 카메라 위치 계산
        Vector3 newPosition = transform.position;
        newPosition.x = target.position.x + offsetX;
        newPosition.y = target.position.y + offsetY;
        if (currentScene == "Main Scene") // 현재 씬 이름이 Main 씬일 때
        {
            newPosition = Cameracontrol(newPosition); // 카메라 제어 함수 호출
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 0.45f); // 러프 기능
        }
        else if (currentScene == "Game Scene") // 현재 씬 이름이 Game Scene일 때
        {
            // 러프 기능 없이 바로 따라감
            transform.position = newPosition;
            newPosition = Cameracontrol(newPosition); 
        }
    }
    public Vector3 Cameracontrol(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, -5.1f, 5.1f); // x 이동 범위 제한
        pos.y = Mathf.Clamp(pos.y, -3.7f, 3.7f); // y 이동 범위 제한
        return pos;
    }

}

