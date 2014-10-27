using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour
{

    public Balloon balloonPrefab;
    public int numBalloonClusters;
    public List<Balloon> balloonClusters;

    void Start()
    {
        balloonClusters = new List<Balloon>();
        CreateBalloonClusters();
    }

    void CreateBalloonClusters()
    {
        for (int i = 0; i < numBalloonClusters; i++)
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 10.0f);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(randomPosition);
            Balloon balloonCluster = (Balloon)Instantiate(balloonPrefab, worldPos, Quaternion.identity);
            balloonCluster.name = "BalloonCluster";
            LinearFlight flight = balloonCluster.GetComponent<LinearFlight>();
            flight.RotateFlightDirection(UnityEngine.Random.Range(0f, 360f));
        }

    }

    public List<Balloon> getBalloonClusters()
    {
        return balloonClusters;
    }
}
