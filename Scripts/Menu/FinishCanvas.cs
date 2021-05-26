using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScoreRating))]

public class FinishCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image medalImage;
    private ScoreRating scoreRating;

    private void Start()
    {
        this.scoreRating = GetComponent<ScoreRating>();
    }

    public void displayFinish(double elapsedTime, Rating rating)
    {
        if(scoreRating == null){this.scoreRating = GetComponent<ScoreRating>();}

        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        string time = timeSpan.ToString("mm\\:ss\\:ff");
        timerText.text = time;

        medalImage.sprite = scoreRating.getScoreSprite(rating);
        gameObject.SetActive(true);
    }

    public void hideFinish()
    {
        gameObject.SetActive(false);
    }
}
