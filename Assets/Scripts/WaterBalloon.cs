using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour {

    private PlayerCharacter player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collidingObject = col.gameObject;
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
