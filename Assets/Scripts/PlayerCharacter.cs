using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    public float jetpackForce;
    public float maxVelocity;
    public float shootingDelay;
    public GameObject missilePrefab;
    public float characterOffset;

    private bool isFacingLeft = false;
    private float shootingCooldown;
    

    void Awake()
    {
        //Small fix to allow player to shoot without waiting delay
        shootingCooldown = shootingDelay;
    }

    // Update is called once per frame
    void Update() {
        shootingCooldown += Time.deltaTime;
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

    void FireMissile()
    {
        if (shootingCooldown > shootingDelay)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Quaternion q = Quaternion.FromToRotation(Vector3.right, pos - transform.position);
            GameObject newMissile = (GameObject)Instantiate(missilePrefab, transform.position, q);

            newMissile.SendMessage("FireWithOffset", characterOffset);
            shootingCooldown = 0;
        }
        
    }

    internal void Kill()
    {
        throw new System.NotImplementedException();
    }
}
