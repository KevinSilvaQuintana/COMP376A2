using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float jetpackForce;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float shootingDelay;
    [SerializeField]
    private float specialShootingDelay;
    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private float characterOffset;
    [SerializeField]
    private bool specialMode;

    private bool isFacingLeft = false;
    private float shootingCooldown;
    private float specialShotCooldown;
    public bool isControlsEnabled = true;
    private Death playerDeath;
    private Color originalColor;
    private Color grayscaleColor;
    private Color currentColor;
    

    void Awake()
    {
        //Small fix to allow player to shoot without waiting delay
        shootingCooldown = shootingDelay;
        specialShotCooldown = specialShootingDelay;
        playerDeath = gameObject.GetComponent<Death>();
        originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update() {
        shootingCooldown += Time.deltaTime;
        specialShotCooldown += Time.deltaTime;
        RegulateVelocity();
        if (isControlsEnabled)
        {
            ManagePlayerInputs();
        }        
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

    public void Kill()
    {
        playerDeath.Die();
    }

    public void DisableControls()
    {
        isControlsEnabled = false;
    }

    public void EnableControls()
    {
        isControlsEnabled = true;
    }

    public void DisableWrapAround() {
        gameObject.GetComponent<WrapAround>().enabled = false;
    }

    public void EnableWrapAround() {
        gameObject.GetComponent<WrapAround>().enabled = true;
    }

    public void DisableCollisions()
    {
        //gameObject.GetComponent<IgnoreCollision>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableCollisions()
    {
        //gameObject.GetComponent<IgnoreCollision>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ApplyColor()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0f, 0f);
    }

    public void RemoveColor()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = originalColor;
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

    private void FireMissile()
    {
        if (shootingCooldown > shootingDelay)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Quaternion q = Quaternion.FromToRotation(Vector3.right, pos - transform.position);
            GameObject newMissile = (GameObject)Instantiate(missilePrefab, transform.position, q);
            newMissile.GetComponent<Missile>().FireWithOffset(characterOffset);
            shootingCooldown = 0;

            if (specialMode && specialShotCooldown > specialShootingDelay)
            {
                Debug.Log("SHOT SPECIAL");
                GameObject newMissile2 = (GameObject)Instantiate(missilePrefab, transform.position, q);
                newMissile2.GetComponent<Missile>().RotateFlightDirection(180.0f);
                newMissile2.GetComponent<Missile>().FireWithOffset(characterOffset);
                
                specialShotCooldown = 0;
            }
        }
        
    }

}
