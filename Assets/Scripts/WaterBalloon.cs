using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour {

    private PlayerCharacter player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collidingObject = col.gameObject;
        Debug.Log("WaterBalloon OnTriggerEnter2D" + collidingObject);
        if (collidingObject.tag == "Missile")
        {
            // Destroy the missile
            Destroy(collidingObject);
            Destroy(gameObject);
        }
        else if (collidingObject.tag == "Player")
        {
            player.Kill();
            Destroy(gameObject);
        }
    }
}
