using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{

    GameManager gameManager;
    public float widthPaddingMin = 3f;
    public float widthPaddingMax = 5f;

    public Transform Object;
    public void Start()
    {
        gameManager = GameManager.Instance;
    }


    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float widthPadding = Random.Range(widthPaddingMin, widthPaddingMax);
        Debug.Log("Random widthPadding: " + widthPadding);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);


        transform.position = placePosition;

        return placePosition;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        MinigamePlayer player = other.GetComponent<MinigamePlayer>();
        if (player != null)
            gameManager.AddScore(1);
    }
}