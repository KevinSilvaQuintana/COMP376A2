﻿using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    public float force;

    void FireWithOffset(float offsetDistance)
    {
        if (offsetDistance > 0)
        {
            transform.Translate(transform.forward * offsetDistance);
        }
        gameObject.rigidbody2D.AddForce(transform.right * force);
    }
}