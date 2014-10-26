using UnityEngine;
using System.Collections;

public class Timeout : MonoBehaviour {

    [SerializeField]
    private float MAX_TIMEOUT;
    private float bulletTime = 0;
	
	void Update () {
        bulletTime += Time.deltaTime;

        if (bulletTime > MAX_TIMEOUT)
        {
            Destroy(gameObject);
        }
	}
}
