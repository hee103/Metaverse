using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{


    public float widthPaddingMin = 3f;
    public float widthPaddingMax = 5f;

    public Transform Object;



    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float widthPadding = Random.Range(widthPaddingMin, widthPaddingMax);
        Debug.Log("Random widthPadding: " + widthPadding);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);


        transform.position = placePosition;

        return placePosition;
    }

}