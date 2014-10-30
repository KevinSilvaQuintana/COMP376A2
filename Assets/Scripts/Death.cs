using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
    [SerializeField]
    private float deathCooldown;
    [SerializeField]
    private float reviveCooldown;

    private PlayerCharacter player;
    private Lives lives;
    public bool isDying = false;
    public bool isReviving = true;

    private float currentTimer;
    public float revivingTimer = 0f;
    public float dyingTimer = 0f;

    void Start()
    {
        player = gameObject.GetComponent<PlayerCharacter>();
        lives = GameObject.FindGameObjectWithTag("Lives").GetComponent<Lives>();
    }

    void Update()
    {
        currentTimer += Time.deltaTime;
        if (isDying)
        {
            dyingTimer += Time.deltaTime;
        }
        if (isReviving)
        {
            revivingTimer += Time.deltaTime;
        }
        if (dyingTimer > deathCooldown)
        {
            dyingTimer = 0f;
            isReviving = true;
            isDying = false;
            Respawn();
        }
        if (revivingTimer > reviveCooldown)
        {
            revivingTimer = 0f;
            isReviving = false;
            isDying = false;
            player.RemoveColor();
            
        }

    }

    public void Die()
    {
        if (!isReviving)
        {
            isDying = true;
            lives.RemoveLife();
            player.DisableControls();
            player.DisableWrapAround();
            player.DisableCollisions();
            
            if (lives.IsGameOver())
            {
                Application.LoadLevel("GameOver");
            }
        }
        else
        {
            Debug.Log("Invincible frames!");
        }
    }

    private void Respawn()
    {
        player.transform.position = new Vector3(0, 0, 0);
        player.EnableControls();
        player.EnableWrapAround();
        player.EnableCollisions();
        player.ApplyColor();
    }
}
