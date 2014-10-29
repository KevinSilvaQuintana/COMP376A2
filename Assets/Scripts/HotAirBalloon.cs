using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour
{

    public float screenOffset;

    [SerializeField]
    private GameObject waterBalloonPrefab;
    [SerializeField]
    private float shootingDelay;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D!" + collider.gameObject);
        if (collider.gameObject.tag == "Missile")
        {
            score.AddScoreHotAirBalloon();
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Player")
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

        Quaternion q = Quaternion.FromToRotation(Vector3.right, playerDirection);
        GameObject waterBalloon = (GameObject)Instantiate(waterBalloonPrefab, transform.position, q);

    }
}
