using UnityEngine;
using System.Collections;

public class HotAirBalloonSpawner : MonoBehaviour
{

    public float spawnOffsetY;

    [SerializeField]
    private GameObject hotAirBalloonPrefab;
    private readonly float SPEED_UP_FACTOR = 1.3f;

    public void DeployHotAirBalloon(bool isSpawnFromRight, bool isSpeedUpEnabled)
    {
        float spawnY = UnityEngine.Random.Range(spawnOffsetY, Screen.height - spawnOffsetY);
        int spawnX = isSpawnFromRight ? Screen.width : 0;

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnX, spawnY, 10));

        GameObject hotAirBalloon = Instantiate(hotAirBalloonPrefab, spawnPosition, Quaternion.identity) as GameObject;
        hotAirBalloon.name = "hotAirBalloon";
        LinearFlight linearFlight = hotAirBalloon.GetComponent<LinearFlight>();
        if (isSpawnFromRight)
        {
            linearFlight.flightDirection = new Vector3(-1, 0, 0);
        }
        else
        {
            linearFlight.flightDirection = new Vector3(1, 0, 0);
        }
        if (isSpeedUpEnabled)
        {
            linearFlight.AdjustSpeedByMultiplicativeFactor(SPEED_UP_FACTOR);
        }
    }
}
