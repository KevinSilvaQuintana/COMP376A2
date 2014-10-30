using UnityEngine;
using System.Collections;

public class GameProgress : MonoBehaviour {

    private int totalBalloons;
    private int currentBalloons;
    private int currentProgress;
    private TextMesh progressText;
    private HotAirBalloonSpawner hotAirBallonSpawner;

    private readonly int BALLOONS_PER_CLUSTER = 15;
    private readonly int HOT_AIR_BALLOON_PROGRESS_1 = 30;
    private readonly int HOT_AIR_BALLOON_PROGRESS_2 = 60;
    private readonly int HOT_AIR_BALLOON_PROGRESS_3 = 90;
    private readonly int INCREASED_SPEED_PROGRESS = 80;
    private readonly int VICTORY_PROGRESS = 100;
    private readonly float SPEED_UP_FACTOR = 1.3f;
   
    bool hotAirBalloon1Deployed1 = false;
    bool hotAirBalloon1Deployed2 = false;
    bool hotAirBalloon1Deployed3 = false;
    bool increasedSpeedDeployed = false;

	// Use this for initialization
	void Awake () {
        BalloonSpawner balloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner").GetComponent<BalloonSpawner>();
        hotAirBallonSpawner = GameObject.FindGameObjectWithTag("HotAirBalloonSpawner").GetComponent<HotAirBalloonSpawner>();
        progressText = gameObject.GetComponent<TextMesh>();
        totalBalloons = balloonSpawner.getBalloonClusters().Count * BALLOONS_PER_CLUSTER;
        currentBalloons = totalBalloons;
	}

    void Update()
    {
        currentProgress = CalculateCurrentProgress();
        progressText.text = "Progress: " + currentProgress + "%";
        CheckHotAirBalloonProgress();
        CheckSpeedIncreaseProgress();
        if (currentProgress >= VICTORY_PROGRESS)
        {
            Application.LoadLevel("Victory");
        }
    }

    public void DecrementBalloons()
    {
        currentBalloons -= 1;
        if (currentBalloons == 0)
        {
            Application.LoadLevel("Victory");
        }
    }

    //Checks if hot air balloons should be spawned
    private void CheckHotAirBalloonProgress()
    {
        if (currentProgress > HOT_AIR_BALLOON_PROGRESS_1 && !hotAirBalloon1Deployed1)
        {
            hotAirBallonSpawner.DeployHotAirBalloon(true, increasedSpeedDeployed);
            hotAirBalloon1Deployed1 = true;
        }
        if (currentProgress > HOT_AIR_BALLOON_PROGRESS_2 && !hotAirBalloon1Deployed2)
        {
            hotAirBallonSpawner.DeployHotAirBalloon(false, increasedSpeedDeployed);
            hotAirBalloon1Deployed2 = true;
        }
        if (currentProgress > HOT_AIR_BALLOON_PROGRESS_3 && !hotAirBalloon1Deployed3)
        {
            hotAirBallonSpawner.DeployHotAirBalloon(true, increasedSpeedDeployed);
            hotAirBalloon1Deployed3 = true;
        }
    }

    //Checks if speed should be increased
    private void CheckSpeedIncreaseProgress()
    {
        if (currentProgress > INCREASED_SPEED_PROGRESS && !increasedSpeedDeployed)
        {
            GameObject[] balloons = GameObject.FindGameObjectsWithTag("Balloon");
            foreach (GameObject b in balloons)
            {
                b.GetComponent<LinearFlight>().AdjustSpeedByMultiplicativeFactor(SPEED_UP_FACTOR);
            }
            increasedSpeedDeployed = true;
        }
    }

    private int CalculateCurrentProgress()
    {
        return 100 - (int)(((float)currentBalloons / (float)totalBalloons) * 100f);
    }
}
