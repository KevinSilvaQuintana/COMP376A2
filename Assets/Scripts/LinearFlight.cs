using UnityEngine;
using System.Collections;

public class LinearFlight : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public float speedIncrement;
    [SerializeField]
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

    public void AdjustSpeedByMultiplicativeFactor(float factor)
    {
        speed *= factor;
    }

    public void RotateFlightDirection(float deg)
    {
        Quaternion q = Quaternion.AngleAxis(deg, Vector3.forward);
        flightDirection = q * flightDirection;
    }
}
