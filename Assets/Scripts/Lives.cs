using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

    [SerializeField]
    private int MAX_LIVES;

    private int currentLives;
    private TextMesh livesText;

	void Start () {
        currentLives = MAX_LIVES;
        livesText = gameObject.GetComponent<TextMesh>();
	}
	
	void Update () {
        livesText.text = "Lives: " + currentLives;
	}

    public void RemoveLife() {
        currentLives --;
    }

    public int GetLives()
    {
        return currentLives;
    }

    public bool IsGameOver()
    {
        return (currentLives < 0);
    }
}
