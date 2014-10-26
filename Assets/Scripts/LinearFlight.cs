using UnityEngine;
using System.Collections;

public class LinearFlight : MonoBehaviour
{

    public float speed;
    public Vector3 forwardDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(gameObject.transform.forward * speed * Time.deltaTime, Space.World);
    }
}
