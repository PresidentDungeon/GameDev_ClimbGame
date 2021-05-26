using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScoreRating))]

public class FinishGameCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text totalTimerInfo;
    [SerializeField] private TMP_Text totalTimerText;
    [SerializeField] private Image medalImage;
    private ScoreRating scoreRating;

    private void Start()
    {
        this.scoreRating = GetComponent<ScoreRating>();
    }

    public void displayFinish(float elapsedTime, float? totalElapsedTime, Rating rating)
    {
        if (scoreRating == null) { this.scoreRating = GetComponent<ScoreRating>(); }
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        string time = timeSpan.ToString("mm\\:ss\\:ff");
        timerText.text = time;

        if(totalElapsedTime != null)
        {
            totalTimerInfo.gameObject.SetActive(true);
            totalTimerText.gameObject.SetActive(true);

            TimeSpan totalTimeSpan = TimeSpan.FromSeconds(totalElapsedTime.Value);
            string totalTime = totalTimeSpan.ToString("mm\\:ss\\:ff");
            totalTimerText.text = totalTime;
        }
        else
        {
            totalTimerInfo.gameObject.SetActive(false);
            totalTimerText.gameObject.SetActive(false);
        }

        Sprite medalSprite = scoreRating.getScoreSprite(rating);
        medalImage.sprite = medalSprite;
        gameObject.SetActive(true);
    }
}
