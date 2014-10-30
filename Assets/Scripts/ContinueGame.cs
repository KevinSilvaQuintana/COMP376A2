using UnityEngine;
using System.Collections;

public class ContinueGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space"))
        {
            Application.LoadLevel("Main");
        }
	}
}
