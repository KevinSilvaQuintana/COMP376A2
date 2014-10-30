using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField]
    private Balloon balloonPrefab;
    [SerializeField]
    private int numBalloonClusters;
    [SerializeField]
    private List<Balloon> balloonClusters;

    //Calling in awake b/c need to be initialized before other objects. Eg: GameProgress
    void Awake()
    {
        balloonClusters = new List<Balloon>();
        CreateBalloonClusters();
    }

    void CreateBalloonClusters()
    {
        for (int i = 0; i < numBalloonClusters; i++)
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(0.2f, 0.8f), UnityEngine.Random.Range(0.2f, 0.8f), 10.0f);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(randomPosition);
            Balloon balloonCluster = (Balloon)Instantiate(balloonPrefab, worldPos, Quaternion.identity);
            balloonCluster.name = "BalloonCluster";
            LinearFlight flight = balloonCluster.GetComponent<LinearFlight>();
            flight.RotateFlightDirection(UnityEngine.Random.Range(0f, 360f));

            balloonClusters.Add(balloonCluster);
        }
    }

    public List<Balloon> getBalloonClusters()
    {
        return balloonClusters;
    }
}
