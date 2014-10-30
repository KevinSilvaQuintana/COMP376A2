using UnityEngine;
using System.Collections;

public class CameraWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collidingObject = col.gameObject;
        
        if (collidingObject.tag == "Balloon")
        {
            Debug.Log("WaterBalloon OnTriggerEnter2D" + collidingObject);
            Balloon balloon = collidingObject.GetComponent<Balloon>();
            balloon.ReverseDirection();
        }
    }
}
