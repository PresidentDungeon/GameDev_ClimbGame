using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScoreRating))]

public class LevelCanvas : MonoBehaviour
{
    private ScoreRating scoreRating;

    [SerializeField] private TMP_Text[] levelScores;
    [SerializeField] private Image[] levelMedals;
    [SerializeField] private string[] sceneNames;
    [SerializeField] TMP_Text bestScore;

    private void Start()
    {
        this.scoreRating = GetComponent<ScoreRating>();
    }

    public void displayCanvas()
    {
        if (scoreRating == null) { this.scoreRating = GetComponent<ScoreRating>(); }

        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (PlayerPrefs.HasKey(sceneNames[i]))
            {
                float elapsedTime = PlayerPrefs.GetFloat(sceneNames[i]);
                Rating levelRating = scoreRating.calculateScore(sceneNames[i], elapsedTime);
                TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
                string time = timeSpan.ToString("mm\\:ss\\:ff");
                levelScores[i].text = time;
                levelMedals[i].sprite = scoreRating.getScoreSprite(levelRating);
            }
        }

        if (PlayerPrefs.HasKey("TotalElapsed"))
        {
            float totalElapsedTime = PlayerPrefs.GetFloat("TotalElapsed");
            TimeSpan totalTimeSpan = TimeSpan.FromSeconds(totalElapsedTime);
            string totalTime = totalTimeSpan.ToString("mm\\:ss\\:ff");
            bestScore.text = totalTime;
        }

        gameObject.SetActive(true);
    }

    public void hideCanvas()
    {
        gameObject.SetActive(false);
    }

}
