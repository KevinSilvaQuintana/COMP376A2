using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    public float jetpackForce;
    public float maxVelocity;
    public GameObject missilePrefab;

    private bool isFacingLeft = false;
    private Camera mainCam;
    

    void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {

        RegulateVelocity();
        ManagePlayerInputs();
	}

    private void ManagePlayerInputs()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0)
        {
            gameObject.rigidbody2D.AddForce(gameObject.transform.up * jetpackForce);
        }
        if (horizontal != 0)
        {
            gameObject.rigidbody2D.AddForce(new Vector3(horizontal, 0.0f, 0.0f) * jetpackForce);
        }

        if ((horizontal > 0 && isFacingLeft) || (horizontal < 0 && !isFacingLeft))
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //ShootMissile();
            FireMissile();
        }
    }

    //Adapted from sonic game tutorial
    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 nextScale = transform.localScale;
        nextScale.x *= -1;
        transform.localScale = nextScale;
    }

    //Adapted from http://answers.unity3d.com/questions/9985/limiting-rigidbody-velocity.html
    private void RegulateVelocity()
    {
        float currentVelocity = gameObject.rigidbody2D.velocity.sqrMagnitude;
        //Debug.Log("Current velocity: " + currentVelocity);
        if (currentVelocity > maxVelocity)
        {   
            gameObject.rigidbody2D.velocity *= 0.80f;
        }
    }

    void ShootMissile()
    {
        Debug.Log("Shot!");
        //Get mouse location
        Vector3 shootingDirection = mainCam.ScreenToWorldPoint(Input.mousePosition - mainCam.WorldToScreenPoint(transform.position));
        Debug.Log("Shooting direction: " + shootingDirection);
        float rotationAngle = Vector3.Angle(Vector3.right, shootingDirection);

        //Instantiate missile and assign its forward direction
        GameObject missile = (GameObject) Instantiate(missilePrefab, transform.position, Quaternion.identity);
        missile.transform.Rotate(Vector3.right, rotationAngle);
        //missile.GetComponent<LinearFlight>().forwardDirection = shootingDirection;

    }

    void FireMissile()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        pos = Camera.main.ScreenToWorldPoint(pos);

        Quaternion q = Quaternion.FromToRotation(Vector3.right, pos - transform.position);
        GameObject newMissile = (GameObject)Instantiate(missilePrefab, transform.position, q);
        newMissile.rigidbody2D.AddForce(newMissile.transform.right * 500.0f);
    }
}
