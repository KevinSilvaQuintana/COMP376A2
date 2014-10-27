using UnityEngine;
using System.Collections;

public class LinearFlight : MonoBehaviour
{

    public float speed;
    public float speedIncrement;
    public Vector3 flightDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(flightDirection.normalized * speed * Time.deltaTime, Space.World);
    }

    public void IncrementSpeed()
    {
        speed += speedIncrement;
    }

    public void RotateFlightDirection(float deg)
    {
        Quaternion q = Quaternion.AngleAxis(deg, Vector3.forward);
        flightDirection = q * flightDirection;
    }
}
