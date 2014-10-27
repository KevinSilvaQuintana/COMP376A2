using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    public float initialSpeed;
    public Balloon balloonPrefab;
    public float sizeDecrement;
    public bool isAlive;
    public int balloonsInCluster;

    //private int balloonsInCluster;
    private Vector3 direction;
    private PlayerCharacter player;

    // Use this for initialization
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        Color newColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Missile")
        {
            // Destroy the missile
            Destroy(obj);
            Pop();
        }
        else if (obj.tag == "Player")
        {
            player.Kill();
        }
    }

    void Pop()
    {
        if (balloonsInCluster == 1)
        {
            Die();
        }
        else
        {
            CreateNewBalloon(Random.Range(0f, 180.0f));
            CreateNewBalloon(Random.Range(0f, -180.0f));
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }

    private void CreateNewBalloon(float rotationDegrees)
    {
        Balloon newBalloon = (Balloon)Instantiate(balloonPrefab, transform.position, Quaternion.identity);
        newBalloon.name = "BalloonPrefab";
        newBalloon.transform.localScale *= sizeDecrement;
        newBalloon.isAlive = true;
        newBalloon.balloonsInCluster = this.balloonsInCluster / 2;
        LinearFlight flight = newBalloon.GetComponent<LinearFlight>();
        flight.IncrementSpeed();
        flight.RotateFlightDirection(rotationDegrees);
    }
}
