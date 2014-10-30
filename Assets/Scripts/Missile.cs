using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    [SerializeField]
    private float force;

    public void FireWithOffset(float offsetDistance)
    {
        Debug.Log("Fire!!!!");
        if (offsetDistance > 0)
        {
            transform.Translate(transform.forward * offsetDistance);
        }
        gameObject.rigidbody2D.AddForce(transform.right * force);
    }
}
