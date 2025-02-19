using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;
    float offsetY;
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 newPosition = transform.position;
        newPosition.x = target.position.x + offsetX;
        newPosition.y = target.position.y + offsetY;
        if (currentScene == "Main Scene")
        {
            newPosition = Cameracontrol(newPosition);
        }
        transform.position = newPosition;

    }
    public Vector3 Cameracontrol(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, -5.1f, 5.1f);
        pos.y = Mathf.Clamp(pos.y, -3.7f, 3.7f);
        return pos;
    }
}

