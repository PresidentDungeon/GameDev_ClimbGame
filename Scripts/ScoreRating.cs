using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreRating : MonoBehaviour {

    const float level1Trackmaster = 99;
    const float level1Gold = 120;
    const float level1Silver = 150;
    const float level1Bronze = 300;

    const float level2Trackmaster = 148;
    const float level2Gold = 180;
    const float level2Silver = 240;
    const float level2Bronze = 480;

    const float level3Trackmaster = 87;
    const float level3Gold = 120;
    const float level3Silver = 150;
    const float level3Bronze = 180;

    const string totalElapsed = "TotalElapsed";

    [SerializeField] private Sprite TrackmasterMedal;
    [SerializeField] private Sprite GoldMedal;
    [SerializeField] private Sprite SilverMedal;
    [SerializeField] private Sprite BronzeMedal;
    [SerializeField] private Sprite MissingMedal;

    public Rating calculateScore(string sceneName, float elapsedTime)
    {
        switch (sceneName)
        {
            case "Level 1":
                if (elapsedTime <= level1Trackmaster) { return Rating.TRACKMASTER; }
                else if (elapsedTime <= level1Gold){return Rating.GOLD;}
                else if (elapsedTime <= level1Silver){return Rating.SILVER;}
                else if (elapsedTime <= level1Bronze){return Rating.BRONZE;}
                return Rating.MISSING;

            case "Level 2":
                if (elapsedTime <= level2Trackmaster) { return Rating.TRACKMASTER; }
                else if (elapsedTime <= level2Gold) { return Rating.GOLD; }
                else if (elapsedTime <= level2Silver) { return Rating.SILVER; }
                else if (elapsedTime <= level2Bronze) { return Rating.BRONZE; }
                return Rating.MISSING;

            case "Level 3":
                if (elapsedTime <= level3Trackmaster) { return Rating.TRACKMASTER; }
                else if (elapsedTime <= level3Gold) { return Rating.GOLD; }
                else if (elapsedTime <= level3Silver) { return Rating.SILVER; }
                else if (elapsedTime <= level3Bronze) { return Rating.BRONZE; }
                return Rating.MISSING;

            default:
                return Rating.MISSING;
        }
    }

    public Sprite getScoreSprite(Rating rating)
    {
        switch (rating)
        {
            case Rating.MISSING:
                return MissingMedal;
            case Rating.BRONZE:
                return BronzeMedal;
            case Rating.SILVER:
                return SilverMedal;
            case Rating.GOLD:
                return GoldMedal;
            case Rating.TRACKMASTER:
                return TrackmasterMedal;
            default:
                return MissingMedal;
        }
    }

    public void saveScore(string sceneName, float elapsedTime)
    {
        if (PlayerPrefs.HasKey(sceneName))
        {
            float levelTime = PlayerPrefs.GetFloat(sceneName);
            if(levelTime < elapsedTime)
            {
                return;
            }
        }

        PlayerPrefs.SetFloat(sceneName, elapsedTime);
    }

    public void saveTotalScore(float totalTimeElapsed)
    {

        if (PlayerPrefs.HasKey(totalElapsed))
        {
            float runTime = PlayerPrefs.GetFloat(totalElapsed);
            if (runTime < totalTimeElapsed)
            {
                return;
            }
        }

        PlayerPrefs.SetFloat(totalElapsed, totalTimeElapsed);
    }
}

