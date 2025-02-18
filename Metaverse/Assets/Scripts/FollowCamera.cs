using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;
    float offsetY;

    void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 posX = transform.position;
        posX.x = target.position.x + offsetX;
        transform.position = posX;

        Vector3 posY = transform.position;
        posY.y = target.position.y + offsetY;
        transform.position = posY;
    }
}

