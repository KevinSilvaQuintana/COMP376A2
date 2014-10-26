using UnityEngine;
using System.Collections;

public class IgnoreCharacterCollision : MonoBehaviour
{

    // Use this for initialization
    void Start() {
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider2D>();
        Collider2D rocketCollider = gameObject.GetComponentInChildren<Collider2D>();

        Physics2D.IgnoreCollision(rocketCollider, playerCollider);
	}

    // Update is called once per frame
    void Update()
    {

    }
}
