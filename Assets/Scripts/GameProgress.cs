using UnityEngine;
using System.Collections;

public class GameProgress : MonoBehaviour {

    private int totalBalloons;
    private int currentBalloons;
    private TextMesh progressText;

    private readonly int BALLOONS_PER_CLUSTER = 15;

	// Use this for initialization
	void Start () {
        BalloonSpawner balloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner").GetComponent<BalloonSpawner>();
        progressText = gameObject.GetComponent<TextMesh>();
        totalBalloons = balloonSpawner.getBalloonClusters().Count * BALLOONS_PER_CLUSTER;
        currentBalloons = totalBalloons;
	}

    void Update()
    {
        int currentProgress = calculateCurrentProgress();
        Debug.Log(currentProgress);
        progressText.text = "Progress: " + currentProgress + "%";
    }

    public void DecrementBalloons()
    {
        currentBalloons -= 1;
        if (currentBalloons == 0)
        {
            Application.LoadLevel("Victory");
        }
    }

    public int calculateCurrentProgress()
    {
        return 100 - (int)(((float)currentBalloons / (float)totalBalloons) * 100f);
    }
}
