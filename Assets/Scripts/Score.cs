using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    private int score = 0;

    private readonly int SCORE_SPLIT_BALLOON = 1;
    private readonly int SCORE_LAST_BALLOON = 2;
    private readonly int SCORE_HOT_AIR_BALLOON = 10;
    private TextMesh scoreText;

	// Use this for initialization
	void Start () {
        scoreText = gameObject.GetComponent<TextMesh>();
	}

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public int getScore()
    {
        return score;
    }

    public void AddScoreSplitBalloon()
    {
        score += SCORE_SPLIT_BALLOON;
    }

    public void AddScoreLastBalloon()
    {
        score += SCORE_LAST_BALLOON;
    }

    public void AddScoreHotAirBalloon()
    {
        score += SCORE_HOT_AIR_BALLOON;
    }
}
