using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour {


    [SerializeField]
    private GameObject waterBalloonPrefab;
    [SerializeField]
    private float shootingDelay;
    [SerializeField]
    private float ScreenOffset = 50f;

    private PlayerCharacter player;
    private float shootingCooldown;

	// Use this for initialization
	void Start () {
        shootingCooldown = shootingDelay;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
        shootingCooldown += Time.deltaTime;
        if (shootingCooldown > shootingDelay)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Quaternion q = Quaternion.FromToRotation(Vector3.right, pos - transform.position);
            GameObject waterBalloon = (GameObject)Instantiate(waterBalloonPrefab, transform.position, q);
            shootingCooldown = 0;
        }
	}
}
