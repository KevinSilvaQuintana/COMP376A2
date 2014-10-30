using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    public float initialSpeed;
    public Balloon balloonPrefab;
    public float sizeDecrement;
    public bool isAlive;
    public int balloonsInCluster;
    public float splitCooldown;

    //private int balloonsInCluster;
    private Vector3 direction;
    private PlayerCharacter player;
    private Score score;
    private GameProgress progress;

    private float splitTimer = 0f;

    // Use this for initialization
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        progress = GameObject.FindGameObjectWithTag("GameProgress").GetComponent<GameProgress>();
        Color newColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        splitTimer += Time.deltaTime;
        if (splitTimer > splitCooldown)
        {
            Pop();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collidingObject = col.gameObject;
        if (collidingObject.tag == "Missile")
        {
            // Destroy the missile
            Destroy(collidingObject);
            Pop();
        }
        else if (collidingObject.tag == "Player")
        {
            player.Kill();
        }
    }

    void Pop()
    {
        if (balloonsInCluster == 1)
        {
            Die();
            score.AddScoreLastBalloon();
        }
        else
        {
            CreateNewBalloon(Random.Range(0f, 180.0f));
            CreateNewBalloon(Random.Range(0f, -180.0f));
            Die();
            score.AddScoreSplitBalloon();
        }
    }

    void Die()
    {
        progress.DecrementBalloons();
        isAlive = false;
        Destroy(gameObject);
    }

    private void CreateNewBalloon(float rotationDegrees)
    {
        Balloon newBalloon = (Balloon)Instantiate(balloonPrefab, transform.position, Quaternion.identity);
        newBalloon.name = "BalloonPrefab";
        newBalloon.transform.localScale *= sizeDecrement;
        newBalloon.balloonsInCluster = this.balloonsInCluster / 2;
        LinearFlight flight = newBalloon.GetComponent<LinearFlight>();
        flight.IncrementSpeed();
        flight.RotateFlightDirection(rotationDegrees);
    }

    public void ReverseDirection()
    {
        Vector3 direction = gameObject.GetComponent<LinearFlight>().flightDirection;
        Debug.Log("before" + direction);
        gameObject.GetComponent<LinearFlight>().flightDirection = -direction;
        Debug.Log("after" + direction);
    }
}
