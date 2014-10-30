using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour
{

    public float screenOffset;

    [SerializeField]
    private GameObject waterBalloonPrefab;
    [SerializeField]
    private float shootingDelay;
    [SerializeField]
    private float balloonOffset;

    private PlayerCharacter player;
    private float shootingCooldown;
    private Score score;


    // Use this for initialization
    void Start()
    {
        shootingCooldown = shootingDelay;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        shootingCooldown += Time.deltaTime;

        if (isOutOfScreen())
        {
            Destroy(gameObject);
        }

        if (shootingCooldown > shootingDelay)
        {
            ShootWaterBalloon();
            shootingCooldown = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            score.AddScoreHotAirBalloon();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            player.Kill();
            Destroy(gameObject);
        }
    }

    private bool isOutOfScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return (screenPosition.x < -screenOffset || screenPosition.x > Screen.width + screenOffset);
    }

    private void ShootWaterBalloon()
    {
        Vector3 playerDirection = player.transform.position - gameObject.transform.position;

        Quaternion q = Quaternion.FromToRotation(Vector3.up, playerDirection);
        GameObject waterBalloon = (GameObject)Instantiate(waterBalloonPrefab, transform.position, q);
        waterBalloon.name = "WaterBallooon";
        LinearFlight linearFlight = waterBalloon.GetComponent<LinearFlight>();
        linearFlight.flightDirection = playerDirection;
        //Must rotate drops because they spawn upside down
        waterBalloon.transform.Rotate(new Vector3(1, 0, 0), 180f);
        
    }
}
